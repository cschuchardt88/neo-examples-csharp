using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;

using System;
using System.ComponentModel;
using System.Numerics;


namespace Neo.SmartContract.Examples
{
    [DisplayName("MyNep17Contract")]
    [ManifestExtra("Author", "neo.events")]
    [ManifestExtra("Description", "NEP-17 Example")]
    [ManifestExtra("Email", "examples@neo.events")]
    [ManifestExtra("Website", "https://www.neo.events/")]
    [ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp/blob/master/src/Nep17ContractExample/MyNep17Contract.cs")]
    [SupportedStandards("NEP-17")]
    [ContractPermission("*", "onNEP17Payment")]
    public class MyNep17Contract : Nep17Token
    {
        #region Prefixes

        private const byte Prefix_Contract = 0x02;
        private const byte Prefix_Minter = 0x03;

        #endregion

        #region Storage

        private static readonly StorageMap ContractMap = new(Storage.CurrentContext, Prefix_Contract);
        private static readonly StorageMap MinterMap = new(Storage.CurrentContext, Prefix_Minter);

        #endregion

        #region Keys

        private static readonly ByteString OwnerKey = "\xff";

        #endregion

        #region Owner

        // Replace with "NWLA1iinq7mVYKpV5dYxMjsagZXsnWsyYV" with your own address.
        [InitialValue("NWLA1iinq7mVYKpV5dYxMjsagZXsnWsyYV", ContractParameterType.Hash160)]
        private static readonly UInt160 Owner = default;

        [Safe]
        public static UInt160 GetOwner() =>
            (UInt160)ContractMap.Get(OwnerKey);

        private static bool IsOwner() =>
            Runtime.CheckWitness(GetOwner());

        public delegate void OnSetOwnerDelegate(UInt160 account);

        [DisplayName("SetOwner")]
        public static event OnSetOwnerDelegate OnSetOwner;

        public static void SetOwner(UInt160 account)
        {
            if (IsOwner() == false)
                throw new InvalidOperationException("No Authorization!");
            if (account != null && account.IsValid)
            {
                ContractMap[OwnerKey] = account;
                OnSetOwner(account);
            }
        }

        #endregion

        #region Minter

        [Safe]
        public static UInt160[] GetMinters()
        {
            var iter = MinterMap.Find(options: FindOptions.KeysOnly | FindOptions.RemovePrefix);
            List<UInt160> minters = new();
            while (iter.Next())
                minters.Add((UInt160)iter.Value);
            return minters;
        }

        private static bool IsMinter()
        {
            var tx = (Transaction)Runtime.ScriptContainer;
            return Runtime.CheckWitness(tx.Sender) && MinterMap[tx.Sender] != null;
        }

        public delegate void OnSetMinterDelegate(UInt160 account, bool canMint);

        [DisplayName("SetMinter")]
        public static event OnSetMinterDelegate OnSetMinter;

        public static void SetMinter(UInt160 account, bool canMint)
        {
            if (IsOwner() == false)
                throw new InvalidOperationException("No Authorization!");
            MinterMap[account] = canMint ? "\x01" : null;
            OnSetMinter(account, canMint);
        }

        #endregion

        #region NEP17

        [Safe]
        public override byte Decimals() => Factor();

        [Safe]
        public override string Symbol() => "NEP17";

        public static new void Mint(UInt160 account, BigInteger amount)
        {
            if (IsOwner() == false || IsMinter() == false)
                throw new InvalidOperationException("No Authorization!");
            Nep17Token.Mint(account, amount);
        }

        public static new void Burn(UInt160 account, BigInteger amount)
        {
            if (IsOwner() == false || IsMinter() == false)
                throw new InvalidOperationException("No Authorization!");
            Nep17Token.Burn(account, amount);
        }

        #endregion

        #region Basic

        public static byte Factor() => 8; // This is an exponent (how many decimals places)

        public static bool Verify() => IsOwner();

        public static void _deploy(object data, bool update)
        {
            if (update)
                return;
            ContractMap[OwnerKey] = Owner;
            Nep17Token.Mint(Owner, 100000000 * BigInteger.Pow(10, Factor()));
        }

        public static bool Update(ByteString nefFile, string manifest)
        {
            if (IsOwner() == false)
                throw new InvalidOperationException("No Authorization!");
            ContractManagement.Update(nefFile, manifest);
            return true;
        }

        public static bool Destroy()
        {
            if (IsOwner() == false)
                throw new InvalidOperationException("No Authorization!");
            ContractManagement.Destroy();
            return true;
        }

        #endregion
    }
}
