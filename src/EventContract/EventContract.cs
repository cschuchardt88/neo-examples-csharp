// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

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
    public delegate void OnSayHelloDelegate(string message);

    [DisplayName("SayHello1")]
    public static event OnSayHelloDelegate OnSayHello;

    [DisplayName("SayHello2")]
    public static event Action<string> SayHello;

    public static void Main()
    {
        OnSayHello("Hello, alice");

        SayHello("Hello, bob");

        Runtime.Notify("SayHello3", new[] { "Hello, joe" });
    }
}
