// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

#pragma warning disable IDE1006

using System.ComponentModel;

namespace Contract.Tests.Contracts;

[Description("EventContract")]
internal interface IEventContract
{
    void main();
}
