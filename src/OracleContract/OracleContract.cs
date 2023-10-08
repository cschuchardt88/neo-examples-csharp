// Copyright (C) 2023 Christopher R Schuchardt
//
// The neo-examples-csharp is free software distributed under the
// MIT software license, see the accompanying file LICENSE in
// the main directory of the project for more details.

using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Attributes;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;
using System;
using System.ComponentModel;

namespace OracleContract;

[DisplayName("OracleContract")]
[ManifestExtra("Author", "neo.events")]
[ManifestExtra("Description", "Oracle Request example")]
[ManifestExtra("Email", "examples@neo.events")]
[ManifestExtra("Website", "https://www.neo.events/")]
[ManifestExtra("Version", "1.0.0")]
[ContractSourceCode("https://github.com/cschuchardt88/neo-examples-csharp")]
[ContractPermission("*", "*")]
public class OracleContract : SmartContract
{
    public delegate void OnRequestSuccessfulDelegate(string requestedUrl, object jsonValue);

    [DisplayName("RequestSuccessful")]
    public static event OnRequestSuccessfulDelegate OnRequestSuccessful;

    public static void DoRequest()
    {
        /*
            JSON DATA
            {
                "id": "6520ad3c12a5d3765988542a",
                "record": {
                    "propertyName": "Hello World!"
                },
                "metadata": {
                    "name": "HelloWorld",
                    "readCountRemaining": 98,
                    "timeToExpire": 86379,
                    "createdAt": "2023-10-07T00:58:36.746Z"
                }
            }
            See JSONPath format at https://github.com/atifaziz/JSONPath
            JSONPath = "$.record.propertyName"
            ReturnValue = ["Hello World!"]
            ReturnValueType = string array
        */
        var requestUrl = "https://api.jsonbin.io/v3/qs/6520ad3c12a5d3765988542a";
        Oracle.Request(requestUrl, "$.record.propertyName", "onOracleResponse", null, Oracle.MinimumResponseFee);
    }

    public static void OnOracleResponse(string requestedUrl, object userData, OracleResponseCode oracleResponse, string jsonString)
    {
        if (Runtime.CallingScriptHash != Oracle.Hash)
            throw new InvalidOperationException("No Authorization!");
        if (oracleResponse != OracleResponseCode.Success)
            throw new Exception("Oracle response failure with code " + (byte)oracleResponse);

        var jsonArrayValues = (string[])StdLib.JsonDeserialize(jsonString);
        var jsonFirstValue = jsonArrayValues[0];

        OnRequestSuccessful(requestedUrl, jsonFirstValue);
    }

    [DisplayName("_deploy")]
    public static void OnDeployment(object data, bool update)
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
