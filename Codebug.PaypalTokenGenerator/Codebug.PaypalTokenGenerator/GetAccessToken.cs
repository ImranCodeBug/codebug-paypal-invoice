using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace Codebug.PaypalTokenGenerator
{
    public class GetAccessToken : CodeActivity
    {
        [Input("Use live Paypal")]
        [RequiredArgument]
        public InArgument<bool> UseLive { get; set; }

        [Input("Client Id")]
        [RequiredArgument]
        public InArgument<string> ClientId { get; set; }

        [Input("Secret")]
        [RequiredArgument]
        public InArgument<string> Secret { get; set; }

        [Output("Access Token")]
        public OutArgument<string> AccessToken { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var tracingService = context.GetExtension<ITracingService>();
            tracingService.Trace($"Started the process to get the token at {DateTime.Now.ToLongTimeString()}");

            var clientId = ClientId.Get(context);
            var isLive = UseLive.Get(context);
            var secret = Secret.Get(context);

            var paypalService = new PaypalService(tracingService);

            tracingService.Trace("Entering token generation logic.");
            var token = paypalService.GenerateTokenAsync(clientId, secret, isLive).Result;

            AccessToken.Set(context, token);

            tracingService.Trace($"Finishing execution at {DateTime.Now.ToLongTimeString()}");
        }
    }
}
