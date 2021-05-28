using RestSharp;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SpecFlowProject2.Steps
{
    [Binding]
    public class EndpointsSteps
    {
        private string requestUrl;
        private IRestResponse actualResponse;

        [Given(@"the url (.*) to (.*) endpoint")]
        public void GivenTheUrlToEndpoint(string url, string functionName)
        {
            requestUrl = url;
        }
        
        [When(@"send to the api.exchangeratesapi.io")]
        public void WhenRequestIsSendRespondIsRecived()
        {
            RestSharpConnector restShartConnector = new RestSharpConnector();
            actualResponse = restShartConnector.SendRequest(requestUrl);
        }

        [Then(@"the API response with error (.*)")]
        public void ThenTheResponseShouldHaveContent(string expectedRequestContent)
        {
            Assert.AreEqual(actualResponse.Content, expectedRequestContent);
        }

        [Then(@"the API response contains (.*) (.*)")]
        public void ThenTheResponseContentShouldContain(string expectedDateFormat, string expectedEuroValue)
        {
            // Planowałem zrobić tu deserializację JSon'a bo wydaje mi się, że to właściwa droga
            // do wykonania eleganckiego sprawdzenia. Niestety nie udało mi się wykonać tego na czas 
            // + w sumie nie jestem pewien czy to właściwa droga.

            string expectedDate = expectedDateFormat.Replace("currentDate", DateTime.Now.ToString("yyyy-MM-dd"));
            int dateExpectedLenght = 17;
            int dateIndex = actualResponse.Content.IndexOf("date");
            Assert.AreEqual(actualResponse.Content.Substring(dateIndex, dateExpectedLenght), expectedDate);

            int eurExpectedLenght = 6;
            int eurIndex = actualResponse.Content.IndexOf("EUR\":");
            Assert.AreEqual(actualResponse.Content.Substring(eurIndex, eurExpectedLenght), expectedEuroValue);
        }
    }
}
