using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class StepTwoOrder
    {
        [Key]
        public Guid StepTwoOrder_Id { get; set; }
        [Required(ErrorMessage = "OrderID is required")]
        [ForeignKey("Order")]
        public Guid Order_Id { get; set; }
        public Order Order { get; set; } = null!;
        public string KeyFeatures { get; set; } = null!;
        public string IntegrationWithExternalSystems { get; set; } = null!;
        [Required(ErrorMessage = "SuportedPlatformId is required")]
        [ForeignKey("SuportedPlatform")]
        public Guid SuportedPlatform_Id { get; set; }
        public SuportedPlatform SuportedPlatform { get; set; } = null!;
        public string SecurityRequirements { get;set; } = null!;
    }
}
