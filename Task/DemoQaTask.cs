using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System.Text;

namespace ApiBatch2023October.Task
{
    public class DemoQaTask
    {
        [Test]
        public void CreateNewAccountUser()
        {
            var options = new RestClientOptions()
            {
                BaseUrl = new Uri("https://demoqa.com/")
            };
            var client = new RestClient(options);
            var request = new RestRequest("Account/v1/User", Method.Post);

            var payload = new
            {
                userName = "Joe_001" + new Random().Next(1, 999),
                password = "Password001!"
            };
            request.AddJsonBody(payload);

            var response = client.Post(request);
            NewUserModel myDeserializedClass = 
                JsonConvert.DeserializeObject<NewUserModel>(response.Content!)!;
            if (response.IsSuccessful.Equals(true))
            {
                Assert.True((int)response.StatusCode == 201);
                Assert.True(myDeserializedClass.username.Equals(payload.userName));
                Assert.True(myDeserializedClass.userID.Count().Equals(36));
                Assert.True(myDeserializedClass.books.Count().Equals(0));
            }

            var option = new ChromeOptions();
            option.AddArguments("start-maximized");
            var driver = new ChromeDriver(option);
            driver.Navigate().GoToUrl("https://demoqa.com/books");
            driver.FindElement(By.CssSelector("#login")).Click();
            driver.FindElement(By.CssSelector("#userName")).SendKeys(payload.userName);
            driver.FindElement(By.CssSelector("#password")).SendKeys(payload.password);
            driver.FindElement(By.CssSelector("#login")).Click();
            Thread.Sleep(2000);
            Assert.True(driver.FindElement(By.CssSelector("#userName-value"))
                .Text.Equals(payload.userName));


            var options2 = new RestClientOptions()
            {
                BaseUrl = new Uri("https://demoqa.com/")
            };
            var client2 = new RestClient(options);
            var request2 = 
                new RestRequest($"Account/v1/User/{myDeserializedClass.userID}", 
                Method.Get);

            request2.AddHeader("authorization", $"Basic {GetToken.GToken(payload.userName, payload.password)}");
            var response2 = client2.Get<GetUserResponseModel>(request2);
            Assert.True(response2?.username.Equals(payload.userName));
            Assert.True(response2?.userId.Count().Equals(36));
        }

        [Test]
        public void GetAllBookWithSingleMethod()
        {
            var responseData = 
                ApiHelper.SendRequest<BooksResponseModel>("https://demoqa.com/", 
                "BookStore/v1/Books", Method.Get);
            Assert.True(responseData.books[0].isbn.Equals("9781449325862"));
            Assert.True(responseData.books[0].title.Equals("Git Pocket Guide"));
        }

        [Test]
        public void CreatNewUserWithSingleMethod()
        {
            var responseData =
                ApiHelper.SendRequest<GetUserResponseModel>("https://demoqa.com/",
                "Account/v1/User", Method.Post, 
                new { userName = "Joe_001" + new Random().Next(1, 999),
                    password = "Password001!" });
        }

        public class NewUserModel
        {
            public string userID { get; set; }
            public string username { get; set; }
            public List<object> books { get; set; }
        }

        public class GetUserResponseModel
        {
            public string userId { get; set; }
            public string username { get; set; }
            public List<object> books { get; set; }
        }

        public class GetToken
        {
            public static string GToken(string username, string password)
            {
                var token = Convert.ToBase64String(
                 Encoding.ASCII.GetBytes($"{username}:{password}"));
                return token;
            }
        }

        public class Book
        {
            public string isbn { get; set; }
            public string title { get; set; }
            public string subTitle { get; set; }
            public string author { get; set; }
            public DateTime publish_date { get; set; }
            public string publisher { get; set; }
            public int pages { get; set; }
            public string description { get; set; }
            public string website { get; set; }
        }

        public class BooksResponseModel
        {
            public List<Book> books { get; set; }
        }



        public class ApiHelper
        {
            public static T SendRequest<T>(string url, string endPoint, Method method,
                object? payload = null)
            {
                var options = new RestClientOptions()
                {
                    BaseUrl = new Uri(url)
                };
                var client = new RestClient(options);
                var request = new RestRequest(endPoint, method);

                if (method == Method.Post)
                {
                    request?.AddJsonBody(payload);
                }

                var response = client.Execute(request);
                T dataObject =
                JsonConvert.DeserializeObject<T>(response.Content!)!;
                return dataObject;
            }
        }
    }
}
