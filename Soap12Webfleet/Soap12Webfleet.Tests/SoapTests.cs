using NUnit.Framework;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WebfleetAddressService;

namespace Soap12Webfleet.Tests {
    [TestFixture]
    public static class SoapTests {
        private const int maxMessageSize = 0x1000000; // 16 MiB
        private const string apiUrl = "https://soap.webfleet.com/v1.33/addressService?wsdl";

        private static readonly GeneralParameters gParm = new() {
            locale = KnownLocales.PL,
            timeZone = KnownTimeZones.Europe_Warsaw,
        };

        private static readonly addressClient client = new(new CustomBinding(
            new MtomMessageEncodingBindingElement {
                MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None),
                MaxBufferSize = maxMessageSize,
            },
            new HttpsTransportBindingElement { MaxReceivedMessageSize = maxMessageSize }),
            new EndpointAddress(apiUrl));

        [SetUp]
        public static void SetUp() {
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
        }

        [Test]
        public static void TestInvalidApiKey() {
            const int invalidApiKeyStatusCode = 1143;

            var aParm = new AuthenticationParameters {
                accountName = "test",
                userName = "test",
                password = "test",
                apiKey = "test",
            };

            var emptyFilter = new FilterParameter { filterCriterion = "" };

            Assert.AreEqual(invalidApiKeyStatusCode,
                Assert.ThrowsAsync<ServiceQueryException>(async () =>
                    (await client.showAddressGroupReportAsync(aParm, gParm, emptyFilter)).@return.GetResults<AddressGroup>())
                        .StatusCode);
        }
    }
}
