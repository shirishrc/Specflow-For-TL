using NUnit.Framework;
using TechTalk.SpecFlow;
using RestSharp;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Specflow.StepDefinitions
{
    [Binding]
    public class APITestsSteps
    {
        public static string finalURL;
        public readonly string baseURL = "https://api.wheretheiss.at/v1/satellites";
        public readonly string satellites_position_url = "25544/positions";
        public static RestClient Client;
        public static RestRequest Request;
        public static IRestResponse Response;
        public static List<string> ids;
        public static List<string> names; 
        public static string timestamps;

        
        

        [Given(@"I have executed GET request for BaseURL")]
        public void GivenIHaveExecutedGETRequestForBaseURL()
        {
            Client = new RestClient(baseURL);
            Request = new RestRequest(baseURL, Method.GET);
        }

        [Then(@"status should be OK and response should contain Name and ID in response")]
        public void ThenStatusShouldBeOKAndResponseShouldContainNameAndIDInResponse()
        {
            Response = Client.Execute(Request);
            Assert.AreEqual(Response.StatusCode.ToString(), "OK");
            dynamic responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject(Response.Content);
            foreach (dynamic i in responseContent)
            {
                name = i.name;
                id = i.id;
            }
            Console.WriteLine(name + ",--," + id);
        }
                
        [Given(@"I have executed GET request for Satellites API by ""(.*)""")]
        public void GivenIHaveExecutedGETRequestForSatellitesAPIBy(string byType)
        {
            switch (byType)
            {
                case "ID":
                    finalURL = baseURL + "/" + id;
                    break;
                case "ID&Position":
                    finalURL = baseURL + "/" + id+ "positions?timestamps="+ timestamps;
                    break;
                case "ID&tles":
                    finalURL = baseURL + "/" + id+ "/tles";
                    break;

            }
            Request = new RestRequest(finalURL, Method.GET);
            Client = new RestClient(finalURL);
            Response = Client.Execute(Request);
            Assert.AreEqual(Response.StatusCode.ToString(), "OK");
            
            Console.WriteLine("Checking response");
        }

        [Then(@"response returned should have ""(.*)""")]
        public void ThenResponseReturnedShouldHave(string expectedFields)
        {
            
            dynamic responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject(Response.Content);
            Console.WriteLine(expectedFields) ;
            
            List<string> expFields = expectedFields.Split(',').ToList();

            foreach (var ele in expFields)
            {
                if (Response.Content.ToString().Contains(ele))
                {
                    Console.WriteLine("Response has " + ele);
                }                
            }

            //foreach (dynamic i in responseContent)
            //{
            //    Console.WriteLine(responseContent.ToString());
                
            //    //name = i.name;
            //    //id = i.id;
            //    //string latitude = i.latitude;
            //    //string longitude = i.longitude;
            //    //string altitude = i.altitude;
            //    //string velocity = i.velocity;
            //    //string visibility = i.visibility;
            //    //string footprint = i.footprint;
            //    //string timestamp = i.timestamp;
            //    //string daynum = i.daynum;
            //    //string solar_lat = i.solar_lat;
            //    //string solar_lon = i.solar_lon;
            //    //string units = i.units;
            //}
            
        }

        [Then(@"status should be OK and response should contain TLE data")]
        public void ThenStatusShouldBeOKAndResponseShouldContainTLEData()
        {
            ScenarioContext.Current.Pending();
        }


        //**//





    }
}
