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
    public class TinkoffPayControllerTests
    {
        private readonly TinkoffPayTestController tinkoffPay;
        private readonly ITestOutputHelper output;
        private readonly TinkoffTestSettings tinkoffTestSettings;

        public TinkoffPayControllerTests(ITestOutputHelper _output)
        {
            tinkoffTestSettings = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, false)
                 .AddEnvironmentVariables()
                 .Build()
                 .GetSection("TinkoffTestSettings")
                 .Get<TinkoffTestSettings>();

            output = _output;

            tinkoffPay = new TinkoffPayTestController(new GatewayClient());

        }

        [Fact]
        public void InitPaymentFormTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var data = new InitPaymentData()
            {
                Amount = "1000",
                OrderId = PaymentClassLibrary.Utils.Extensions.RandomString(7),
                TerminalKey = tinkoffTestSettings.TinkoffTerminalKey,
                Password = tinkoffTestSettings.TinkoffTerminalPassword,
                PayForm = "mfo",
                PayType = "O",
                DATA = new MfoAgreementData { MfoAgreement = "2660522" }
            };

            // Делаем запрос на оплату через платежную форму
            var result = tinkoffPay.InitPayment(data) as ObjectResult;

            output.WriteLine(JsonConvert.SerializeObject(result.Value));

            Assert.True((result.Value as InitPaymentResultData).Success);
        }

        [Fact]
        public void PayWithRebillIdTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var initRebillData = new InitRebillData()
            {
                Amount = "1000",
                OrderId = PaymentClassLibrary.Utils.Extensions.RandomString(7),
                TerminalKey = tinkoffTestSettings.TinkoffRebillTerminalKey,
                Password = tinkoffTestSettings.TinkoffRebillTerminalPassword,
                PayType = "O",
                DATA = new MfoAgreementData { MfoAgreement = "266052" }
            };

            var resultInit = tinkoffPay.InitRebill(initRebillData) as ObjectResult;
            var resultInitValue = resultInit.Value as InitRebillResultData;

            output.WriteLine(JsonConvert.SerializeObject(resultInitValue));

            Assert.True(resultInitValue.Success && resultInitValue.Status == "NEW");

            var chargeRebillData = new ChargeRebillData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffRebillTerminalKey,
                Password = tinkoffTestSettings.TinkoffRebillTerminalPassword,
                RebillId = "3258888283",
                PaymentId = resultInitValue.PaymentId,
            };

            var resultCharge = tinkoffPay.ChargeRebill(chargeRebillData) as ObjectResult;
            var resultChargeValue = resultCharge.Value as ChargeRebillResultData;

            output.WriteLine(JsonConvert.SerializeObject(resultChargeValue));

            Assert.True(resultChargeValue.Success && resultChargeValue.Status == "CONFIRMED");
        }

        [Fact]
        public void GetStatePaymentTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var data = new GetStatePaymentData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffRebillTerminalKey,
                Password = tinkoffTestSettings.TinkoffRebillTerminalPassword,
                PaymentId = "4517558907",
            };

            var result = tinkoffPay.GetState(data) as ObjectResult;

            var state = result.Value as GetStateResultData;

            output.WriteLine(JsonConvert.SerializeObject(state));

            Assert.True(state.Success);
        }

        [Fact]
        public void GetAddCardStateTest()
        {
            output.WriteLine(DateTime.Now.ToString());

            var data = new GetAddStateCardData()
            {
                TerminalKey = tinkoffTestSettings.TinkoffRebillTerminalKey,
                Password = tinkoffTestSettings.TinkoffRebillTerminalPassword,
                RequestKey = "1a15bd9a-aff6-4176-9fac-0e81748bfe92",
            };

            var result = tinkoffPay.GetAddCardState(data) as ObjectResult;

            var state = result.Value as GetAddCardStateResultData;

            output.WriteLine(JsonConvert.SerializeObject(state));

            Assert.True(state.Success);
        }
    }
}
