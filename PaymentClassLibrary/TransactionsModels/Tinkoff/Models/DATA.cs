using PaymentClassLibrary.Normalizer;
using PaymentClassLibrary.Utils;
using PaymentClassLibrary.Utils.TypeModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaymentClassLibrary.TransactionsModels.Tinkoff
{
    /// <summary>
    /// Соглашение с МФО
    /// </summary>
    public class MfoAgreementData : IModel
    {
        /// <summary>
        /// Обычно передаем идентификатор клиента
        /// </summary>
        [JsonProperty("mfoAgreement")]
        [Display(Name = "mfoAgreement")]
        public string MfoAgreement { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            var dataBrowser = new StandartModel();

            return new SimpleNormalizerParameters().GetNormalizedParameters(dataBrowser.GetData(this));
        }
    }

    /// <summary>
    /// Назначение платежа
    /// </summary>
    public class PaymentPurposeDetailsData : IModel
    {
        /// <summary>
        /// Здесь передаем номер договора
        /// </summary>
        [JsonProperty("paymentPurposeDetails")]
        [Display(Name = "paymentPurposeDetails")]
        public string PaymentPurposeDetails { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override string ToString()
        {
            var dataBrowser = new StandartModel();

            return new SimpleNormalizerParameters().GetNormalizedParameters(dataBrowser.GetData(this));
        }
    }
}