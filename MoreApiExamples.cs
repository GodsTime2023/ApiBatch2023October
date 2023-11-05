using ApiBatch2023October.Task1.ApiPageObject;
using NUnit.Framework;
using System;


namespace ApiBatch2023October
{
    public class MoreApiExamples
    {
        [Test]
        public void GetListOfBooksWithPageObject()
        {
            var pObject = new Pageobject();
            var client = pObject.SetUrl();
            var request = 
                pObject.GetRequest(pObject.baseurl2, 
                "/BookStore/v1/Books", 
                RestSharp.Method.Get);
            var response = pObject.GetResponse();
            var data = pObject.GetDeserializedcontent<BooksResponseModel>(response);
        }

        [Test]
        public void GetListOfBooksWithPageObjectWithChains()
        {
            var pObjectWithChains = new PageobjectWithChains();
            var response = pObjectWithChains.SetUrl()
                .GetRequest(pObjectWithChains.baseurl2,
                "BookStore/v1/Books",
                RestSharp.Method.Get)
                .Build<BooksResponseModel>();
            //var data = pObjectWithChains.GetDeserializedcontent<BooksResponseModel>(response);
        }
    }

    public class MoreApiExamples2
    {
        Pageobject pObject;
        PageobjectWithChains pObjectWithChains;
        public MoreApiExamples2()
        {
            pObject = new Pageobject();
            new PageobjectWithChains();
        }

        [Test]
        public void GetListOfBooksWithPageObject()
        {
            var client = pObject.SetUrl();
            var request =
                pObject.GetRequest(pObject.baseurl2,
                "/BookStore/v1/Books",
                RestSharp.Method.Get);
            var response = pObject.GetResponse();
            var data = pObject.GetDeserializedcontent<BooksResponseModel>(response);
        }

        [Test]
        public void GetListOfBooksWithPageObjectWithChains()
        {
            var response = pObjectWithChains.SetUrl()
                .GetRequest(pObjectWithChains.baseurl2,
                "BookStore/v1/Books",
                RestSharp.Method.Get)
                .Build<BooksResponseModel>();
            Assert.True(null);
        }
    }
}
