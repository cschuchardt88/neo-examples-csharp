// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

#pragma warning disable CS0626

using Neo;
using Neo.SmartContract.Framework.Attributes;
using System.Numerics;

namespace CallContract;

[Contract("0xf0385a6ee70ff7c86ab6287a63697d6d445efba9")]
public class ExampleCoin
{
    [ContractHash]
    public static extern UInt160 Hash { get; }

    #region NEP-17

    [Safe]
    public static extern byte Decimals();

    [Safe]
    public static extern string Symbol();

    [Safe]
    public static extern BigInteger TotalSupply();

    [Safe]
    public static extern BigInteger BalanceOf(UInt160 owner);

    public static extern bool Transfer(UInt160 from, UInt160 to, BigInteger amount, object data);

    #endregion

    #region Owner

    [Safe]
    public static extern UInt160 GetOwner();

    public static extern void SetOwner(UInt160 newOwner);

    #endregion

    #region Minter

    [Safe]
    public static extern UInt160 GetMinter();

    public static extern void SetMinter(UInt160 newMinter);

    #endregion

    #region Admin

    public static extern bool Withdraw(UInt160 to, BigInteger amount);

    public static extern void Burn(UInt160 account, BigInteger amount);

    #endregion
}
