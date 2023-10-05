// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Contract.Tests.Contracts;
using Neo.BlockchainToolkit;
using Neo.BlockchainToolkit.Models;
using Neo.BlockchainToolkit.SmartContract;
using Neo.VM;
using NeoTestHarness;

namespace Contract.Tests;

[CheckpointPath("bin/checkpoints/contract-deployed.neoxp-checkpoint")]
public class UT_EventContract : IClassFixture<CheckpointFixture<UT_EventContract>>
{
    private readonly CheckpointFixture _checkpointFixture;
    private readonly ExpressChain _expressChain;

    public UT_EventContract(CheckpointFixture<UT_EventContract> fixture)
    {
        _checkpointFixture = fixture;
        _expressChain = fixture.FindChain();
    }

    [Fact]
    public void Test_Main()
    {
        var settings = _expressChain.GetProtocolSettings();
        var aliceScriptHash = _expressChain.GetDefaultAccountScriptHash("alice");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, aliceScriptHash);

        var vmStateResult = engine.ExecuteScript<IEventContract>(e => e.main());

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        Assert.NotNull(engine.Notifications);
        Assert.Equal(3, engine.Notifications.Count);

        Assert.Equal("SayHello1", engine.Notifications[0].EventName);
        Assert.Equal("SayHello2", engine.Notifications[1].EventName);
        Assert.Equal("SayHello3", engine.Notifications[2].EventName);
    }
}
