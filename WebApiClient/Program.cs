using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;
using EasyHttp.Http;

namespace WebApiClient
{
    class Program
    {
        static TokenResponse GetClientToken()
        {
            var client = new TokenClient(
                "http://local.identityserver:5001/connect/token",
                "webentrypoint_silicon",
                "TAKE-THIS-FROM_WINSERVICE");

            var token = client.RequestClientCredentialsAsync("MvcFrontEnd").Result;
            if (token.IsError)
                Console.WriteLine("Error geting Token: {0} ", token.Error);
            return token;
        }

        static TokenResponse GetUserToken()
        {
            var client = new TokenClient(
                "http://local.identityserver:5001/connect/token",
                "webentrypoint_onbehalf",
                "TAKE-THIS-FROM_WINSERVICE");

            var token= client.RequestResourceOwnerPasswordAsync("bob", "TAKE-THIS-FROM_WINSERVICE", "MvcFrontEnd").Result;
            if (token.IsError)
                Console.WriteLine("Error geting Token: {0} ", token.Error);
            return token;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new System.Net.Http.HttpClient();
            if (!response.IsError)
            {
                client.SetBearerToken(response.AccessToken);

                Console.WriteLine(client.GetStringAsync("http://local.frontend/Message/PostBack").Result);
            }
            else Console.WriteLine("Error getting the token: {0}", response.Error);

        }
        static void Main(string[] args)
        {
            EasyHttpReturnsUnsuportedMediaType(GetClientToken());

            //EasyHttpReturnsUnsuportedMediaType(GetUserToken());
            Console.ReadLine();
        }

        private static void EasyHttpReturnsUnsuportedMediaType(TokenResponse tResponse)
        {
            // with UnAuthorized postbackUrl this works fine
            var auth_header = string.Format("Bearer {0}", tResponse.AccessToken);
            var eHttp = new EasyHttp.Http.HttpClient();
            var x = new PostData();
            x.MessageId = "hello";
            eHttp.Request.AddExtraHeader("Authorization", auth_header);
            eHttp.Post("http://local.frontend/Message/PostBack", x, HttpContentTypes.ApplicationJson);
            Console.WriteLine("ehttp returned {0}", eHttp.Response.StatusCode);
        }
    }
    public class PostData
    {
        public string MessageId { get; set; }
    }
}
