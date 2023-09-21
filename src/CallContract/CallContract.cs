// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using System.ComponentModel;

namespace CallContracts;

[DisplayName("MyBasicContract")]
[ManifestExtra("Author", "neo.events")]
[ManifestExtra("Description", "How to Call a Contract")]
[ManifestExtra("Email", "examples@neo.events")]
[ManifestExtra("Website", "https://www.neo.events/")]
[ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp")]
public class CallContract : SmartContract
{
    public static void Main()
    {
        HelloWorldContract.SayHello("Bob");
    }
}
