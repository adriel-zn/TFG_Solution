using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TFG.Test.CustomerTest
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public async Task Test_GetTokenAuthentication()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            using (var server = new HttpServer(config))
            {

                var client = new HttpClient(server);

                string url = "https://localhost:44330/api/v2/users/authenticate";

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Post,
                    Content = new StringContent("{\"username\":\"Username1\",\"password\":\"Password1\"}", Encoding.UTF8, "application/json")
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.SendAsync(request))
                {
                    var test = response;
                }
            }
        }

        [TestMethod]
        public async Task  Test_GetAllCustomer()
        {
            //Arrange
            var config = new HttpConfiguration();
            //configure web api
            config.MapHttpAttributeRoutes();

            //Act
            using (var server = new HttpServer(config))
            {

                var client = new HttpClient(server);

                string url = "https://localhost:44330/api/v2/customers/";

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.SendAsync(request))
                {
                    //Assert
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
            }

        }
    }
}
