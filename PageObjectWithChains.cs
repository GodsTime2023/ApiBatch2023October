namespace ApiBatch2023October
{
    using RestSharp;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    namespace Task1.ApiPageObject
    {
        public class PageobjectWithChains
        {
            public RestClient restClient;
            public RestRequest restRequest;
            public string baseurl = "https://demoqa.com/";
            public string baseurl2 = "https://demoqa.com/";

            public PageobjectWithChains SetUrl()
            {
                restClient = new RestClient();
                return this;
            }

            public PageobjectWithChains GetRequest(string baseurl, string endpoint, Method method)
            {
                restRequest = new RestRequest(baseurl + endpoint, method);
                restRequest.AddHeader("Accept", "Application/json");
                return this;
            }

            public PageobjectWithChains DeleteRequest(string baseurl, string endpoint, Method method, object payload)
            {
                restRequest = new RestRequest(baseurl + endpoint, method);
                restRequest.AddHeader("Accept", "Application/json");
                restRequest.AddJsonBody(payload);
                return this;
            }

            public PageobjectWithChains PostRequest(string baseurl, string endpoint, Method method, object payload)
            {
                restRequest = new RestRequest(baseurl + endpoint, method);
                restRequest.AddHeader("Accept", "Application/json");
                restRequest.AddParameter("application/json", restRequest.AddJsonBody(payload),
                    ParameterType.RequestBody);
                return this;
            }

            public RestResponse GetResponse(RestClient client, RestRequest request)
            {
                return client.Execute(request);
            }

            public RestResponse GetmyResponse()
            {
                return restClient.Execute(restRequest);
            }

            public T GetDeserializedcontent<T>(RestResponse response)
            {
                var content = response.Content;
                T deserializedObject = JsonConvert.DeserializeObject<T>(content);
                return deserializedObject;
            }

            public T Build<T>()
            {
                var resp = restClient.Execute(restRequest);
                var content = resp.Content;
                T deserializedObject = JsonConvert.DeserializeObject<T>(content);
                return deserializedObject;
            }

            public RestResponse SendRequest(string baseurl, string endpoint, Method method,
                object payload = null, Dictionary<string, string> header = null,
                Dictionary<string, string> param = null)
            {
                restClient = new RestClient();
                restRequest = new RestRequest(baseurl + endpoint, method);
                if (payload != null)
                {
                    restRequest.AddBody(payload);
                }

                if (header != null)
                {
                    foreach (var key in header.Keys)
                    {
                        restRequest.AddParameter(key, header[key]);
                    }
                }

                if (param != null)
                {
                    foreach (var key in param.Keys)
                    {
                        restRequest.AddParameter(key, param[key]);
                    }
                }
                return restClient.Execute(restRequest);
            }

            public T SendRequest<T>(string baseurl, string endpoint, Method method,
                object payload = null, Dictionary<string, string> header = null,
                Dictionary<string, string> param = null)
            {
                restClient = new RestClient();
                restRequest = new RestRequest(baseurl + endpoint, method);
                if (payload != null)
                {
                    restRequest.AddBody(payload);
                }

                if (header != null)
                {
                    foreach (var key in header.Keys)
                    {
                        restRequest.AddParameter(key, header[key]);
                    }
                }

                if (param != null)
                {
                    foreach (var key in param.Keys)
                    {
                        restRequest.AddParameter(key, param[key]);
                    }
                }
                return restClient.Execute<T>(restRequest).Data;
            }
        }
    }

}
