using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class StepOneOrder
    {
        [Key]
        public Guid StepOneOrder_Id { get; set; }

        [Required(ErrorMessage = "OrderID is required")]
        [ForeignKey("Order")]
        public Guid Order_Id { get; set; }
        public Order Order { get; set; } = null!;
        [Required(ErrorMessage = "TypeOfApplication Id is required")]
        [ForeignKey("TypeOfApplication")]
        public Guid TypeOfApplication_Id { get; set; }
        public TypeOfApplication TypeOfApplication { get; set; } = null!;   
        public string TargetAudience { get; set; } = null!;
        public string CurrentChallengesOrProblems { get; set; } = null!;
        public string MainPurpose { get; set; } = null!;
    }
}
