namespace Soriana.PPS.Common.Services
{
    public abstract class ServiceBase
    {
        protected abstract void ValidateRequest(object request);
        protected abstract void ValidateResponse(object response, object request = null);
    }
}
