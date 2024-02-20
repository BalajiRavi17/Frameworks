using APIAutomation.RestAPIAutomation.Comparers;
using APIAutomation.RestAPIAutomation.JsonSchema.Users;
using APIAutomation.RestAPIAutomation.RequestPayloadCreator;
using APIAutomation.RestAPIAutomation.RequestResponsePayloadCreator;
using APIAutomation.RestAPIAutomation.RequestResponsePayloadCreator.RequestLibraries;
using log4net;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using WebAutomation.Support.Reporting;

namespace APIAutomation.TestCases.StepDefinition
{
    [Binding]
    public sealed class UsersStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;
        RequestLibrary helper;
        string requestJsonString;
        LogGenerator logGenerator;
        ILog Logger;

        private UsersStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            logGenerator = LogGenerator.getInstance();
            Logger = logGenerator.GenerateLogs(typeof(UsersStepDefinition));
        }


        [Given(@"Endpoint for users")]
        public void GivenEndpointForUsers()
        {
            try
            {
                helper = new RequestLibrary();

                Logger.Info("Inital set up successfull");
            }
            catch (Exception ex)
            {
                Logger.Error("Initial set up failed");
            }
        }

        [When(@"I set up a get request for ""([^""]*)""")]
        public void WhenISetUpAGetRequestFor(string userId)
        {
            try
            {
                RestRequest restRequest = helper.RequestCreator(Method.Get,"/users"+userId);
                _scenarioContext.Add("RestRequest", restRequest);
                Logger.Info("Set up Successful");
            }
            catch (Exception ex)
            {
                Logger.Error("Set up failed");
            }
        }

        [When(@"the API is executed")]
        public async Task WhenTheAPIIsExecuted()
        {
            try
            {
                var _restClient = _scenarioContext.Get<RestClient>("RestClient");
                var restResponse = await helper.getResponse(_restClient, _scenarioContext.Get<RestRequest>("RestRequest"));
                _scenarioContext.Add("RestResponse", restResponse);
                Logger.Info("Successfully API is executed");
            } catch (Exception ex)
            {
                Logger.Error("API exeution failed");
            }
        }

        [When(@"I set up a Post request")]
        public void WhenISetUpAPostRequest()
        {
            try
            {
                var requestPayload = new PostUsersRequest();
                var request = PostUsersRequest.createPayload("Balaji", "Test Engineer");
                requestJsonString = JsonConvert.SerializeObject(request);
                _scenarioContext.Add("Request", request);
                var restRequest = helper.RequestCreator(Method.Post, "api/users", requestJsonString);
                _scenarioContext.Add("RestRequest", restRequest);

                Logger.Info("Request has be set successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to set up Request");
            }
        }


        [Then(@"Get List user response should be OK and response payload should match")]
        public void ThenGetSingleUserResponseShouldBeOKAndResponsePayloadShouldMatch()
        {
            try
            {
                var restResponse = _scenarioContext.Get<RestResponse>("RestResponse");
                var json = JsonConvert.DeserializeObject<Root>(restResponse.Content);

                Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);
                Assert.IsTrue(GetUserComparer.compareResults(json, RootUserResponse.GetUserResponse()));
                
                Logger.Info("Successfully verified result");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify result");
            }
        }

        [Then(@"Response should be Created and response payload should match")]
        public void ThenResponseShouldBeCreatedAndResponsePayloadShouldMatch()
        {
            try
            {
                var restResponse = _scenarioContext.Get<RestResponse>("RestResponse");
                var json = JsonConvert.DeserializeObject<PostUsers>(restResponse.Content);

                Assert.AreEqual(HttpStatusCode.Created, restResponse.StatusCode);
                Assert.IsTrue(PostUserComparer.compareResults(_scenarioContext.Get<PostUsers>("Request"), json));
                
                Logger.Info("Successfully verified the results");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify result");
            }
        }

        [When(@"I set up a Put request for ""([^""]*)""")]
        public void WhenISetUpAPutRequestFor(string userId)
        {
            try
            {
                var requestPayload = new PostUsersRequest();
                var request = PostUsersRequest.createPayload("Balaji Ravi", "Software test automation engineer");
                requestJsonString = JsonConvert.SerializeObject(request);
                _scenarioContext.Add("Request", request);
                var restRequest = helper.RequestCreator(Method.Put, "api/users" + userId, requestJsonString);
                _scenarioContext.Add("RestRequest", restRequest);
                
                Logger.Info("Request has be set successfully");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to set up Request");
            }
        }

        [Then(@"Response should be OK and response payload should match")]
        public void ThenResponseShouldBeOKAndResponsePayloadShouldMatch()
        {
            try
            {
                var restResponse = _scenarioContext.Get<RestResponse>("RestResponse");
                var json = JsonConvert.DeserializeObject<PostUsers>(restResponse.Content);

                Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);
                Assert.IsTrue(PostUserComparer.compareResults(_scenarioContext.Get<PostUsers>("Request"), json));
                
                Logger.Info("Successfully verified the results");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify result");
            }
        }

        [When(@"I set up a Delete request for ""([^""]*)""")]
        public void WhenISetUpADeleteRequestFor(string userId)
        { 
            try
            {
                RestRequest restRequest = helper.RequestCreator(Method.Delete, "/users" + userId);
                _scenarioContext.Add("RestRequest", restRequest);
                Logger.Info("Set up Successful");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to set up Request");
            }
        }

        [Then(@"Response should be No content")]
        public void ThenResponseShouldBeNoContent()
        {
            try
            {
                var restResponse = _scenarioContext.Get<RestResponse>("RestResponse");
                Assert.AreEqual(HttpStatusCode.NoContent, restResponse.StatusCode);

                Logger.Info("Successfully verified the results");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to verify result");
            }
        }
    }
}