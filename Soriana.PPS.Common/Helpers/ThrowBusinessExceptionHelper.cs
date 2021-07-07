using Soriana.PPS.Common.DTO.Common;

namespace Soriana.PPS.Common.Helpers
{
    public static class ThrowBusinessExceptionHelper
    {
        #region Public Methods
        public static void ThrowBusinessException(object request, object response, string descriptionDetail, string serviceInterface, int statusCode, string descriptionCode)
        {
            throw new BusinessException(new BusinessResponse()
            {
                StatusCode = statusCode,
                Description = descriptionCode,
                DescriptionDetail = descriptionDetail,
                ContentRequest = request,
                ContentResponse = response
            })
            {
                ServiceInterface = serviceInterface,
            };
        }

        public static BusinessException GetThrowException(object request, object response, string descriptionDetail, string serviceInterface, int statusCode, string descriptionStatusCode)
        {
            return new BusinessException(new BusinessResponse()
            {
                StatusCode = statusCode,
                Description = descriptionStatusCode,
                DescriptionDetail = descriptionDetail,
                ContentRequest = request,
                ContentResponse = response
            })
            {
                ServiceInterface = serviceInterface,
            };
        }
        #endregion
    }
}
