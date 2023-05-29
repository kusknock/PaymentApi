using Microsoft.Extensions.Configuration;
using PaymentApi.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace PaymentApi.Tests.Tests
{
    public class X509Tests
    {
        private readonly ITestOutputHelper output;
        private readonly TinkoffTestSettings tinkoffTestSettings;

        public X509Tests(ITestOutputHelper _output)
        {
            tinkoffTestSettings = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, false)
                 .AddEnvironmentVariables()
                 .Build()
                 .GetSection("TinkoffTestSettings")
                 .Get<TinkoffTestSettings>();

            output = _output;
        }

        [Fact]
        void PrivateKeyTests()
        {
            var cert1 = new X509Certificate2(tinkoffTestSettings.PfxPath, tinkoffTestSettings.PfxPassword, X509KeyStorageFlags.Exportable);
            RSACryptoServiceProvider key1 = new RSACryptoServiceProvider();
            key1.FromXmlString(cert1.PrivateKey.ToXmlString(true));

            var myStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            myStore.Open(OpenFlags.ReadOnly);
            X509Certificate2 cert2 = myStore.Certificates.Find(X509FindType.FindBySerialNumber, tinkoffTestSettings.TinkoffCertSerial, true)[0];

            RSACryptoServiceProvider key2 = new RSACryptoServiceProvider();
            key2.FromXmlString(cert2.PrivateKey.ToXmlString(true));

            output.WriteLine(cert1.GetSerialNumberString() + "/" + cert2.GetSerialNumberString());

            Assert.True(cert1.Equals(cert2));
            Assert.Equal(cert1, cert2);

            byte[] signedHashValue1 = key1.SignData(Convert.FromBase64String("stringstringstringstringstringstringstringstringstringstringstringstringstringstring"),
                CryptoConfig.MapNameToOID("SHA256"));

            string base64StringHash1 = Convert.ToBase64String(signedHashValue1);

            byte[] signedHashValue2 = key2.SignData(Convert.FromBase64String("stringstringstringstringstringstringstringstringstringstringstringstringstringstring"),
                CryptoConfig.MapNameToOID("SHA256"));

            string base64StringHash2 = Convert.ToBase64String(signedHashValue2);

            Assert.Equal(base64StringHash1, base64StringHash2);
        }
    }
}
