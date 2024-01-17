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

namespace BannedSymbols;

[DisplayName("StorageContract")]
[ManifestExtra("Author", "neo.events")]
[ManifestExtra("Description", "Simple Smart Contract Example")]
[ManifestExtra("Email", "examples@neo.events")]
[ManifestExtra("Website", "https://www.neo.events/")]
[ManifestExtra("Version", "1.0.0")]
[ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp")]
[ContractPermission("*", "*")]
public class BannedSymbolsClass : SmartContract
{
    private const byte Prefix_Hello = 0x00;

    public static void Main()
    {
        short i = 0;
        int i1 = 0;
        long i2 = 0;
        Int128 i3 = 0;
        ushort i4 = 0;
        uint i5 = 0;
        ulong i6 = 0;
        UInt128 i7 = 0;
        bool i8 = false;

        string str1 = i.ToString();
        string str2 = i1.ToString();
        string str3 = i2.ToString();
        string str4 = i3.ToString();
        string str5 = i4.ToString();
        string str6 = i5.ToString();
        string str7 = i6.ToString();
        string str8 = i7.ToString();
        string str9 = i8.ToString();
    }
}
