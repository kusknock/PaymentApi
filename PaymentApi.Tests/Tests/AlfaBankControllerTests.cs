using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PaymentApi.Configuration;
using PaymentApi.Controllers;
using PaymentClassLibrary.TransactionsModels.AlfaBank;
using PaymentClassLibrary.Transport;
using PaymentClassLibrary.Utils;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Xunit;
using Xunit.Abstractions;

namespace PaymentApi.Tests
{
    public class AlfaBankControllerTests
    {
        private readonly AlfaBankController alfaBank;
        private readonly ITestOutputHelper output;
        private readonly AlfaBankSettings settings;

        public AlfaBankControllerTests(ITestOutputHelper _output)
        {
            settings = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, false)
                 .AddEnvironmentVariables()
                 .Build()
                 .GetSection("AlfaBankSettings")
                 .Get<AlfaBankSettings>();

            output = _output;

            alfaBank = new AlfaBankController(
                new GatewayClient(),
                Options.Create(settings));

        }

        [Fact]
        public void PreauthTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var data = new PreauthData();

            data.ControlKey = settings.MerchantControlKey;
            data.EndPointId = AlfaBankMethod.Methods.Preauth.EndPointId;
            data.ClientOrderId = "902B4F";
            data.OrderDescription = "Test Order Description";
            data.IpAddress = "65.153.12.232";
            data.FirstName = "John";
            data.LastName = "Smith";
            data.SSN = "1267";
            data.BirthDate = "19820115";
            data.Address = "100 Main st";
            data.City = "Seattle";
            data.State = "WA";
            data.ZipCode = "98102";
            data.Country = "US";
            data.Phone = "+12063582043";
            data.CellPhone = "+19023384543";
            data.Email = "john.smith@gmail.com";
            data.Amount = "3";
            data.Currency = "RUB";
            data.MerchantData = "VIPCustomer";
            data.ServerCallbackUrl = @"http://80.255.129.49:8080/AlfaBankCallback/CardRegistrationCallback?typeProcess=callback";
            data.RedirectUrlSuccess = "http://80.255.129.49:8080/AlfaBankCallback/CardRegistrationCallback?typeProcess=success";
            data.RedirectUrlFail = "http://80.255.129.49:8080/AlfaBankCallback/CardRegistrationCallback?typeProcess=fail";
            data.PreferredLanguage = "RU";

            var result = alfaBank.Preauth(data) as ObjectResult;

            var nvc = HttpUtility.ParseQueryString((result.Value as string).Replace("\n", ""));

            output.WriteLine(result.Value as string);

            Assert.False(nvc["type"] == "validation-error");
        }

        [Fact]
        public void StatusTest()
        {
            var query = @"type=async-response
                        &serial-number=00000000-0000-0000-0000-0000035a85f6
                        &merchant-order-id=902B4FF5
                        &paynet-order-id=2250815";

            var response = HttpUtility.ParseQueryString(query);

            var data = new StatusData()
            {
                ControlKey = settings.MerchantControlKey,
                Login = settings.ConsumerKey,
                MerchantOrderId = response["merchant-order-id"].Trim(),
                OrderId = response["paynet-order-id"].Trim(),
            };

            var statusResponse = alfaBank.Status(data, AlfaBankMethod.Methods.ReturnTransaction.ApiMethod) as ObjectResult;

            output.WriteLine(statusResponse.Value as string);

            var nvc = HttpUtility.ParseQueryString((statusResponse.Value as string));

            Assert.False(nvc["type"].Trim() == "validation-error");
        }

        // Перед тестом нужно вставить response после preauth в query
        [Fact]
        public void CardRefTest()
        {
            var query = string.Empty;
    //            @"type=async-form-response
    //&serial-number=00000000-0000-0000-0000-0000035a951a
    //&merchant-order-id=902B4F
    //&paynet-order-id=2250976";

            var statusResponse = Status(
                AlfaBankMethod.Methods.Preauth,
                query);

            output.WriteLine(statusResponse.ToQueryString());

            Assert.True(statusResponse["order-stage"].Trim() == "auth_approved");

            var cardRegData = new CardRegistrationData();

            cardRegData.ControlKey = settings.MerchantControlKey;
            cardRegData.Login = settings.ConsumerKey;
            cardRegData.ClientOrderId = statusResponse["merchant-order-id"].Trim();
            cardRegData.CardRegOrderId = statusResponse["paynet-order-id"].Trim();

            var cardRegTran = alfaBank.CreateCardRef(cardRegData) as ObjectResult;

            output.WriteLine(cardRegTran.Value as string);

            Assert.True(HttpUtility.ParseQueryString(cardRegTran.Value as string)["status"].Trim() == "approved");

            var retTranData = new RetrunTransactionData();

            retTranData.ControlKey = settings.MerchantControlKey;
            retTranData.Login = settings.ConsumerKey;
            retTranData.ClientOrderId = "902B4FF5";
            retTranData.OrderId = statusResponse["paynet-order-id"].Trim();
            retTranData.Comment = "Return money after card registration";

            var returnTran = alfaBank.Return(retTranData) as ObjectResult;

            output.WriteLine(returnTran.Value as string);

            statusResponse = Status(
                AlfaBankMethod.Methods.ReturnTransaction,
                returnTran.Value as string);

            output.WriteLine(statusResponse.ToQueryString());

            Assert.True(statusResponse["order-stage"].Trim() == "cancel_approved");
        }

        [Fact]
        public void TransferMoneyTest()
        {
            var data = new TransferCardData();

            data.Login = settings.ConsumerKey;
            data.MerchantOrderId = "902B123";
            data.CardRefId = "389797";
            data.OrderDescription = "Test Order Description";
            data.Amount = "1000";
            data.Currency = "RUB";
            data.IpAddress = "65.153.12.232";
            data.FirstName = "John";
            data.LastName = "Smith";
            data.MiddleName = "M";
            data.SSN = "1267";
            data.BirthDate = "19820115";
            data.Address = "100 Main st";
            data.City = "Seattle";
            data.State = "WA";
            data.ZipCode = "98102";
            data.Country = "US";
            data.Phone = "+12063582043";
            data.CellPhone = "+19023384543";
            data.Email = "john.smith@gmail.com";
            data.Purpose = "user_account1";
            data.MerchantData = "VIP Customer";

            data.ReceiverFirstName = "Jane";
            data.ReceiverMiddleName = "L";
            data.ReceiverLastName = "Doe";
            data.ReceiverPhone = "";
            data.ReceiverResident = "true";
            data.ReceiverDocSeries = "1111";
            data.ReceiverDocNumber = "222222";
            data.ReceiverDocId = "21";
            data.ReceiverAddress = "Red Sq, 1a";
            data.ReceiverCity = "Moscow";


            data.ControlKey = settings.MerchantControlKey;
            data.ServerCallbackUrl = @"http://80.255.129.49:8080/AlfaBankCallback/TransferMoneyCallback?typeProcess=callback";
            data.RedirectUrlSuccess = "http://80.255.129.49:8080/AlfaBankCallback/TransferMoneyCallback?typeProcess=success";
            data.RedirectUrlFail = "http://80.255.129.49:8080/AlfaBankCallback/TransferMoneyCallback?typeProcess=fail";

            var response = alfaBank.TransferMoney(data) as ObjectResult;

            output.WriteLine(response.Value as string);

            Task.Delay(2000);

            var status = Status(AlfaBankMethod.Methods.TransferMoney, response.Value as string);

            output.WriteLine(status.ToQueryString());

            Assert.True(status["order-stage"].Trim() == "transfer_approved");
        }

        [Fact]
        public void SaleFormTest()
        {
            var data = new SaleTranData();

            data.ControlKey = settings.MerchantControlKey;
            data.EndPointId = AlfaBankMethod.Methods.SaleForm.EndPointId;

            data.ClientOrderId = "902B4FF5";
            data.OrderDescription = "Test Order Description";
            data.IpAddress = "65.153.12.232";
            data.FirstName = "John";
            data.LastName = "Smith";
            data.SSN = "1267";
            data.BirthDate = "19820115";
            data.Address = "100 Main st";
            data.City = "Seattle";
            data.State = "WA";
            data.ZipCode = "98102";
            data.Country = "US";
            data.Phone = "+12063582043";
            data.CellPhone = "+19023384543";
            data.Email = "john.smith@gmail.com";
            data.Amount = "1000";
            data.Currency = "RUB";
            data.MerchantData = "VIPCustomer";

            data.ServerCallbackUrl = @"http://80.255.129.49:8080/AlfaBankCallback/SaleTransactionCallback?typeProcess=callback";
            data.RedirectUrlSuccess = "http://80.255.129.49:8080/AlfaBankCallback/SaleTransactionCallback?typeProcess=success";
            data.RedirectUrlFail = "http://80.255.129.49:8080/AlfaBankCallback/SaleTransactionCallback?typeProcess=fail";
            data.PreferredLanguage = "RU";

            var response = alfaBank.Sale(data) as ObjectResult;

            output.WriteLine(response.Value as string);

            var nvc = HttpUtility.ParseQueryString(response.Value as string);

            Assert.True(nvc["redirect-url"] is not null);
        }

        [Fact]
        public void ReccurentPayTest()
        {
            var data = new ReccurentTranData();

            data.ControlKey = settings.MerchantControlKey;

            data.Login = settings.ConsumerKey;
            data.ClientOrderId = "902B4FF5";
            data.CardRefId = "389797";
            data.OrderDescription = "Test Order";
            data.Amount = "1000";
            data.Currency = "RUB";
            data.IpAddress = "8.8.8.8";
            data.ServerCallbackUrl = @"http://80.255.129.49:8080/AlfaBankCallback/ReccurentTransactionCallback?typeProcess=callback";
            data.RedirectUrlSuccess = "http://80.255.129.49:8080/AlfaBankCallback/ReccurentTransactionCallback?typeProcess=success";
            data.RedirectUrlFail = "http://80.255.129.49:8080/AlfaBankCallback/ReccurentTransactionCallback?typeProcess=fail";
            //recTranData.ReccurentScenario = "";
            //recTranData.ReccurentIniciator = "";

            var response = alfaBank.Reccurent(data) as ObjectResult;

            output.WriteLine(response.Value as string);

            Task.Delay(5 * 1000);

            var status = Status(AlfaBankMethod.Methods.ReccurentPayment, response.Value as string);

            output.WriteLine(status.ToQueryString());

            Assert.True(status["status"].Trim() == "approved");
        }

        private NameValueCollection Status(AlfaBankMethod method, string query)
        {
            var response = HttpUtility.ParseQueryString(query);

            var data = new StatusData()
            {
                ControlKey = settings.MerchantControlKey,
                Login = settings.ConsumerKey,
                MerchantOrderId = response["merchant-order-id"].Trim(),
                OrderId = response["paynet-order-id"].Trim(),
            };

            var statusResponse = alfaBank.Status(data, method.ApiMethod) as ObjectResult;

            return HttpUtility.ParseQueryString(statusResponse.Value as string);
        }
    }
}
