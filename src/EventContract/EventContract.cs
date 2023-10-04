// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Services;
using System;
using System.ComponentModel;

namespace EventContract;

[DisplayName("EventContract")]
[ManifestExtra("Author", "neo.events")]
[ManifestExtra("Description", "Simple Smart Contract Example")]
[ManifestExtra("Email", "examples@neo.events")]
[ManifestExtra("Website", "https://www.neo.events/")]
[ManifestExtra("Version", "1.0.0")]
[ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp")]
[ContractPermission("*", "*")]
public class EventContract : SmartContract
{
    public delegate void OnSayHelloDelegate(UInt160 address);

    [DisplayName("SayHello")]
    public static event OnSayHelloDelegate OnSayHello;

    [DisplayName("SayHelloAgain")]
    public static event Action<UInt160> SayHello;

    public static void Main()
    {
        OnSayHello(Runtime.CallingScriptHash);

        Runtime.Notify("SayHello", new[] { Runtime.CallingScriptHash });

        SayHello(Runtime.CallingScriptHash);
    }
}
