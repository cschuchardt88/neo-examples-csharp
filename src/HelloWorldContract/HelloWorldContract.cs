// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Native;
using System.ComponentModel;

namespace HelloWorldContract;

[DisplayName("HelloWorldContract")]
[ManifestExtra("Author", "neo.events")]
[ManifestExtra("Description", "Simple Smart Contract Example")]
[ManifestExtra("Email", "examples@neo.events")]
[ManifestExtra("Website", "https://www.neo.events/")]
[ManifestExtra("Version", "1.0.0")]
[ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp")]
[ContractPermission("*", "*")]
public class HelloWorldContract : SmartContract
{
    [Safe]
    public static string SayHello(string name)
    {
        return "Hello, " + name;
    }

    public static void _deploy(object data, bool update)
    {
        if (update)
            return;
    }

    public static bool Update(ByteString nefFile, string manifest)
    {
        ContractManagement.Update(nefFile, manifest);
        return true;
    }

    public static bool Destroy()
    {
        ContractManagement.Destroy();
        return true;
    }
}
