using System.Text.Json.Serialization;

namespace API_Portofolio.Models.Order.Request
{
    public class StepTwo_DTO
    {
        [JsonPropertyName("keyFeatures")]
        public string KeyFeatures { get; set; } = null!;

        [JsonPropertyName("integrationWithExternalSystems")]
        public string IntegrationWithExternalSystems { get; set; } = null!;

        [JsonPropertyName("supportedPlatforms")]
        public Guid SupportedPlatforms { get; set; }

        [JsonPropertyName("securityRequirements")]
        public string SecurityRequirements { get; set; } = null!;
    }
}
