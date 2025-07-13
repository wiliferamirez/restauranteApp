using System.Text.Json.Serialization;

namespace auth.DTOs
{
    public class RecaptchaVerifyResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("challenge_ts")]
        public DateTime ChallengeTs { get; set; }
        [JsonPropertyName("hostname")]
        public string Hostname { get; set; } = null!;
        [JsonPropertyName("error-codes")]
        public List<string>? ErrorCodes { get; set; }
    }
}
