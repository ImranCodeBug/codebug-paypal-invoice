using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Codebug.PaypalTokenGenerator
{
    public class PaypalService
    {
        private readonly ITracingService _tracingService;

        private Dictionary<bool, string> _paypalUrl = new Dictionary<bool, string>
        {
            { true, Constants.LiveUrl },
            { false, Constants.SandBoxUrl }
        };

        public PaypalService(ITracingService tracingService)
        {
            _tracingService = tracingService ?? throw new ArgumentNullException(nameof(tracingService));
        }

        public async Task<string> GenerateTokenAsync(string clientId, string secret, bool isLive)
        {
            clientId.EmptyOrThrowError(nameof(clientId));
            secret.EmptyOrThrowError(nameof(secret));

            _tracingService.Trace($"Generating access token for client id {clientId}");

            var credentialBytes = Encoding.UTF8.GetBytes($"{clientId}:{secret}");
            var token = $"Basic ${Convert.ToBase64String(credentialBytes)}";

            using (var httpClient = new HttpClient())            
            {
                httpClient.BaseAddress = new Uri(_paypalUrl[isLive]);
                httpClient.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", token);

                var content = new FormUrlEncodedContent(new[] 
                { 
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });

                var response = await httpClient.PostAsync(Constants.TokenPostFix, content);

                if (response.IsSuccessStatusCode)
                {
                    _tracingService.Trace("Api call successful");
                    var responseStream = await response.Content.ReadAsStringAsync();
                    var tokenResponse = ConvertResponseToTokenResponse(responseStream);
                    return tokenResponse.AccessToken;
                }

                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    _tracingService.Trace($"The api call has been unsuccessful. The response status is {(int)response.StatusCode} The original resposne from the server is {responseString}");
                    throw new InvalidPluginExecutionException("Error occured while retrieving access token from paypal. the full response content has been posted to tracelog.");
                }

            }

            throw new NotImplementedException();
        }

        private TokenResponse ConvertResponseToTokenResponse(string responseContent)
        {
            var serialiser = new DataContractJsonSerializer(typeof(TokenResponse));
            var tokenResponse = new TokenResponse();

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(responseContent)))
            {
                tokenResponse = (TokenResponse)serialiser.ReadObject(memoryStream);
                _tracingService.Trace($"Sucessfully deseralised the response. Token Response is for app {tokenResponse.AppId} which will expire in {tokenResponse.ExpiresInSec} sec");
                memoryStream.Close();                
            }

            return tokenResponse;      
        }
    }
}
