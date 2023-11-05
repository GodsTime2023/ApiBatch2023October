using NUnit.Framework;
using RestSharp;

namespace ApiBatch2023October
{
    public class TestExamples
    {
        [Test]
        public void GetAllUsers()
        {
            var options = new RestClientOptions()
            {
                MaxTimeout = -1,
                BaseUrl = new Uri("https://reqres.in/")
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/users?page=2", Method.Get);
            //RestResponse response = client.Execute(request);
            //if (response.IsSuccessful == true)
            //    Assert.True(response.StatusDescription == "OK");

            //Example1
            //ResponseModel? myDeserializedClass =
            //    JsonConvert.DeserializeObject<ResponseModel>(response.Content!);

            //Example2
            GellAllResponseModel myDeserializedClass = client.Execute<GellAllResponseModel>(request).Data!;

            Assert.True(myDeserializedClass?.data.Count.Equals(6));
            Assert.True(myDeserializedClass?.page.Equals(2));
            Assert.True(myDeserializedClass?.per_page.Equals(6));
            Assert.True(myDeserializedClass?.total.Equals(12));
            Assert.True(myDeserializedClass?.total_pages.Equals(2));
            Assert.True(myDeserializedClass?
                .support.text
                .Equals("To keep ReqRes free, contributions towards server costs are appreciated!"));
            Assert.True(myDeserializedClass?
                .support.url
                .Equals("https://reqres.in/#support-heading"));

            Assert.True(myDeserializedClass?.data[0].avatar.Equals("https://reqres.in/img/faces/7-image.jpg"));
            Assert.True(myDeserializedClass?.data[0].email.Equals("michael.lawson@reqres.in"));
            Assert.True(myDeserializedClass?.data[0].first_name.Equals("Michael"));
            Assert.True(myDeserializedClass?.data[0].id.Equals(7));
            Assert.True(myDeserializedClass?.data[0].last_name.Equals("Lawson"));
        }

        [Test]
        public void GetSingleUser()
        {
            var options = new RestClientOptions()
            {
                MaxTimeout = -1,
                BaseUrl = new Uri("https://reqres.in/")
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/users/2", Method.Get);
            
            RestResponse response = client.Execute(request); //200
            Assert.True((int)response.StatusCode == 200);
            GetSingleResponseModel myDeserializedClass = client.Execute<GetSingleResponseModel>(request).Data!;

            Assert.True(myDeserializedClass?
                .support.text
                .Equals("To keep ReqRes free, contributions towards server costs are appreciated!"));
            Assert.True(myDeserializedClass?
                .support.url
                .Equals("https://reqres.in/#support-heading"));

            Assert.True(myDeserializedClass?.data.avatar.Equals("https://reqres.in/img/faces/2-image.jpg"));
            Assert.True(myDeserializedClass?.data.email.Equals("janet.weaver@reqres.in"));
            Assert.True(myDeserializedClass?.data.first_name.Equals("Janet"));
            Assert.True(myDeserializedClass?.data.id.Equals(2));
            Assert.True(myDeserializedClass?.data.last_name.Equals("Weaver"));
        }

        [Test]
        public void CreateNewUser()
        {
            var options = new RestClientOptions()
            {
                MaxTimeout = -1,
                BaseUrl = new Uri("https://reqres.in/")
            };
            var client = new RestClient(options);
            var request = new RestRequest("api/users", Method.Post);

            var name = "Joseph" + new Random().Next(0, 999);
            var job = "Test Engineer" + new Random().Next(0, 999);

            //var payload = new NewUser
            //{
            //    name = name,
            //    job = job
            //};

            var payload = new{
                name = name,
                job = job };

            request.AddJsonBody(payload);
            RestResponse response = client.Execute(request); //201
            Assert.True((int)response.StatusCode == 201);
            NewUserResponseModel myDeserializedClass = 
                client.Execute<NewUserResponseModel>(request).Data!;
            Assert.True(myDeserializedClass.name.Equals(name));
            Assert.True(myDeserializedClass.job.Equals(job));
            Assert.NotNull(myDeserializedClass.id);
            Assert.NotNull(myDeserializedClass.createdAt);
        }

        [Test]
        public void GetAllBooks()
        {
            var options = new RestClientOptions()
            {
                MaxTimeout = -1,
                BaseUrl = new Uri("https://demoqa.com/")
            };
            var client = new RestClient(options);
            var request = new RestRequest("BookStore/v1/Books", Method.Get);
            BooksResponseModel response = client.Execute<BooksResponseModel>(request).Data!;

            Assert.True(response.books[0].isbn.Equals("9781449325862"));
            Assert.True(response.books[0].title.Equals("Git Pocket Guide"));
        }

    }
}
