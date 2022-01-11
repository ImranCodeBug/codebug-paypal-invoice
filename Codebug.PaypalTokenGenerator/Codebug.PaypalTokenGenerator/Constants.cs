namespace Codebug.PaypalTokenGenerator
{
    public static class Constants
    {
        public const string LiveUrl = "https://api-m.paypal.com";
        public const string SandBoxUrl = "https://api-m.sandbox.paypal.com";
        public const string TokenPostFix = "/v1/oauth2/token";
        public const string AccessToken = "access_token";
        public const string TokenType = "token_type";
        public const string AppId = "app_id";
        public const string ExpiresIn = "expires_in";
    }
}
