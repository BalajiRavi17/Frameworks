using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.RestAPIAutomation.RequestResponsePayloadCreator.RequestLibraries
{
    public class Http_RequestLibrary
    {
        string baseUri = "https://reqres.in/api/";
        public HttpRequestMessage HttpRequestCreator(HttpMethod method, string url)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(baseUri + url)
            };

            return httpRequestMessage;
        }
        public HttpRequestMessage HttpRequestCreator(HttpMethod method, string url, StringContent requestPayload)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(baseUri + url),
                Content = requestPayload
            };

            return httpRequestMessage;
        }
        public async Task<HttpResponseMessage> getResponse(HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
