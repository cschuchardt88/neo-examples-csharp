// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Neo.BlockchainToolkit;
using Neo.BlockchainToolkit.Models;
using Neo.BlockchainToolkit.SmartContract;
using Neo.VM;
using NeoTestHarness;

namespace Contract.Tests;

[CheckpointPath("bin/checkpoints/contract-deployed.neoxp-checkpoint")]
public class UT_HelloWorldContract : IClassFixture<CheckpointFixture<UT_HelloWorldContract>>
{
    private readonly CheckpointFixture _checkpointFixture;
    private readonly ExpressChain _expressChain;

    public UT_HelloWorldContract(CheckpointFixture<UT_HelloWorldContract> fixture)
    {
        _checkpointFixture = fixture;
        _expressChain = fixture.FindChain();
    }

    [Fact]
    public void Test1()
    {
        var settings = _expressChain.GetProtocolSettings();
        var aliceAccount = _expressChain.GetDefaultAccount("alice");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, aliceAccount.ToScriptHash(settings.AddressVersion));

        var vmState = engine.ExecuteScript<IHelloWorldContract>(e => e.sayHello("alice"));

        Assert.Equal(VMState.HALT, vmState);
    }
}
