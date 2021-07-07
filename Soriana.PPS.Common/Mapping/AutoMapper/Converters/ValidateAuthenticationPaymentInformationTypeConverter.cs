using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Salesforce;
using System.Linq;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class ValidateAuthenticationPaymentInformationTypeConverter : ITypeConverter<PaymentOrderProcessRequest, Riskv1authenticationresultsPaymentInformation>
    {
        #region Public Methods
        public Riskv1authenticationresultsPaymentInformation Convert(PaymentOrderProcessRequest source, Riskv1authenticationresultsPaymentInformation destination, ResolutionContext context)
        {
            if (source == null) return destination;
            string mappingSource = (source.PaymentToken.Contains(CharactersConstants.HYPHEN_CHAR)) ? source.PaymentToken.Split(CharactersConstants.HYPHEN_CHAR)[1] : string.Empty;
            string mappingCardType = (source.MerchantDefinedData.Where(mdd => mdd.Key == PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_PAYMENT_TYPE_ID).Any()) ? source.MerchantDefinedData.Where(mdd => mdd.Key == PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_PAYMENT_TYPE_ID).FirstOrDefault().Value : PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_DEFAULT;
            if (mappingCardType.ToUpper() == PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_CARD_TYPE_NAME_VISA)
                mappingCardType = PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_VISA;
            else if (mappingCardType.ToUpper() == PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_CARD_TYPE_NAME_MASTERCARD)
                mappingCardType = PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_MASTERCARD;
            else if (mappingCardType.ToUpper() == PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_CARD_TYPE_NAME_AMERICAN_EXPRESS)
                mappingCardType = PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_AMERICAN_EXPRESS;
            else
                mappingCardType = PaymentProcessorConstants.PAYMENT_ORDER_PROCESS_CARD_TYPE_ID_DEFAULT;
            destination = new Riskv1authenticationresultsPaymentInformation(
                Customer: new Ptsv2paymentsPaymentInformationCustomer(CustomerId: mappingSource),
                TokenizedCard: new Riskv1authenticationresultsPaymentInformationTokenizedCard(Type: mappingCardType, Number: source.TokenizedCardNumber)
            );
            return destination;
        }
        #endregion
    }
}
