// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Contract.Tests.Contracts;
using Neo.BlockchainToolkit;
using Neo.BlockchainToolkit.Models;
using Neo.BlockchainToolkit.SmartContract;
using Neo.IO;
using Neo.VM;
using NeoTestHarness;

namespace Contract.Tests;

[CheckpointPath("bin/checkpoints/contract-deployed.neoxp-checkpoint")]
public class UT_ExampleCoin : IClassFixture<CheckpointFixture<UT_ExampleCoin>>
{
    private readonly CheckpointFixture _checkpointFixture;
    private readonly ExpressChain _expressChain;

    public UT_ExampleCoin(CheckpointFixture<UT_ExampleCoin> fixture)
    {
        _checkpointFixture = fixture;
        _expressChain = fixture.FindChain();
    }

    [Fact]
    public void Test_GetOwner()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(e => e.getOwner());

        var ownerResult = engine.ResultStack.Pop();

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        Assert.NotNull(ownerResult);
        Assert.False(ownerResult.IsNull);
        Assert.Equal(ownerScriptHash.ToArray(), ownerResult.GetSpan().ToArray());
    }

    [Fact]
    public void Test_SetOwner()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(e => e.setOwner(ownerScriptHash));

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        Assert.Single(engine.Notifications);
        Assert.Equal("SetOwner", engine.Notifications[0].EventName);

        Assert.Single(engine.Notifications[0].State);
        Assert.Equal(ownerScriptHash.ToArray(), engine.Notifications[0].State[0].GetSpan().ToArray());
    }

    [Fact]
    public void Test_GetMinter()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(e => e.getMinter());

        var minterResult = engine.ResultStack.Pop();

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        Assert.NotNull(minterResult);
        Assert.False(minterResult.IsNull);
        Assert.Equal(ownerScriptHash.ToArray(), minterResult.GetSpan().ToArray());
    }

    [Fact]
    public void Test_SetMinter_Emit_Event()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");
        var aliceScriptHash = _expressChain.GetDefaultAccountScriptHash("alice");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(e => e.setMinter(aliceScriptHash));

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        Assert.Single(engine.Notifications);
        Assert.Equal("SetMinter", engine.Notifications[0].EventName);

        Assert.Single(engine.Notifications[0].State);
        Assert.Equal(aliceScriptHash.ToArray(), engine.Notifications[0].State[0].GetSpan().ToArray());
    }

    [Fact]
    public void Test_Mint_With_Owner_As_Sender()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");
        var aliceScriptHash = _expressChain.GetDefaultAccountScriptHash("alice");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(e => e.mint(aliceScriptHash, 1));

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        Assert.Single(engine.Notifications);
        Assert.Equal("Transfer", engine.Notifications[0].EventName);
        Assert.Equal(3, engine.Notifications[0].State.Count);
        Assert.True(engine.Notifications[0].State[0].IsNull);
        Assert.Equal(aliceScriptHash.ToArray(), engine.Notifications[0].State[1].GetSpan().ToArray());
        Assert.Equal(1, engine.Notifications[0].State[2].GetInteger());
    }

    [Fact]
    public void Test_Mint_With_Minter_As_Sender()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");
        var aliceScriptHash = _expressChain.GetDefaultAccountScriptHash("alice");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var ownerEngine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = ownerEngine.ExecuteScript<IExampleCoin>(e => e.setMinter(aliceScriptHash));

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, ownerEngine.State);

        using var aliceEngine = new TestApplicationEngine(snapshot, settings, aliceScriptHash);

        vmStateResult = aliceEngine.ExecuteScript<IExampleCoin>(e => e.mint(aliceScriptHash, 2));

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, aliceEngine.State);

        Assert.Single(aliceEngine.Notifications);
        Assert.Equal("Transfer", aliceEngine.Notifications[0].EventName);
        Assert.Equal(3, aliceEngine.Notifications[0].State.Count);
        Assert.True(aliceEngine.Notifications[0].State[0].IsNull);
        Assert.Equal(aliceScriptHash.ToArray(), aliceEngine.Notifications[0].State[1].GetSpan().ToArray());
        Assert.Equal(2, aliceEngine.Notifications[0].State[2].GetInteger());
    }

    [Fact]
    public void Test_Decimals()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(e => e.decimals());

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        var decimalsResult = engine.ResultStack.Pop();

        Assert.Equal(8, decimalsResult.GetInteger());
    }

    [Fact]
    public void Test_Symbol()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(e => e.symbol());

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        var symbolResult = engine.ResultStack.Pop();

        Assert.Equal("EXAMPLE", symbolResult.GetString());
    }

    [Fact]
    public void Test_TotalSupply()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(
            e => e.mint(ownerScriptHash, 1000),
            e => e.totalSupply());

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        var totalSupplyResult = engine.ResultStack.Pop();

        Assert.Equal(1000, totalSupplyResult.GetInteger());
    }

    [Fact]
    public void Test_BalanceOf()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(
            e => e.mint(ownerScriptHash, 1),
            e => e.balanceOf(ownerScriptHash));

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        var balanceResult = engine.ResultStack.Pop();

        Assert.Equal(1, balanceResult.GetInteger());
    }

    [Fact]
    public void Test_Transfer()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");
        var aliceScriptHash = _expressChain.GetDefaultAccountScriptHash("alice");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(
            e => e.mint(ownerScriptHash, 2),
            e => e.transfer(ownerScriptHash, aliceScriptHash, 1, null));

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        Assert.NotNull(engine.Notifications);
        Assert.Equal(2, engine.Notifications.Count);
        Assert.Equal("Transfer", engine.Notifications[1].EventName);
        Assert.Equal(3, engine.Notifications[1].State.Count);
        Assert.Equal(ownerScriptHash.ToArray(), engine.Notifications[1].State[0].GetSpan().ToArray());
        Assert.Equal(aliceScriptHash.ToArray(), engine.Notifications[1].State[1].GetSpan().ToArray());
        Assert.Equal(1, engine.Notifications[1].State[2].GetInteger());

        var transferResult = engine.ResultStack.Pop();

        Assert.True(transferResult.GetBoolean());
    }

    [Fact]
    public void Test_Burn()
    {
        var settings = _expressChain.GetProtocolSettings();
        var ownerScriptHash = _expressChain.GetDefaultAccountScriptHash("owner");

        using var snapshot = _checkpointFixture.GetSnapshot();

        using var engine = new TestApplicationEngine(snapshot, settings, ownerScriptHash);

        var vmStateResult = engine.ExecuteScript<IExampleCoin>(
            e => e.mint(ownerScriptHash, 1000),
            e => e.burn(ownerScriptHash, 50));

        Assert.Equal(VMState.HALT, vmStateResult);
        Assert.Equal(VMState.HALT, engine.State);

        Assert.NotNull(engine.Notifications);
        Assert.Equal(2, engine.Notifications.Count);
        Assert.Equal("Transfer", engine.Notifications[1].EventName);
        Assert.Equal(3, engine.Notifications[1].State.Count);
        Assert.Equal(ownerScriptHash.ToArray(), engine.Notifications[1].State[0].GetSpan().ToArray());
        Assert.True(engine.Notifications[1].State[1].IsNull);
        Assert.Equal(50, engine.Notifications[1].State[2].GetInteger());
    }
}
