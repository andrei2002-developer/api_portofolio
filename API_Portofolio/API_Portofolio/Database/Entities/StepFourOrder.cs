using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class StepFourOrder
    {
        [Key]
        public Guid StepFourOrder_Id { get; set; }
        [Required(ErrorMessage = "OrderID is required")]
        [ForeignKey("Order")]
        public Guid Order_Id { get; set; }
        public Order Order { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public string PreferredTechnologies { get; set; } = null!;
        [Required(ErrorMessage = "HostingPreferenceId is required")]
        [ForeignKey("HostingPreference")]
        public Guid HostingPreference_Id { get; set; }
        public HostingPreference HostingPreference { get; set; } = null!;
        public string CollaborationWorkflow { get; set;} = null!;
        public string LegalConstraints { get; set; } = null!;
    }
}
