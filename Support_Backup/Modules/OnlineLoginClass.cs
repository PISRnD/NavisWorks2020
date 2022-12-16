using System;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
/*This is using in the Login process - 2*/
using Amazon.Runtime;
using RestSharp;
using System.Net;
using ThirdParty.Json.LitJson;

namespace PivdcNavisworksSupportModule
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

        public Task<bool> ValidateLogin(string username, string password)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var credential = new { email = username, password = password };
                string credentialData = Newtonsoft.Json.JsonConvert.SerializeObject(credential);
                var client = new RestClient("https://license.pivdc.com/login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = credentialData;
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK && !response.Content.Contains("Could not verify user"))
                {
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Task.FromResult(false);
        }
    }
}