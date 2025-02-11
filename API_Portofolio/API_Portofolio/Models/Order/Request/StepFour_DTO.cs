using System.Text.Json.Serialization;

namespace API_Portofolio.Models.Order.Request
{
    public class StepFour_DTO
    {
        [JsonPropertyName("deadline")]
        public DateTime? Deadline { get; set; }

        [JsonPropertyName("preferredTechnologies")]
        public string PreferredTechnologies { get; set; } = null!;

        [JsonPropertyName("hostingPreferences")]
        public Guid HostingPreferences { get; set; }

        [JsonPropertyName("collaborationWorkflow")]
        public string CollaborationWorkflow { get; set; } = null!;

        [JsonPropertyName("legalConstraints")]
        public string LegalConstraints { get; set; } = null!;
    }
}
