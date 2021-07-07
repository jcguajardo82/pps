using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Soriana.PPS.Common.Configuration;
using Soriana.PPS.Common.Constants;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.HttpClient
{
    public sealed class HttpClientService : IHttpClientService
    {
        #region Private Fields
        private readonly IHttpClientFactory _HttpClientFactory;
        private HttpClientOptions _HttpClientOptions;
        private readonly HttpClientListOptions _HttpClientListOptions;
        private readonly bool _HasMultipleEndPoints = false;
        #endregion
        #region Public Properties
        public HttpClientOptions HttpClientOptions { get { return _HttpClientOptions; } }
        public HttpClientListOptions HttpClientListOptions { get { return _HttpClientListOptions; } }
        public bool HasMultipleEndPoints { get { return _HasMultipleEndPoints; } }
        #endregion
        #region Public Constructors
        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
            _HasMultipleEndPoints = false;
        }
        public HttpClientService(IHttpClientFactory httpClientFactory, IOptions<HttpClientOptions> options)
        {
            _HasMultipleEndPoints = false;
            _HttpClientFactory = httpClientFactory;
            _HttpClientOptions = options?.Value;
        }
        public HttpClientService(IHttpClientFactory httpClientFactory, IOptions<HttpClientListOptions> options)
        {
            _HasMultipleEndPoints = true;
            _HttpClientFactory = httpClientFactory;
            _HttpClientListOptions = options?.Value;
        }
        #endregion
        #region Public Methods
        public void SetHttpClientOptions(HttpClientOptions httpClientOptions)
        {
            _HttpClientOptions = httpClientOptions;
        }
        public async Task<IActionResult> SendAsync(string message, HttpMethod httpMethod, bool ensureSuccessStatusCode)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, _HttpClientOptions.EndPoint))
            {
                if (!string.IsNullOrEmpty(_HttpClientOptions.Accept))
                    requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_ACCEPT, _HttpClientOptions.Accept);
                if (!string.IsNullOrEmpty(_HttpClientOptions.ContentType))
                    requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_CONTENT_TYPE, _HttpClientOptions.ContentType);
                if (!string.IsNullOrEmpty(_HttpClientOptions.UserAgent))
                    requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_USER_AGENT, _HttpClientOptions.UserAgent);
                System.Net.Http.HttpClient client = _HttpClientFactory.CreateClient(_HttpClientOptions.EndPointName);
                requestMessage.Content = new StringContent(message);
                HttpResponseMessage reponseMessage = await client.SendAsync(requestMessage);
                if (ensureSuccessStatusCode)
                    reponseMessage.EnsureSuccessStatusCode();
                Stream streamOutput = await reponseMessage.Content.ReadAsStreamAsync();
                return new OkObjectResult(streamOutput);
            }
        }
        public async Task<IActionResult> PostAsync(string message, bool ensureSuccessStatusCode = false)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _HttpClientOptions.EndPoint))
            {
                if (!string.IsNullOrEmpty(_HttpClientOptions.Accept))
                    requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_ACCEPT, _HttpClientOptions.Accept);
                if (!string.IsNullOrEmpty(_HttpClientOptions.ContentType))
                    requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_CONTENT_TYPE, _HttpClientOptions.ContentType);
                if (!string.IsNullOrEmpty(_HttpClientOptions.UserAgent))
                    requestMessage.Headers.Add(HttpClientConstants.HTTP_CLIENT_HEADER_USER_AGENT, _HttpClientOptions.UserAgent);
                System.Net.Http.HttpClient client = _HttpClientFactory.CreateClient(_HttpClientOptions.EndPointName);
                requestMessage.Content = new StringContent(message);
                HttpResponseMessage reponseMessage = await client.PostAsync(requestMessage.RequestUri, requestMessage.Content);
                Stream streamOutput;
                try
                {
                    reponseMessage.EnsureSuccessStatusCode();
                    streamOutput = await reponseMessage.Content.ReadAsStreamAsync();
                }
                catch
                {
                    streamOutput = await reponseMessage.Content.ReadAsStreamAsync();
                    return new BadRequestObjectResult(streamOutput);
                }
                return new OkObjectResult(streamOutput);
            }
        }

        public Task<IActionResult> SendAsync(string message, HttpMethod httpMethod, string useUserAgent, bool ensureSuccessStatusCode = false)
        {
            if (_HttpClientListOptions == null && HasMultipleEndPoints) return null;
            _HttpClientOptions = _HttpClientListOptions.HttpClientOptions.Where(op => op.UserAgent == useUserAgent).FirstOrDefault();
            if (_HttpClientOptions == null && !_HasMultipleEndPoints) return null;
            return SendAsync(message, httpMethod, ensureSuccessStatusCode);
        }

        public Task<IActionResult> PostAsync(string message, string useUserAgent, bool ensureSuccessStatusCode = false)
        {
            if (_HttpClientListOptions == null && HasMultipleEndPoints) return null;
            _HttpClientOptions = _HttpClientListOptions.HttpClientOptions.Where(op => op.UserAgent == useUserAgent).FirstOrDefault();
            if (_HttpClientOptions == null && !_HasMultipleEndPoints) return null;
            return PostAsync(message, ensureSuccessStatusCode);
        }
        #endregion
    }
}
