# neo-examples-csharp
Neo N3 blockchain C# examples. Including node plugins, RPC client and smart contracts.

# Requirements
- Dotnet 7.0+
- Visual Studio 2022 Version 17.7.4

# Build
**In a `Terminal` type:**
```
tux@PC01:~/Downloads$ git clone https://github.com/cschuchardt88/neo-examples-csharp.git
tux@PC01:~/Downloads$ cd neo-examples-csharp
tux@PC01:~/Downloads/neo-examples-csharp$ dotnet build All.sln
```


# Smart Contract Examples
- [Contract Start (Beginner)](/src/HelloWorldContract/HelloWorldContract.cs)
- [Emit Events (Beginner)](/src/EventContract/EventContract.cs)
- [Using Storage and StorageMaps (Beginner)](/src/StorageContract/StorageContract.cs)
- [Calling deployed contracts (Beginner/Advanced)](/src/CallContract)
- [NEP-17 (Beginner/Advanced)](/src/ExampleCoin/ExampleCoin.cs)
