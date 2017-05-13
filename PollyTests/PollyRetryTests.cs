using System;
using System.Net;
using NUnit.Framework;
using Polly;
using RestSharp;

namespace PollyTests
{
    [TestFixture]
    public class PollyRetryTests
    {
        [Test]
        public void ExponentialRetry()
        {
            int numRetries = 0;
            var policy = Policy
                .HandleResult<IRestResponse>(r => r.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetry(3, (retryCount, context) =>
                    {
                        numRetries++;
                        return TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                    }
                );
            policy.ExecuteAndCapture(MakeRequest);

            Assert.That(numRetries, Is.EqualTo(3));
        }

        private IRestResponse MakeRequest()
        {
            var client = new RestClient("http://www.google.com/fake_page");
            var restResponse = client.Execute(new RestRequest());
            return restResponse;
        }
    }
}