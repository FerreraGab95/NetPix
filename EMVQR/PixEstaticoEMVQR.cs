using emv_qrcps.QrCode.Merchant;
using NetPix.PaymentData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NetPix.EMVQR
{
    public class PixEstaticoEMVQR : IEMVQR
    {
        private const string MerchantAccountInfoGloballyUniqueIdentifier = "BR.GOV.BCB.PIX";
        private const string MerchantAccountPaymentNetworkSpecificID = "01";
        private const string MerchantAccountInformationID = "26";
        private const string PayLoadFormatIndicator = "01";
        private const string CountryCode = "BR";
        private const string MerchantCategoryCode = "0000";
        private const string TransactionCurrency = "986";
        private readonly PixEstaticoPaymentData paymentData;

        public PixEstaticoEMVQR(PixEstaticoPaymentData paymentData)
        {
            this.paymentData = paymentData;
        }


        public string GeneratePayload()
        {
            paymentData.Validate();

            var emvqr = new MerchantEMVQR();

            emvqr.SetPayloadFormatIndicator(PayLoadFormatIndicator);
            emvqr.SetCountryCode(CountryCode);
            emvqr.SetMerchantCategoryCode(MerchantCategoryCode);
            emvqr.SetTransactionCurrency(TransactionCurrency);

            var merchantAccountInformation = new MerchantAccountInformation();
            merchantAccountInformation.SetGloballyUniqueIdentifier(MerchantAccountInfoGloballyUniqueIdentifier);
            merchantAccountInformation.AddContextSpecificData(MerchantAccountPaymentNetworkSpecificID, paymentData.Chave);

            emvqr.AddMerchantAccountInformation(MerchantAccountInformationID, merchantAccountInformation);

            emvqr.SetMerchantName(paymentData.NomeBeneficiario);
            emvqr.SetMerchantCity(paymentData.CidadeBeneficiario);
            emvqr.SetTransactionAmount(paymentData.Valor.ToString("F2", CultureInfo.InvariantCulture));

            var additionalFieldTemplate = new AdditionalDataFieldTemplate();
            additionalFieldTemplate.SetReferenceLabel(string.IsNullOrEmpty(paymentData.CampoAdicional) ? 
                "***" : paymentData.CampoAdicional);

            emvqr.SetAdditionalDataFieldTemplate(additionalFieldTemplate);

            return emvqr.GeneratePayload();
        }
    }
}
