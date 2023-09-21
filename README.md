# Test usługi SOAP Webfleet
[Dokumentacja API WEBFLEET.connect](https://www.webfleet.com/static/help/webfleet-connect/en_gb/index.html)

Klient usługi wygenerowany z [WSDL](https://soap.webfleet.com/v1.33/addressService?wsdl).

Testowane są dwie wersje bibliotek .NET w Soneta.SDK.

### Konfiguracja ServiceModel4 (dołączone do enova365 2310.0.0-netbeta.105874)
* `System.ServiceModel.Http 4.8.1`
* `System.ServiceModel.Primitive 4.9.0`
### Konfiguracja ServiceModel6 (najnowsze)
* `System.ServiceModel.Http 6.0.0`
* `System.ServiceModel.Primitive 6.0.0`

## Jak uruchomić
```powershell
dotnet test -c ServiceModel6
dotnet test -c ServiceModel4
```

## Wyniki
Konfiguracja ServiceModel6 przechodzi test bez błędów.

Konfiguracja ServiceModel4 wyrzuca błąd:
```log
  Failed TestInvalidApiKey [696 ms]
  Error Message:
     Expected: <Soap12Webfleet.ServiceQueryException>
  But was:  <System.ServiceModel.FaultException: Fault occurred while processing. Request intercepted and blocked by application security manager (request id = 12110123186432548434)
   at System.ServiceModel.Channels.ServiceChannel.HandleReply(ProxyOperationRuntime operation, ProxyRpc& rpc)
   at System.ServiceModel.Channels.ServiceChannel.EndCall(String action, Object[] outs, IAsyncResult result)
   at System.ServiceModel.Channels.ServiceChannelProxy.TaskCreator.<>c__DisplayClass1_0.<CreateGenericTask>b__0(IAsyncResult asyncResult)
--- End of stack trace from previous location ---
   at Soap12Webfleet.Tests.SoapTests.<>c__DisplayClass4_0.<<TestInvalidApiKey>b__0>d.MoveNext() in E:\HW\Soap12Webfleet\Soap12Webfleet\Soap12Webfleet.Tests\SoapTests.cs:line 42
--- End of stack trace from previous location ---
   at NUnit.Framework.Internal.TaskAwaitAdapter.GenericAdapter`1.BlockUntilCompleted()
   at NUnit.Framework.Internal.MessagePumpStrategy.NoMessagePumpStrategy.WaitForCompletion(AwaitAdapter awaiter)
   at NUnit.Framework.Internal.AsyncToSyncAdapter.Await(Func`1 invoke)
   at NUnit.Framework.Assert.ThrowsAsync(IResolveConstraint expression, AsyncTestDelegate code, String message, Object[] args)>

  Stack Trace:
     at Soap12Webfleet.Tests.SoapTests.TestInvalidApiKey() in E:\HW\Soap12Webfleet\Soap12Webfleet\Soap12Webfleet.Tests\SoapTests.cs:line 40

1)    at Soap12Webfleet.Tests.SoapTests.TestInvalidApiKey() in E:\HW\Soap12Webfleet\Soap12Webfleet\Soap12Webfleet.Tests\SoapTests.cs:line 40
```

