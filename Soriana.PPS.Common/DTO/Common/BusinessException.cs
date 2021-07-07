using Newtonsoft.Json;
using System;

namespace Soriana.PPS.Common.DTO.Common
{
    public sealed class BusinessException : Exception
    {
        #region Public Properties
        public string ExecutedService { get; set; }
        public string ExecutedInnerService { get; set; }
        public string ServiceInterface { get; set; }
        public int StatusCode { get; }
        #endregion
        #region Constructors
        public BusinessException(BusinessResponse businessMessage) : base(message: JsonConvert.SerializeObject(businessMessage))
        {
            ExecutedService = businessMessage.DescriptionDetail.ToString();
            StatusCode = businessMessage.StatusCode;
        }
        public BusinessException(string message) : base(message: message)
        { }
        public BusinessException(string message, Exception inner) : base(message: message, innerException: inner)
        { }
        #endregion
    }
}
