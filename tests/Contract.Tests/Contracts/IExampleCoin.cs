// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

#pragma warning disable IDE1006

using Neo;
using System.ComponentModel;
using System.Numerics;

namespace Contract.Tests.Contracts;

[Description("ExampleCoin")]
internal interface IExampleCoin
{
    UInt160 getOwner();
    void setOwner(UInt160 newOwner);
    UInt160 getMinter();
    void setMinter(UInt160 newMinter);
    void mint(UInt160 to, BigInteger amount);
    byte decimals();
    string symbol();
    BigInteger totalSupply();
    BigInteger balanceOf(UInt160 owner);
    bool transfer(UInt160 from, UInt160 to, BigInteger amount, object data);
    void burn(UInt160 account, BigInteger amount);
    bool withdraw(UInt160 to, BigInteger amount);
    void onNEP17Payment(UInt160 from, BigInteger amount, object data);
    bool verify();
    bool update(byte[] nefFile, string manifest);
}
