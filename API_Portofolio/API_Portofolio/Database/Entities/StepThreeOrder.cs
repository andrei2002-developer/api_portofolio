using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class StepThreeOrder
    {
        [Key]
        public Guid StepThreeOrder_Id { get; set; }
        [Required(ErrorMessage = "OrderID is required")]
        [ForeignKey("Order")]
        public Guid Order_Id { get; set; }
        public Order Order { get; set; } = null!;
        public string EndUserDescription { get; set; } = null!;
        public string UsageContext { get; set; } = null!;
        public string DesignPreferences { get; set; } = null!;
        public string AccessibilityNeeds { get; set; } = null!;
        public string CustomizationOptions { get; set; } = null!;
    }
}
