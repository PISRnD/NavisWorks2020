using System.Net.Http;
using System;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
/*This is using in the Login process - 2*/
using Amazon.Runtime;
using PivdcSupportModel;
using Newtonsoft.Json;
using PivdcSupport.Model;
using RestSharp;
using System.Net;

namespace PivdcSupportModule
{
    public class OnlineLoginClass
    {
        private string POOL_ID = "ap-south-1_YGJ2RAT1e";
        private string CLIENTAPP_ID = "6vorla84a6ld40cai1com2jm2h";
        string AccessKeyId = "AKIA6RIVBYRK6XMRHEHY";
        string SecretAccessKey = "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";
        AmazonCognitoIdentityProviderClient provider { get; set; }
        CognitoUserPool userPool { get; set; }

        public OnlineLoginClass()
        {
#if false
            if (System.IO.File.Exists(string.Format("{0}\\AWSSDK.Core.dll", SupportDatas.AssemblyDirectory)))
            {
                System.Reflection.Assembly.LoadFrom(string.Format("{0}\\AWSSDK.Core.dll", SupportDatas.AssemblyDirectory));
            }
            else
            {
                new System.Exception(string.Format("{0}\\AWSSDK.Core.dll, Not found", SupportDatas.AssemblyDirectory))
                    .ErrorLogEntry(System.Reflection.MethodBase.GetCurrentMethod().Name);
            } 
#endif
        }

        public async Task<CognitoUser> ValidateUser(string username, string password)
        {
            try
            {
                /*Login process - 1*/
#if true
                provider = new AmazonCognitoIdentityProviderClient(AccessKeyId, SecretAccessKey, Amazon.RegionEndpoint.APSouth1);
                userPool = new CognitoUserPool(this.POOL_ID, this.CLIENTAPP_ID, provider);
                CognitoUser user = new CognitoUser(username, this.CLIENTAPP_ID, userPool, provider);
                InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
                {
                    Password = password
                };
                AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
                if (authResponse.AuthenticationResult != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
#endif
                /*Login process - 1*/

                /*Login process - 2*/
#if false
                AmazonCognitoIdentityProviderConfig IdentityProvider = new AmazonCognitoIdentityProviderConfig();
                IdentityProvider.RegionEndpoint = Amazon.RegionEndpoint.APSouth1;
                IdentityProvider.DefaultConfigurationMode = DefaultConfigurationMode.Auto;
                provider = new AmazonCognitoIdentityProviderClient(AccessKeyId, SecretAccessKey, IdentityProvider);
                userPool = new CognitoUserPool(this.POOL_ID, this.CLIENTAPP_ID, provider);
                CognitoUser user = new CognitoUser(username, this.CLIENTAPP_ID, userPool, provider);
                InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
                {
                    Password = password
                };
                AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
                if (authResponse.AuthenticationResult != null)
                {
                    return user;
                }
                else
                {
                    return null;
                } 
#endif
                /*Login process - 2*/

                /*Login process - 3*/

                /*Login process - 3*/
            }
            catch (System.Exception ex)
            {
                ex.ErrorLogEntry(System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        //public async Task<bool> ValidateLogin(string username, string password)
        //{
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("https://api.p360.pinnacleinfotech.com/api/Authentication/CheckUser");
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            HttpResponseMessage response = await client.GetAsync("?username=" + username + "&password=" + password);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string _callback = await response.Content.ReadAsStringAsync();
        //                if (string.IsNullOrEmpty(_callback) || _callback.Contains("Incorrect"))
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ErrorLogEntry(System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //    return false;
        //}

        public async Task<bool> ValidateLogin(string username, string password)
        {
            Credentials credentials = new Credentials();
            credentials.email = username;
            credentials.password = password;
            string jsonData = JsonConvert.SerializeObject(credentials);
            var client = new RestClient("https://license.preprod.pivdc.com/login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = jsonData;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK && !response.Content.Contains("Could not verify user"))
            {
                return true;
            }



            return false;



        }
    }
}