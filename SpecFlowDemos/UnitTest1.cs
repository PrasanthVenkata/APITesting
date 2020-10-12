using System;
using System.Collections.Generic;
using Dynamitey.DynamicObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Serialization.Json;
using log4net;
using log4net.Config;
using FluentAssertions;
using System.Configuration;

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
            var request = new RestRequest("posts/{post}", Method.GET);
            request.AddUrlSegment("post", 1);
            var jsonresponse = client.Execute(request);

            var deserialiserObject = new JsonDeserializer();
            var dictObj = deserialiserObject.Deserialize<Dictionary<string, string>>(jsonresponse);

            string response = dictObj["id"].ToString();
            log.Info("Welcome to log4net logging");
            log.Info("Value of id is " + response);
            response.Should().NotBeNull("The value of id should be not null but found :" + response);

            response = dictObj["title"].ToString();
            log.Info("Value of title is " + response);
            response.Should().NotBeNull("The value of title should be not null but found :" + response);

            //we can use the JObject for deserialiation


        }

        [TestMethod]
        // here the class is anonymous
        public void PostMethodCallAnonymous()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/", Method.POST);
            request.AddJsonBody(new { name = "New added post from API code" });
            client.Execute(request);
        }



        [TestMethod]
        // here the class is strongly typed - new Posts class
        public void PostMethodCallTyped()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/", Method.POST);
            // creating a new record
            request.AddJsonBody(new Posts {  author = "prasanth", title = "Testing api from typed method code" }); ;
            var response = client.Execute(request);
            var deserObj = new JsonDeserializer();
            var outputDict = deserObj.Deserialize<Dictionary<string, string>>(response);
            string authorName =  outputDict["author"];
            log.Debug("Author name from newly added record is " + authorName);
            authorName.Should().Be("prasanth");
        }



        [TestMethod]
        // here the class is strongly typed - new Posts class
        public void PostMethodCallTypedWithClassName()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/", Method.POST);
            // creating a new record
            request.AddJsonBody(new Posts { author = "prasanth", title = "Testing api from typed method code" }); ;
            var response = client.Execute<Posts>(request).Data;
            string authorName = response.author;
            log.Debug("Author name from newly added record is " + authorName);
            authorName.Should().Be("prasanth");
        }
    }
}