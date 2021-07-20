using NUnit.Framework;
using RestSharp;
using Specflow.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace Specflow.StepDefinitions
{
    [Binding]
    public class APITests_ISSPositionSSteps
    {
        public List<RestClient> Clients { get; private set; }
        public List<IRestResponse> PositionResponses { get; private set; }
        public List<RestRequest> Requests { get; private set; }
        public List<string> finalURLs { get; private set; }
        
        public string strTimestamps = null;
        public readonly string baseURL = "https://api.wheretheiss.at/v1/satellites";

        [Given(@"I have called the Satellite Position API with paramters ""(.*)"" and (.*) Timestamp\(s\)")]
        public void GivenIHaveCalledTheSatellitePositionAPIWithParamtersAndTimestampS(string urlParameter, int numTimeStamps)
        {
            finalURLs = new List<string>();
            long timestamp = DateTime.Now.ToFileTime();            
            strTimestamps = timestamp.ToString().Substring(0, 11);

            foreach (var item in Setup.ids)
            {
                if(numTimeStamps == 0)
                {                    
                    finalURLs.Add(baseURL + "/" + item + "/positions?timestamps=");
                }else if (numTimeStamps > 1)
                {
                    for (int i=2;i <= numTimeStamps; i++)
                    {
                        long itimestamp = timestamp - i*1111111111;
                        strTimestamps = strTimestamps + ","+itimestamp.ToString().Substring(0, 11);
                    }
                    if (urlParameter.Contains("kilometers"))
                    {
                        finalURLs.Add(baseURL + "/" + item + "/positions?timestamps=" + strTimestamps+ "&units=kilometers");
                    }
                    else if(urlParameter.Contains("miles"))
                    {                        
                        finalURLs.Add(baseURL + "/" + item + "/positions?timestamps=" + strTimestamps + "&units=miles");
                    }
                    else
                    {
                        finalURLs.Add(baseURL + "/" + item + "/positions?timestamps=" + strTimestamps);
                    }
                                       
                }
                else
                {
                    if (urlParameter.Contains("kilometers"))
                    {
                        finalURLs.Add(baseURL + "/" + item + "/positions?timestamps=" + strTimestamps + "&units=kilometers");
                    }
                    else if (urlParameter.Contains("miles"))
                    {
                        finalURLs.Add(baseURL + "/" + item + "/positions?timestamps=" + strTimestamps + "&units=miles");
                    }
                    else
                    {
                        finalURLs.Add(baseURL + "/" + item + "/positions?timestamps=" + strTimestamps);
                    }                    
                }                
            }
        }

        [Given(@"I have called the Satellite Position API with paramters ""(.*)"" and InvalidTimestamp\(s\)")]
        public void GivenIHaveCalledTheSatellitePositionAPIWithParamtersAndInvalidTimestampS(string p0)
        {
            foreach (var item in Setup.ids)
            {
                finalURLs = new List<string>();
                string strtimestamp = "invalidChars";
                finalURLs.Add(baseURL + "/" + item + "/positions?timestamps=" + strtimestamp);
            }
        }

        [When(@"response status for Position API is ""(.*)""")]
        public void WhenResponseStatusForPositionAPIIs(string expStatus)
        {
            PositionResponses = new List<IRestResponse>();
            foreach (var item in finalURLs)
            {
               
                var restClient = new RestClient(item);
                var restRequest = new RestRequest(item, Method.GET);
                PositionResponses.Add(restClient.Execute(restRequest));
                string status = PositionResponses[0].StatusCode.ToString();
                Assert.AreEqual(expStatus, status);                
            }
        }
        

        [Then(@"response should contain (.*) Position\(s\) data")]
        public void ThenResponseShouldContainPositionSData(int expectedcount)
        {
            string msgFromAPI="";
            foreach (var response in PositionResponses)
            {
                if (expectedcount == 0)
                {
                    msgFromAPI = response.StatusCode.ToString();
                    Assert.AreEqual(msgFromAPI, "BadRequest");                    
                }
                else { 
                    int responsecount = PositionResponses.Count;
                    Assert.IsTrue(response.ContentType.Equals("application/json"));  
                    var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PositionResponse>>(response.Content);
                    if (expectedcount > 0)
                    {
                        var counts = responseContent.Count;
                        Assert.AreEqual(counts, expectedcount);
                        Console.WriteLine("Expected Responsecount = " + expectedcount + " matches Actual Responseccount = " + counts);
                        for (int i = 0; i < counts; i++)
                        {
                            Assert.AreEqual(responseContent[i].id, 25544);
                            Console.WriteLine("Expected id(25544) matches with actual id returned for response element at["+i+"]");
                        }
                    }
                }
            }
        }
    }
}
