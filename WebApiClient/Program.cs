using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;
using EasyHttp.Http;
using Newtonsoft.Json;

namespace WebApiClient
{
    class Program
    {
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
        static TokenResponse GetSiliconClientToken()
        {
            var client = new TokenClient(
                "https://local.identityserver:5000/connect/token",
                "dev-silicon",
                "dev-silicon-secret");

            var token = client.RequestClientCredentialsAsync("MvcFrontEnd").Result;
            if (token.IsError)
                Console.WriteLine("Error geting Token: {0} ", token.Error);
            return token;
        }

        static void Main(string[] args)
        {
            var token = GetSiliconClientToken();
            //var behalfToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjFqNV9UQnJEMmxpR002VGVES0ZWTlhqenpWcyIsImtpZCI6IjFqNV9UQnJEMmxpR002VGVES0ZWTlhqenpWcyJ9.eyJpc3MiOiJodHRwczovL2xvY2FsLmlkZW50aXR5c2VydmVyOjUwMDAiLCJhdWQiOiJodHRwczovL2xvY2FsLmlkZW50aXR5c2VydmVyOjUwMDAvcmVzb3VyY2VzIiwiZXhwIjoxNDg4NTYxNDg1LCJuYmYiOjE0ODg1NjE0ODQsImNsaWVudF9pZCI6Im12Y0FqYXgiLCJzY29wZSI6Ik12Y0Zyb250RW5kIiwic3ViIjoiYm9iQGhpdG1hc3Rlci5jb20iLCJhdXRoX3RpbWUiOjE0ODg1NjE0ODMsImlkcCI6Imlkc3J2IiwiYW1yIjpbInBhc3N3b3JkIl19.hjeoNPhOAKUzUAKhSvZNl3hrvGa1D1O1ZPH35jNLSTBbZoyW7zRSJLJC9ipAJmpau80eICY1-fmIMUCauY-TBcT_5Y3Ldswvxkt-_Ls9ruykc8YpEX6S2lXqxaUHJEUpRNk6lTPE0EbAsBgJuN_c96QW5EXw3Vv-Wv8VhdJW13OcYCV0ChKxShEsjKqv42vw4JykgDr8fLlToI6NMvKaLiYrZpwdUuysh3PGchJkl6GE76y7jnq9cexOiWWAY2hAOXJnDAEXx1q1ZKi1EwuyPKPJtVTVejyK9rK438mfjUSgIXYPt9KDgtTvYoRcwKNTUbturClYlAcLNbXQXuBmjg";
            var oldSiliconToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjFqNV9UQnJEMmxpR002VGVES0ZWTlhqenpWcyIsImtpZCI6IjFqNV9UQnJEMmxpR002VGVES0ZWTlhqenpWcyJ9.eyJpc3MiOiJodHRwczovL2xvY2FsLmlkZW50aXR5c2VydmVyOjUwMDAiLCJhdWQiOiJodHRwczovL2xvY2FsLmlkZW50aXR5c2VydmVyOjUwMDAvcmVzb3VyY2VzIiwiZXhwIjoxNDg4NTYzNDA0LCJuYmYiOjE0ODg1NjM0MDMsImNsaWVudF9pZCI6ImRldi1zaWxpY29uIiwic2NvcGUiOiJNdmNGcm9udEVuZCJ9.IPZ_PNcqTAWmYU5PJwc74AVxNxBjzytaSow5UuIYhLcyM0pyJ_JalWkOD9mz6aqBc9ri3CJpVqDHutPDCakU4eRFGZHVGrP79Kk3fXNoYEASJ6wE_3zfrukhoXiesR6R5tI-8aQVMUMKcCvueZQ1WaEPytJDs3Th-iQDIAFl-IvA-wiRX8gWZLhF93rxi0Bb1H38UePRLzwky3XKyI2U_S8UPthyRESAjK40GEloxhMDpPSpYj8zffksqKQhy_Vcn_1XHzT16UnaZizaNMkqV0jfykbI99fvlhksHllwbN5z-IdVA4-CgyxZt09sjU6xUlPgPUIk9G6ESwHWUbHhkg";
            WriteExpired("siliconToken", token.AccessToken);
            //WriteExpired("behalfToken", behalfToken);

            PostBackUsingEasyHttp(token.AccessToken);
            PostBackUsingHttpClient(token.AccessToken);

            //EasyHttpReturnsUnsuportedMediaType(GetUserToken());
            Console.ReadLine();
        }

        static TokenResponse GetSiliconOnBehalfOfUserToken()
        {
            var client = new TokenClient(
                "https://local.identityserver:5000/connect/token",
                "mvcAjax",
                "mvcAjax");

            var token = client.RequestResourceOwnerPasswordAsync("bob", "dev-bob-password", "MvcFrontEnd").Result;
            if (token.IsError)
                Console.WriteLine("Error geting Token: {0} ", token.Error);
            return token;
        }
        private static void PostBackUsingEasyHttp(string accesstoken)
        {
            // with UnAuthorized postbackUrl this works fine
            //var auth_header = string.Format("Bearer {0}", tResponse.AccessToken);
            var auth_header = string.Format("Bearer {0}", accesstoken);
            var eHttp = new EasyHttp.Http.HttpClient();
            
            eHttp.Request.AddExtraHeader("Authorization", auth_header);

            var data = new PostbackData();
            eHttp.Post("https://local.frontend/Message/PostBack", data, HttpContentTypes.ApplicationJson);
            Console.WriteLine("ehttp returned {0}", eHttp.Response.StatusCode);
        }
  
        private static void PostBackUsingHttpClient(string accessToken)
        {
            var data = new PostbackData();
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var byteContent = SerializeDataBag(data);
                var response = client.PostAsync("https://local.frontend/Message/PostBack", byteContent).Result;
                Console.WriteLine("HttpClient returned {0}", response.StatusCode);
            }
        }

        private static ByteArrayContent SerializeDataBag(PostbackData msgObj)
        {
            var stringContent = JsonConvert.SerializeObject(msgObj);
            Console.WriteLine("HttpClient sends {0}", stringContent);
            var bytes = System.Text.Encoding.UTF8.GetBytes(stringContent);
            return new ByteArrayContent(bytes);
        }
        private static void WriteExpired(string tokenName, string jwt)
        {
            // #PastedCode
            //
            //=> Retrieve the 2nd part of the JWT token (this the JWT payload)
            var payloadBytes = jwt.Split('.')[1];

            //=> Padding the raw payload with "=" chars to reach a length that is multiple of 4
            var mod4 = payloadBytes.Length % 4;
            if (mod4 > 0) payloadBytes += new string('=', 4 - mod4);

            //=> Decoding the base64 string
            var payloadBytesDecoded = Convert.FromBase64String(payloadBytes);

            //=> Retrieve the "exp" property of the payload's JSON
            var payloadStr = Encoding.UTF8.GetString(payloadBytesDecoded, 0, payloadBytesDecoded.Length);
            var payload = JsonConvert.DeserializeAnonymousType(payloadStr, new { Exp = 0UL });

            Console.WriteLine("Valid: token {0} is valid until {1}.", tokenName, new DateTime(1970, 1, 1, 0, 1, 0).AddSeconds(payload.Exp).ToLongTimeString());

            //=> Comparing the exp timestamp to the current timestamp
            var currentTimestamp = (ulong)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

        }
    }
        public class PostbackData
    {
        public PostbackData()
        {
            MessageId = "hello from Client";
            Content = "not much";
            Started = DateTime.Today;
            Duration = "0";
            UserName = "Bob";
        }
        public string MessageId { get; set; }
        public string UserName { get; set; }
        public DateTime Started { get; set; }

        public string Duration { get; set; }
        public string Content { get; set; }

    }


}
