using System.ComponentModel.DataAnnotations;

namespace API_Portofolio.Database.Entities
{
    public class HostingPreference
    {
        [Key]
        public Guid HostingPreference_Id { get; set; }
        public Guid HostingPreference_ReferenceCode { get; set; }
        public string HostingPreference_Name { get; set; } = null!;

        public HostingPreference()
        {
            HostingPreference_Id = Guid.NewGuid();
            HostingPreference_ReferenceCode = Guid.NewGuid();
        }

        public ICollection<StepFourOrder> StepFourOrders { get; set; } = new List<StepFourOrder>();
    }
}
