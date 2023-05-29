﻿using PaymentClassLibrary.Signature.SignatureCreators;
using PaymentClassLibrary.SignatureMethods;
using PaymentClassLibrary.SignatureModels;
using PaymentClassLibrary.TransactionsModels;
using PaymentClassLibrary.Transport.Requests;
using PaymentClassLibrary.Transport.Responses;
using PaymentClassLibrary.Utils;
using System;

namespace PaymentClassLibrary.Queries
{
    /// <summary>
    /// Формирование запроса для транзакции с терминала EACQ
    /// </summary>
    public class TinkoffPayQuery : Query
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="signatureCreator"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public TinkoffPayQuery(ISignatureCreator signatureCreator) : base(signatureCreator)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="paymentTransaction"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override IRequest CreateRequest(IPaymentTransaction paymentTransaction)
        {
            return base.CreateRequest(paymentTransaction);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="paymentTransaction"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        protected override IModel CreateSignature(IPaymentTransaction paymentTransaction)
        {
            return signatureCreator.CreateSignature(paymentTransaction.GetSignatureData());
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="paymentTransaction"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        protected override IRequest PaymentTransactionToRequest(IPaymentTransaction paymentTransaction)
        {
            return paymentTransaction.GetRequest();
        }
    }
}
