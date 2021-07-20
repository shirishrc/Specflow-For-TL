using NUnit.Framework;
using RestSharp;
using Specflow.Entities;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Specflow.StepDefinitions
{
    [Binding]
    public class APITests_ISSTlesSteps
    {
        public List<RestClient>Clients { get; private set; }
        public List<IRestResponse> TlesResponses { get; private set; }
        public List<RestRequest> Requests { get; private set; }
        public List<string> finalURLs { get; private set; }
        

        public readonly string baseURL = "https://api.wheretheiss.at/v1/satellites";

        [Given(@"I have called the Satellite API by ""(.*)""")]
        public void GivenIHaveCalledTheSatelliteAPIBy(string urlParameter)
        {
            finalURLs = new List<string>();
            foreach (var item in Setup.ids)
            {
                if (urlParameter.Equals("IDTles")){
                    finalURLs.Add(baseURL + "/" + item + "/tles");
                }
                else if (urlParameter.Equals("IDTles-TextParameter"))
                {
                    finalURLs.Add(baseURL + "/" + item + "/tles?format=text");
                }
                               
            }           
        }
        
        [When(@"response status is OK")]
        public void WhenResponseStatusIsOK()
        {
            TlesResponses = new List<IRestResponse>();
            foreach (var item in finalURLs)
            {
                //Clients.Add(new RestClient(item));
                //Requests.Add(new RestRequest(item, Method.GET));
                var restClient=new RestClient(item);
                var restRequest =new RestRequest(item, Method.GET);
                TlesResponses.Add(restClient.Execute(restRequest));
            }            
        }
        
        [Then(@"response should contain TLE data")]
        public void ThenResponseShouldContainTLEData()
        {
            foreach (var response in TlesResponses)
            {
               
                Assert.IsTrue(response.ContentType.Equals("application/json"));
                var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<TlesResponse>(response.Content);
                Console.WriteLine(responseContent.id);
                Assert.AreEqual(responseContent.id, 25544);
            }           
        }

        [Then(@"response should contain TLE data in text format")]
        public void ThenResponseShouldContainTLEDataInTextFormat()
        {
            foreach (var response in TlesResponses)
            {
                //var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<TlesResponse>(response.Content);
                Assert.IsTrue(response.ContentType.Equals("text/plain"));
                var responseContent = response.Content;
            }
        }

    }
}
