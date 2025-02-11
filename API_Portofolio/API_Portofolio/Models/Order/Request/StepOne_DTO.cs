using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Portofolio.Models.Order.Request
{
    public class StepOne_DTO
    {
        [JsonPropertyName("applicationType")]
        public Guid ApplicationType { get; set; }

        [JsonPropertyName("targetAudience")]
        public string TargetAudience { get; set; } = null!;

        [JsonPropertyName("challenges")]
        public string Challenges { get; set; } = null!;

        [JsonPropertyName("purpose")]
        public string Purpose { get; set; } = null!;
    }
}
