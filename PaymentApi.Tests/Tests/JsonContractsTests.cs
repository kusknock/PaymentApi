using Newtonsoft.Json;
using PaymentClassLibrary.Signature.SignatureAttributes;
using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Utils.CustomContractResolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentApi.Tests
{
    public class JsonContractsTests
    {
        [Fact]
        public void JsonIgnoreSignatureFieldWithResolverTests()
        {
            var initPaymentData = new InitPaymentData
            {
                Amount = "1000",
                OrderId = "test",
                PayForm = "test",
                PayType = "test",
                DATA = new MfoAgreementData
                {
                    MfoAgreement = "test"
                },
                TerminalKey = "test",
                Password = "test"
            };

            var requestData = RequestData.Serializer.SerializeJsonData(initPaymentData, new ExcluderPropertiesResolver<JsonIgnoreForSignature>());

            var data = JsonConvert.DeserializeObject<InitPaymentData>(requestData.RequestString);

            Assert.Null(data.Password);
        }

        [Fact]
        public void JsonIgnoreSignatureFieldWithoutResolverTests()
        {
            var initPaymentData = new InitPaymentData
            {
                Amount = "test",
                OrderId = "test",
                PayForm = "test",
                PayType = "test",
                DATA = new MfoAgreementData
                {
                    MfoAgreement = "test"
                },
                TerminalKey = "test",
                Password = "test"
            };

            var requestData = RequestData.Serializer.SerializeJsonData(initPaymentData);

            var data = JsonConvert.DeserializeObject<InitPaymentData>(requestData.RequestString);

            Assert.NotNull(data.Password);
        }
    }
}
