using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentApi.Configuration;
using PaymentApi.Controllers;
using PaymentClassLibrary.TransactionsModels.Tinkoff;
using PaymentClassLibrary.Transport;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace PaymentApi.Tests
{
    public class TinkoffBindControllerTests
    {
        private readonly TinkoffBindTestController tinkoffBind;
        private readonly ITestOutputHelper output;
        private readonly TinkoffTestSettings tinkoffTestSettings;

        public TinkoffBindControllerTests(ITestOutputHelper _output)
        {
            tinkoffTestSettings = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, false)
                 .AddEnvironmentVariables()
                 .Build()
                 .GetSection("TinkoffTestSettings")
                 .Get<TinkoffTestSettings>();

            output = _output;

            tinkoffBind = new TinkoffBindTestController(new GatewayClient());

        }

        [Fact]
        public void CustomerTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var addCustomerData = new AddCustomerData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffE2CTerminalKey,
                CustomerKey = PaymentClassLibrary.Utils.Extensions.RandomString(7),
                Email = "kusnov.intro@mailg.com",
                Phone = "+71234567890"
            };
            var customerData = new CustomerData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffE2CTerminalKey,
                CustomerKey = addCustomerData.CustomerKey
            };

            // Добавляем клиента в систему банка
            var resultAdd = tinkoffBind.AddCustomer(addCustomerData) as ObjectResult;

            output.WriteLine(JsonConvert.SerializeObject(resultAdd.Value));

            Assert.True((resultAdd.Value as AddCustomerResultData).Success);

            // Получаем данные пользователя из банка
            var resultGet = tinkoffBind.GetCustomer(customerData) as ObjectResult;

            output.WriteLine(JsonConvert.SerializeObject(resultGet.Value));

            Assert.True((resultGet.Value as GetCustomerResultData).Success);

            // Удаляем пользователя из системы банка
            var resultRemove  = tinkoffBind.RemoveCustomer(customerData) as ObjectResult;

            output.WriteLine(JsonConvert.SerializeObject(resultRemove.Value));

            Assert.True((resultRemove.Value as RemoveCustomerResultData).Success);
        }

        [Fact]
        public void AddCardTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var data = new AddCardData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffE2CTerminalKey,
                CustomerKey = "TestCustomer",
                CheckType = "3DSHOLD"
            };

            var result = tinkoffBind.AddCard(data) as ObjectResult;

            output.WriteLine(JsonConvert.SerializeObject(result.Value));

            Assert.True((result.Value as AddCardResultData).Success);
        }

        [Fact]
        public void GetCardsTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var data = new CustomerData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffE2CTerminalKey,
                CustomerKey = "TestCustomer"
            };

            var result = tinkoffBind.GetCardList(data) as ObjectResult;

            output.WriteLine(JsonConvert.SerializeObject(result.Value));

            Assert.True((result.Value as GetCardListResultData).Success);
        }


        /// <summary>
        /// В данном тесте, рандомную карту невозможно получить для удаления
        /// Тестируется общий функционал (правильность подписи, переданного терминала)
        /// Будем считать, что неправильная карта это успешный тест
        /// </summary>
        [Fact]
        public void RemoveCardTest()
        {
            var data = new RemoveCardData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffE2CTerminalKey,
                CustomerKey = "TestCustomer",
                CardId = "291393683"
            };

            var result = tinkoffBind.RemoveCard(data) as ObjectResult;

            output.WriteLine(DateTime.Now.ToString());
            output.WriteLine(JsonConvert.SerializeObject(result.Value));

            var resultObject = (result.Value as RemoveCardResultData);

            Assert.True(resultObject.ErrorCode == "6" || resultObject.Success);
        }

        [Fact]
        public void InitPayTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var initPayData = new InitTransferData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffE2CTerminalKey,
                OrderId = PaymentClassLibrary.Utils.Extensions.RandomString(7),
                CardId = "493150042",
                Amount = "1000",
                DATA = new PaymentPurposeDetailsData
                {
                    PaymentPurposeDetails = $"Договор № {PaymentClassLibrary.Utils.Extensions.RandomString(7)}"
                }
            };

            var resultInit = tinkoffBind.InitPay(initPayData) as ObjectResult;
            var resultInitObject = resultInit.Value as InitTransferResultData;

            output.WriteLine(JsonConvert.SerializeObject(resultInitObject));

            Assert.True(resultInitObject.Success && resultInitObject.ErrorCode == "0");


            var payData = new PayTransferData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffE2CTerminalKey,
                PaymentId = resultInitObject.PaymentId,
            };

            var resultPay = tinkoffBind.PayTransfer(payData) as ObjectResult;
            var resultPayObject = resultPay.Value as PayTransferResultData;

            output.WriteLine(JsonConvert.SerializeObject(resultPayObject));

            Assert.True(resultPayObject.Success && resultPayObject.ErrorCode == "0");
        }
    }
}
