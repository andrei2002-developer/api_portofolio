using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class TypeOfApplication
    {
        [Key]
        public Guid TypeOfApplication_Id { get; set; }
        public Guid TypeOfApplication_ReferenceCode { get; set; }
        public string TypeOfApplication_Name { get; set; } = null!;

        public TypeOfApplication()
        {
            TypeOfApplication_Id = Guid.NewGuid();
            TypeOfApplication_ReferenceCode = Guid.NewGuid(); 
        }

        public ICollection<StepOneOrder> StepOneOrders { get; set; } = new List<StepOneOrder>();
    }
}
