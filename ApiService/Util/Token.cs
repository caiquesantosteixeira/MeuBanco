using Newtonsoft.Json;

namespace ApiService.Util
{
    public class Token
    {
        
        [JsonProperty("Access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("Data")]
        public Data Data { get ; set ;}
   
    }
}
