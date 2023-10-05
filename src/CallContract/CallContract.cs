// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Services;
using System.ComponentModel;

namespace CallContract;

[DisplayName("CallContract")]
[ManifestExtra("Author", "neo.events")]
[ManifestExtra("Description", "How to Call a Contract")]
[ManifestExtra("Email", "examples@neo.events")]
[ManifestExtra("Website", "https://www.neo.events/")]
[ManifestExtra("Version", "1.0.0")]
[ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp")]
[ContractPermission("*", "*")]
public class CallContract : SmartContract
{
    public static void Main()
    {
        HelloWorldContract.SayHello(Runtime.CallingScriptHash);

        var balance = ExampleCoin.BalanceOf(Runtime.CallingScriptHash);
        Runtime.Log(Runtime.CallingScriptHash + " balance is " + balance);
    }
}
