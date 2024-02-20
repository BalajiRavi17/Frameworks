using APIAutomation.RestAPIAutomation.Comparers;
using APIAutomation.RestAPIAutomation.JsonSchema.Users;
using APIAutomation.RestAPIAutomation.RequestPayloadCreator;
using APIAutomation.RestAPIAutomation.RequestResponsePayloadCreator;
using APIAutomation.RestAPIAutomation.RequestResponsePayloadCreator.RequestLibraries;
using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using TechTalk.SpecFlow;
using WebAutomation.Support.Reporting;

namespace APIAutomation.TestCases.StepDefinition
{
    [Binding]
    public class HUsersStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        Http_RequestLibrary helper;
        LogGenerator logGenerator;
        ILog Logger;

        public HUsersStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            logGenerator = LogGenerator.getInstance();
            Logger = logGenerator.GenerateLogs(typeof(UsersStepDefinition));
        }

        [Given(@"Endpoint for users_H")]
        public void GivenEndpointForUsers_H()
        {


            try
            {
                helper = new Http_RequestLibrary();

                Logger.Info("Inital set up successfull");
            }
            catch (Exception ex)
            {
                Logger.Error("Initial set up failed");
            }
        }

        [When(@"I set up a get request for ""([^""]*)""_H")]
        public void WhenISetUpAGetRequestFor_H(string userId)
        {
            try
            {
                var httpRequest = helper.HttpRequestCreator(HttpMethod.Get,"users"+userId);
                _scenarioContext.Add("HttpRequest", httpRequest);
                Logger.Info("Set up Successful");
            }
            catch (Exception ex)
            {
                Logger.Error("Set up failed");
            }
        }

        [When(@"the API is executed_H")]
        public async Task WhenTheAPIIsExecuted_H()
        {
            try
            {
                var httpClient = _scenarioContext.Get<HttpClient>("HttpClient");
                HttpResponseMessage httpResponse = await helper.getResponse(httpClient, _scenarioContext.Get<HttpRequestMessage>("HttpRequest"));
                _scenarioContext.Add("HttpResponse", httpResponse);
                
                Logger.Info("Successfully API is executed");
            }
            catch (Exception ex)
            {
                Logger.Error("API exeution failed");
            }
        }

        [When(@"I set up a Post request_H")]
        public void WhenISetUpAPostRequest_H()
        {
            try
            {
                var requestPayload = new PostUsersRequest();
                
                var request = PostUsersRequest.createPayload("Balaji", "Test Engineer");

                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                
                _scenarioContext.Add("Request", request);
                
                var httpRequest = helper.HttpRequestCreator(HttpMethod.Post, "users", content);
                
                _scenarioContext.Add("HttpRequest", httpRequest);

                Logger.Info("Request has be set successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Request set up failed");
            }
        }

        [When(@"I set up a Put request for ""([^""]*)""_H")]
        public void WhenISetUpAPutRequestFor_H(string userId)
        {
            try
            {
                var requestPayload = new PostUsersRequest();
                var request = PostUsersRequest.createPayload("Balaji Ravi", "Software test automation engineer");
                
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                _scenarioContext.Add("Request", request);
                var httpRequest = helper.HttpRequestCreator(HttpMethod.Put, "users/" + userId, content);
                _scenarioContext.Add("HttpRequest", httpRequest);

                Logger.Info("Request has be set successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Request set up failed");
            }
        }

        [When(@"I set up a Delete request for ""([^""]*)""_H")]
        public void WhenISetUpADeleteRequestFor_H(string userId)
        {
            try
            {
                var httpRequest = helper.HttpRequestCreator(HttpMethod.Delete, "users" + userId);
                _scenarioContext.Add("HttpRequest", httpRequest);
                Logger.Info("Set up Successful");
            }
            catch (Exception ex)
            {
                Logger.Error("Request set up failed");
            }
        }

        [Then(@"Response should be No content_H")]
        public void ThenResponseShouldBeNoContent_H()
        {
            try
            {
                HttpResponseMessage httpResponse = _scenarioContext.Get<HttpResponseMessage>("HttpResponse");

                Assert.AreEqual(HttpStatusCode.NoContent, httpResponse.StatusCode);

                Logger.Info("Successfully verified the results");
            }
            catch (Exception ex)
            {
                Logger.Error("Result verification failed");
            }
        }

        [Then(@"Response should be OK and response payload should match_H")]
        public async Task ThenResponseShouldBeOKAndResponsePayloadShouldMatch_H()
        {
            try
            {
                HttpResponseMessage httpResponse = _scenarioContext.Get<HttpResponseMessage>("HttpResponse");

                var json = await httpResponse.Content.ReadFromJsonAsync<PostUsers>();
                
                Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
                Assert.IsTrue(PostUserComparer.compareResults(_scenarioContext.Get<PostUsers>("Request"),json));

                Logger.Info("Successfully verified result");
            }
            catch (Exception ex)
            {
                Logger.Error("Result verification failed");
            }
        }

        [Then(@"Response should be Created and response payload should match_H")]
        public async Task ThenResponseShouldBeCreatedAndResponsePayloadShouldMatch_H()
        {
            try
            {
                HttpResponseMessage httpResponse = _scenarioContext.Get<HttpResponseMessage>("HttpResponse");

                var json = await httpResponse.Content.ReadFromJsonAsync<PostUsers>();

                Assert.AreEqual(HttpStatusCode.Created, httpResponse.StatusCode);
                Assert.IsTrue(PostUserComparer.compareResults(_scenarioContext.Get<PostUsers>("Request"), json));

                Logger.Info("Successfully verified the results");
            }
            catch (Exception ex)
            {
                Logger.Error("Result verification failed");
            }
        }

        [Then(@"Get List user response should be OK and response payload should match_H")]
        public async Task ThenGetListUserResponseShouldBeOKAndResponsePayloadShouldMatch_H()
        {
            try
            {
                HttpResponseMessage httpResponse = _scenarioContext.Get<HttpResponseMessage>("HttpResponse");

                var responseBody = await httpResponse.Content.ReadAsStringAsync();
                var json= JsonConvert.DeserializeObject<Root>(responseBody);

                Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
                Assert.IsTrue(GetUserComparer.compareResults(json, RootUserResponse.GetUserResponse()));

                Logger.Info("Successfully verified result");
            }
            catch (Exception ex)
            {
                Logger.Error("Result verification failed");
            }
        }
    }
}