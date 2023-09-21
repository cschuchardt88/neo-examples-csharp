// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Neo;
using Neo.SmartContract.Framework.Attributes;

#pragma warning disable CS0626

namespace CallContracts;

[Contract("0xb0674a6ee70ff7c86ab6287a63697d6d445efba7")]
public class HelloWorldContract
{
    [ContractHash]
    public static extern UInt160 Hash { get; }

    public static extern string SayHello(string name);
}
