// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using System.ComponentModel;

namespace Contract.Tests;

[Description("HelloWorldContract")]
internal interface HelloWorldContract
{
    string sayHello(string name);
    void _deploy(object data, bool update);
    bool update(byte[] nefFile, string manifest);
    bool destroy();
}
