using Newtonsoft.Json;
using RestSharp;

namespace ApiBatch2023October
{
    public class Pageobject
    {
        public RestClient restClient;
        public RestRequest restRequest;
        public RestResponse restResponse;
        public string baseurl = "https://reqres.in";
        public string baseurl2 = "https://demoqa.com";

        public RestClient SetUrl()
        {
            restClient = new RestClient();
            return restClient;
        }

        public RestRequest GetRequest(string baseUrl, string endpoint, Method method)
        {
            restRequest = new RestRequest(baseUrl + endpoint, method);
            restRequest.AddHeader("Accept", "Application/json");
            return restRequest;
        }

        public RestRequest DeleteRequest(string baseUrl, string endpoint, Method method, object payload)
        {
            restRequest = new RestRequest(baseUrl + endpoint, method);
            restRequest.AddHeader("Accept", "Application/json");
            restRequest.AddJsonBody(payload);
            return restRequest;
        }

        public RestRequest PostRequest(string baseUrl, string endpoint, Method method, object payload)
        {
            restRequest = new RestRequest(baseUrl + endpoint, method);
            restRequest.AddHeader("Accept", "Application/json");
            restRequest.AddParameter("application/json", restRequest.AddBody(payload),
                ParameterType.RequestBody);
            return restRequest;
        }

        public RestRequest PostRequest2(string baseUrl, string endpoint, Method method, object payload)
        {
            restRequest = new RestRequest(baseUrl + endpoint, method);
            restRequest.AddJsonBody(payload);
            return restRequest;
        }

        public RestResponse GetResponse()
        {
            return restClient.Execute(restRequest);
        }

        public T? GetDeserializedcontent<T>(RestResponse response)
        {
            var content = response.Content;
            T? deserializedObject = JsonConvert.DeserializeObject<T>(content);
            return deserializedObject;
        }
    }
}
