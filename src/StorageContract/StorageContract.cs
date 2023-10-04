// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Services;
using System.ComponentModel;

namespace StorageContract;

[DisplayName("StorageContract")]
[ManifestExtra("Author", "neo.events")]
[ManifestExtra("Description", "Simple Smart Contract Example")]
[ManifestExtra("Email", "examples@neo.events")]
[ManifestExtra("Website", "https://www.neo.events/")]
[ManifestExtra("Version", "1.0.0")]
[ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp")]
[ContractPermission("*", "*")]
public class StorageContract : SmartContract
{
    private const byte Prefix_Hello = 0x00;

    public static void Main()
    {
        Storage.Put(new[] { Prefix_Hello }, "Hello World!");

        var text = Storage.Get(new[] { Prefix_Hello });

        StorageMap HelloMap = new(Prefix_Hello);

        HelloMap["Hello"] = "World!";

        var world = HelloMap["Hello"];
    }
}
