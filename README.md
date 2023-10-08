# neo-examples-csharp
Neo N3 blockchain C# examples. Including neo-cli plugins, RPC client and smart contracts.

## Requirements
- Dotnet 7.0

# Implements (In Examples)
- [neo 3.6.0](https://github.com/neo-project/neo/releases/tag/v3.6.0)
- [neo-node 3.6.0](https://github.com/neo-project/neo-node/releases/tag/v3.6.0)
- [RpcClient 3.6.1](https://github.com/neo-project/neo-modules/releases/tag/v3.6.1)
- [neo-devpack-dotnet 3.6.0](https://github.com/neo-project/neo-devpack-dotnet/releases/tag/v3.6.0)
- [neo-test 3.5.17](https://github.com/N3developertoolkit/neo-test/releases/tag/3.5.17)
- [neo-express 3.5.20](https://github.com/neo-project/neo-express/releases/tag/3.5.20)

# What's to come
- [ ] Tutorials on Plugins, RpcClient and Smart Contracts
- [ ] `neo-cli` node Plugin examples
- [ ] `RpcClient` examples
- [ ] Nep-11 smart contract example
- [x] Nep-17 smart contract example
- [x] Emit events example (smart contract)
- [x] Smart contract basic storage example
- [x] Calling smart contract example
- [x] Test smart contract example

# Smart Contract Examples
- [Basic Contract (Beginner)](/src/HelloWorldContract/HelloWorldContract.cs)
- [Emit Events (Beginner)](/src/EventContract/EventContract.cs)
- [Using Storage and StorageMaps (Beginner)](/src/StorageContract/StorageContract.cs)
- [Calling deployed contracts (Beginner/Advanced)](/src/CallContract)
- [NEP-17 (Beginner/Advanced)](/src/ExampleCoin/ExampleCoin.cs)
- [Build Smart Contract Tests](/tests/Contract.Tests)

# Build Repository
**In a `Terminal` type:**
```
tux@PC01:~/Downloads$ git clone https://github.com/cschuchardt88/neo-examples-csharp.git
tux@PC01:~/Downloads$ cd neo-examples-csharp
tux@PC01:~/Downloads/neo-examples-csharp$ dotnet tool restore
tux@PC01:~/Downloads/neo-examples-csharp$ dotnet build All.sln
```
