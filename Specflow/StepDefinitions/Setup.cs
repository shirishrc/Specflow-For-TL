using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using Specflow.Entities;
using TechTalk.SpecFlow;

namespace Specflow.StepDefinitions
{
    [Binding]
    public static class Setup
    {
        private static string baseURL = "https://api.wheretheiss.at/v1/satellites";

        public static RestClient Client { get; private set; }
        public static RestRequest Request { get; private set; }
        public static List<string> names { get; private set; }
        public static List<int> ids { get; private set; }

        [BeforeTestRun]

        [Given(@"I have executed GET request for BaseURL and status should be OK and response should contain Name and ID in response")]
        public static void GivenIHaveExecutedGETRequestForBaseURLAndStatusShouldBeOKAndResponseShouldContainNameAndIDInResponse()
        {
            Client = new RestClient(baseURL);
            Request = new RestRequest(baseURL, Method.GET);
            names = new List<string>();
            ids = new List<int>();
            var Response = Client.Execute(Request);
            Assert.AreEqual(Response.StatusCode.ToString(), "OK");
            var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SatelliteResponse>>(Response.Content);
            foreach (var i in responseContent)
            {
                names.Add(i.name);
                ids.Add(i.id);
                Console.WriteLine(i.name + i.id);
            }
        }

    }
}
