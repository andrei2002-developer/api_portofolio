using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class SuportedPlatform
    {
        [Key]
        public Guid SuportedPlatform_Id { get; set; }
        public Guid SuportedPlatform_ReferenceCode { get; set; }
        public string SuportedPlatform_Name { get; set; }

        public SuportedPlatform()
        {
            SuportedPlatform_Id = Guid.NewGuid();
            SuportedPlatform_ReferenceCode = Guid.NewGuid();
        }

        public ICollection<StepTwoOrder> StepTwoOrders { get; set; } = new List<StepTwoOrder>();
    }
}
