using Neo;
using Neo.SmartContract.Framework.Attributes;
using System.Numerics;

#pragma warning disable CS0626

namespace CallContracts;

[Contract("0xb0674a6ee70ff7c86ab6287a63697d6d445efba7")]
public class MyNep17Contract
{
    [ContractHash]
    public static extern UInt160 Hash { get; }

    [Safe]
    public static extern bool Verify();

    #region Owner

    public static extern void SetOwner(UInt160 account);

    [Safe]
    public static extern UInt160 GetOwner();

    #endregion

    #region Minter

    public static extern void SetMinter(UInt160 account, bool canMint);

    [Safe]
    public static extern UInt160[] GetMinters();

    #endregion

    #region NEP17

    [Safe]
    public static extern byte Decimals();

    [Safe]
    public static extern string Symbol();

    [Safe]
    public static extern BigInteger TotalSupply();

    [Safe]
    public static extern byte Factor();

    [Safe]
    public static extern BigInteger BalanceOf(UInt160 owner);

    public static extern bool Transfer(UInt160 from, UInt160 to, BigInteger amount, object data);

    public static extern void Mint(UInt160 account, BigInteger amount);

    public static extern void Burn(UInt160 account, BigInteger amount);

    #endregion

}
