using NUnit.Framework;
using RestSharp;

namespace ApiBatch2023October
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var options = new RestClientOptions("https://reqres.in/")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/users?page=2", Method.Get);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        [Test]
        public void Test5WithoutAsync()
        {
            var client = new RestClient(); //Client object
            var request =
                new RestRequest("https://reqres.in/api/users?page=2"); //Request object
            RestResponse response = client.Execute(request); //Response object
            Assert.True(response.Headers?.Count() == 16);
        }

        [Test]
        public void Test2WithoutAsync()
        {
            var options = new RestClientOptions("https://reqres.in/")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/users?page=2", Method.Get);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        [Test]
        public void Test3WithoutAsync()
        {
            var client = new RestClient("https://reqres.in/");
            var request = new RestRequest("api/users?page=2", Method.Get);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        [Test]
        public void Test4WithoutAsync()
        {
            var client = new RestClient();
            var request = 
                new RestRequest("https://reqres.in/api/users?page=2", Method.Get);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        [Test]
        public void Test2()
        {
            var options = new RestClientOptions()
            {
                MaxTimeout = -1,
                BaseUrl = new Uri("https://reqres.in/")
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/users?page=2", Method.Get);
            RestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}