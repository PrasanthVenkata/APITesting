using System;
using System.Collections.Generic;
using Dynamitey.DynamicObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Serialization.Json;
using log4net;
using log4net.Config;
using FluentAssertions;

namespace SpecFlowDemos
{
    [TestClass]
    public class UnitTest1
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [TestMethod]
        public void TestMethod1()
        {
            var client = new RestClient("http://localhost:3000/");           
            var request = new RestRequest("posts/{post}",Method.GET);
            request.AddUrlSegment("post", 1);
            var jsonresponse = client.Execute(request);

            var deserialiserObject = new JsonDeserializer();
            var dictObj= deserialiserObject.Deserialize<Dictionary<string, string>>(jsonresponse);
            
            string response = dictObj["id"].ToString();
            log.Info("Welcome to log4net logging");
            log.Info("Value of id is " + response);
            response.Should().NotBeNull("The value of id should be not null but found :" +response );

            response = dictObj["title"].ToString();
            log.Info("Value of title is " + response);
            response.Should().NotBeNull("The value of title should be not null but found :" + response);


        }
    }
}
