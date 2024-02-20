using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace APIAutomation.RestAPIAutomation.RequestResponsePayloadCreator.RequestLibraries
{
    public class RequestLibrary
    {

        public RestRequest RequestCreator(Method method, string endpoint, Dictionary<string, string> headers = null)
        {
            RestRequest restRequest = new RestRequest(endpoint)
            {
                Method = method,
            };
            restRequest.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }

            });
            if (headers != null)
            {
                restRequest.AddHeaders(headers);
            }
            return restRequest;
        }

        public RestRequest RequestCreator(Method method, string endpoint, string requestBody, Dictionary<string, string> headers = null)
        {
            RestRequest restRequest = new RestRequest(endpoint)
            {
                Method = method,
            };
            restRequest.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }

            });
            if (headers != null)
            {
                restRequest.AddHeaders(headers);
            }
            restRequest.AddBody(requestBody);
            return restRequest;
        }
        public async Task<RestResponse> getResponse(RestClient restClient, RestRequest restRequest)
        {
            return await restClient.ExecuteAsync(restRequest);
        }
    }
}