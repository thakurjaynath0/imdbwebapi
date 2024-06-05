using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace ImdbWebApi.Test.StepDefinitions
{
    public class BaseSteps
    {
        protected WebApplicationFactory<TestStartup> Factory;
        protected HttpClient Client { get; set; }
        protected HttpResponseMessage Response { get; set; }

        private const string basePath = @"E:\ASP.NET Core\Projects\Imdb_Application\ImdbWebApi.Test\MockData\";

        public BaseSteps(WebApplicationFactory<TestStartup> baseFactory)
        {
            Factory = baseFactory;  
        }

        [Given(@"I am a client")]
        public void IAmClient()
        {
            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost:57781/")
            });
        }

        [When(@"I make a GET Request to '(.*)'")]
        public async Task MakeGetRequest(string resourceEndpoint)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            Response = await Client.GetAsync(uri);  
        }

        [When(@"I make a PUT Request to '(.*)' with the following Data '(.*)'")]
        public virtual async Task MakePutRequest(string resourceEndPoint, string filePath)
        {
            var path = Path.Combine(basePath, "RequestData", filePath);
            var jsonData = File.ReadAllText(path);
            var putRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Response = await Client.PutAsync(putRelativeUri, content); 
        }

        [When(@"I make a POST Request to '(.*)' with the following Data '(.*)'")]
        public virtual async Task MakePostRequest(string resourceEndPoint, string filePath)
        {
            var path = Path.Combine(basePath, "RequestData", filePath);
            var jsonData = File.ReadAllText(path);
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Response = await Client.PostAsync(postRelativeUri, content);
        }

        [When(@"I make a DELETE Request to '(.*)'")]
        public virtual async Task MakeDeleteRequest(string resourceEndPoint)
        {
            var deleteRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            Response = await Client.DeleteAsync(deleteRelativeUri);
        }

        [Then(@"response code must be '(.*)'")]
        public void CompareResponseCode(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

        [Then(@"response data must look like '(.*)'")]
        public void CompareResponse(string filePath)
        {
            var path = Path.Combine(basePath, "ResponseData", filePath);
            var jsonData = File.ReadAllText(path);
            var expectedResponse = JToken.Parse(jsonData);
            var responseData = Response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var actualResponse = JToken.Parse(responseData);
            Assert.True(JToken.DeepEquals(actualResponse, expectedResponse));
        }
    }
}

        