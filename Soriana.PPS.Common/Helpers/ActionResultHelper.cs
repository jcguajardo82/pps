using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Helpers
{
    public static class ActionResultHelper
    {
        #region Public Methods
        public static async Task<T> GetResponseType<T>(MemoryStream response)
        {
            //string jsonResponse = await new StreamReader(response).ReadToEndAsync();
            //object objectDeserialized = JsonConvert.DeserializeObject<object>(jsonResponse);
            //return (T)Convert.ChangeType(objectDeserialized, typeof(T));
            string jsonResponse = await new StreamReader(response).ReadToEndAsync();
            T responseType;
            try
            {
                responseType = JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            finally
            {

            }
            return responseType;
        }
        #endregion
    }
}
