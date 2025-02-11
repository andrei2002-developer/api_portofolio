using API_Portofolio.Models.Order.Request;
using API_Portofolio.Models.Order.Response;
using ErrorOr;

namespace API_Portofolio.Interface
{
    public interface IOrderServices
    {
        Task<ErrorOr<List<TypeOfApplication_DTO>>> GetTypesOfApplicationAsync();
        Task<ErrorOr<List<SuportedPlatform_DTO>>> GetSuportedPlatformsAsync();
        Task<ErrorOr<List<HostingPreference_DTO>>> GetHostingPreferencesAsync();
        Task<ErrorOr<int>> SendOrderAsync(SendOrder_DTO request,string idUser);
    }
}
