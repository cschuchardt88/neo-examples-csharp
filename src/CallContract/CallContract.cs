using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using System.ComponentModel;

namespace CallContracts;

[DisplayName("MyBasicContract")]
[ManifestExtra("Author", "neo.events")]
[ManifestExtra("Email", "YourName@example.com")]
[ManifestExtra("Description", "How to Call a Contract")]
[ManifestExtra("Email", "examples@neo.events")]
[ManifestExtra("Website", "https://www.neo.events/")]
[ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp")]
public class CallContract : SmartContract
{
    public static void Main(UInt160[] args)
    {
        var supply = MyNep17Contract.TotalSupply();
        MyNep17Contract.Mint(args[0], supply);
    }
}
