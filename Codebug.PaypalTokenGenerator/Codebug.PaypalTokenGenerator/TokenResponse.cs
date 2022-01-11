using System.Runtime.Serialization;

namespace Codebug.PaypalTokenGenerator
{
    [DataContract]
    public class TokenResponse
    {
        [DataMember(Name = Constants.AccessToken)]
        public string AccessToken { get; set; }
        [DataMember(Name = Constants.TokenType)]
        public string TokenType { get; set; }
        [DataMember(Name = Constants.AppId)]
        public string AppId { get; set; }
        [DataMember(Name = Constants.ExpiresIn)]
        public int ExpiresInSec { get; set; }        
    }
}
