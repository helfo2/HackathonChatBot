using FleetChatBotServer.Infraestructure.Libraries.Utils;
using FleetChatBotServer.Infrastructure.Commons.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FleetChatBotServer.Infrastructure.Commons.HttpConnection
{
    public class HttpConnection : IHttpConnection
    {
        private readonly HttpClient _httpClient = new();

        public HttpConnection(HttpConnectionConfig httpConnectionConfig)
        {
            HttpConnectionConfig = httpConnectionConfig;

            if (!string.IsNullOrEmpty(HttpConnectionConfig.UserName))
            {
                byte[] byteArray = Encoding.ASCII.GetBytes($"{HttpConnectionConfig.UserName}:{HttpConnectionConfig.Password}");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }
            _httpClient.BaseAddress = new Uri(HttpConnectionConfig.ServiceUri.AbsoluteUri + $"api/{HttpConnectionConfig.ServiceKey}/{HttpConnectionConfig.ServiceVersion}/");
        }

        public HttpConnectionConfig HttpConnectionConfig { get; set; }

        public async Task<T> GetAsync<T>(HttpResquestBody requestBody)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(BuildRequestUri(requestBody));
                HttpStatusCode statusCode = response.StatusCode;
                Uri requestedUri = response.RequestMessage.RequestUri;
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = $"Error during request uri: {requestedUri} - StatusCode: {statusCode} - Reason: {response.ReasonPhrase} - Message: {result}";
                    Log.Error(errorMessage);
                    throw new Exception(errorMessage);
                }

                Log.Information("Request uri: {@0} - StatusCode: {@1} - Content: {@2}", requestedUri, statusCode, result);
                return Helpers.JsonSerializer.Deserialize<T>(result);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetAsync error");
                throw;
            }
        }

        public async Task<T> PostAsync<T>(HttpPostBody postBody)
        {
            try
            {
                StringContent content = new(postBody.Content != null ? Helpers.JsonSerializer.Serialize(postBody.Content) : "", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(BuildRequestUri(postBody), content);
                HttpStatusCode statusCode = response.StatusCode;
                Uri requestedUri = response.RequestMessage.RequestUri;
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = $"Error during post uri: {requestedUri} - StatusCode: {statusCode} - Reason: {response.ReasonPhrase} - Message: {result}";
                    Log.Error(errorMessage);
                    throw new Exception(errorMessage);
                }

                Log.Debug("Post uri: {@0} - StatusCode: {@1} - Content: {@2}", requestedUri, statusCode, result);
                return Helpers.JsonSerializer.Deserialize<T>(result);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "PostAsync error");
                throw;
            }
        }

        private string BuildRequestUri(HttpBaseBody body)
        {
            return _httpClient.BaseAddress + body.ControllerPath + BuildQueryString(body.QueryOptions);
        }

        private string BuildQueryString(IDictionary<string, object> queryOptions)
        {
            if (queryOptions == null || queryOptions.Count == 0)
            {
                return "";
            }
            string result = "";
            foreach (KeyValuePair<string, object> item in queryOptions)
            {
                result += $"{(result.Length == 0 ? "?" : "&")}{item.Key}={FormatQueryValue(item.Value)}";
            }
            return result;
        }

        private static string FormatQueryValue(object value)
        {
            return value == null ? "null" : value is string[] values ? string.Join(",", values) : value.ToString();
        }
    }
}