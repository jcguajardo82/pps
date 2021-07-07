using Microsoft.AspNetCore.Mvc;
using Soriana.PPS.Common.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.HttpClient
{
    public interface IHttpClientService
    {
        Task<IActionResult> SendAsync(string message, HttpMethod httpMethod, bool ensureSuccessStatusCode = false);
        Task<IActionResult> PostAsync(string message, bool ensureSuccessStatusCode = false);
        void SetHttpClientOptions(HttpClientOptions httpClientOptions);
        Task<IActionResult> SendAsync(string message, HttpMethod httpMethod, string useUserAgent, bool ensureSuccessStatusCode = false);
        Task<IActionResult> PostAsync(string message, string useUserAgent, bool ensureSuccessStatusCode = false);
    }
}
