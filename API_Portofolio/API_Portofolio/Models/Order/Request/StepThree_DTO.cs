using System.Text.Json.Serialization;

namespace API_Portofolio.Models.Order.Request
{
    public class StepThree_DTO
    {
        [JsonPropertyName("endUserDescription")]
        public string EndUserDescription { get; set; } = null!;

        [JsonPropertyName("usageContext")]
        public string UsageContext { get; set; } = null!;

        [JsonPropertyName("designPreferences")]
        public string DesignPreferences { get; set; } = null!;

        [JsonPropertyName("accessibilityNeeds")]
        public string AccessibilityNeeds { get; set; } = null!;

        [JsonPropertyName("customizationOptions")]
        public string CustomizationOptions { get; set; } = null!;
    }
}
