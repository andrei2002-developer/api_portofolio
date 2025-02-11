using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Portofolio.Database.Entities
{
    public class Order
    {
        [Key]
        public Guid Order_Id { get; set; }

        public int OrderNumber { get; set; }

        [Required(ErrorMessage = "ReferenceCode is required")]
        public Guid Order_ReferenceCode { get; set; }

        [Required(ErrorMessage = "IdUser is required")]
        [ForeignKey("Account")]
        public string Order_IdUser { get; set; } = null!;

        public Account Account { get; set; } = null!;
        public StepOneOrder StepOneOrder { get; set; } = null!;
        public StepTwoOrder StepTwoOrder { get; set; } = null!;
        public StepThreeOrder StepThreeOrder { get; set; } = null!;
        public StepFourOrder StepFourOrder { get; set; } = null!;
    }
}
