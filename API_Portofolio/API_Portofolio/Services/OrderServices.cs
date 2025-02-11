using API_Portofolio.Database;
using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;
using API_Portofolio.Models.Order.Request;
using API_Portofolio.Models.Order.Response;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Portofolio.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<OrderServices> _logger;

        public OrderServices(DatabaseContext databaseContext, ILogger<OrderServices> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public async Task<ErrorOr<List<TypeOfApplication_DTO>>> GetTypesOfApplicationAsync()
        {
            try
            {
                // Selectează doar câmpurile necesare pentru DTO
                var result = await _databaseContext.TypeOfApplications
                    .AsNoTracking() // Utilizare AsNoTracking pentru performanță în cazul în care nu modifici entitățile
                    .Select(u => new TypeOfApplication_DTO
                    {
                        ReferenceCode = u.TypeOfApplication_ReferenceCode,
                        Name = u.TypeOfApplication_Name
                    })
                    .ToListAsync();

                if (result == null || !result.Any())
                {
                    return Error.Failure(description: "No applications found.");
                }

                _logger.LogInformation("Lista Tip Aplicatie preluata cu succes!");
                return result; // Returnează lista de DTO-uri
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching types of applications.");
                return Error.Failure(description: "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ErrorOr<List<SuportedPlatform_DTO>>> GetSuportedPlatformsAsync()
        {
            try
            {
                // Selectează doar câmpurile necesare pentru DTO
                var result = await _databaseContext.SuportedPlatforms
                    .AsNoTracking() // Utilizare AsNoTracking pentru performanță în cazul în care nu modifici entitățile
                    .Select(u => new SuportedPlatform_DTO
                    {
                        ReferenceCode = u.SuportedPlatform_ReferenceCode,
                        Name = u.SuportedPlatform_Name
                    })
                    .ToListAsync();

                if (result == null || !result.Any())
                {
                    return Error.Failure(description: "No applications found.");
                }

                return result; // Returnează lista de DTO-uri
            }
            catch (Exception ex)
            {
                // Logare eroare și returnare mesaj generic (pentru securitate)
                //_logger.LogError(ex, "An error occurred while fetching types of applications.");
                return Error.Failure(description: "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ErrorOr<List<HostingPreference_DTO>>> GetHostingPreferencesAsync()
        {
            try
            {
                // Selectează doar câmpurile necesare pentru DTO
                var result = await _databaseContext.HostingPreferences
                    .AsNoTracking() // Utilizare AsNoTracking pentru performanță în cazul în care nu modifici entitățile
                    .Select(u => new HostingPreference_DTO
                    {
                        ReferenceCode = u.HostingPreference_ReferenceCode,
                        Name = u.HostingPreference_Name
                    })
                    .ToListAsync();

                if (result == null || !result.Any())
                {
                    return Error.Failure(description: "No applications found.");
                }

                return result; // Returnează lista de DTO-uri
            }
            catch (Exception ex)
            {
                // Logare eroare și returnare mesaj generic (pentru securitate)
                //_logger.LogError(ex, "An error occurred while fetching types of applications.");
                return Error.Failure(description: "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ErrorOr<int>> SendOrderAsync(SendOrder_DTO request,string idUser)
        {
            try
            {
                var idTypeOfApplication = await _databaseContext.TypeOfApplications.FirstOrDefaultAsync(u => u.TypeOfApplication_ReferenceCode == request.StepOne.ApplicationType);

                var idSuportedPlatform = await _databaseContext.SuportedPlatforms.FirstOrDefaultAsync(u => u.SuportedPlatform_ReferenceCode == request.StepTwo.SupportedPlatforms);

                var idHostingPreference = await _databaseContext.HostingPreferences.FirstOrDefaultAsync(u => u.HostingPreference_ReferenceCode == request.StepFour.HostingPreferences);

                var utcDateTime = request.StepFour.Deadline.Value.ToUniversalTime();

                int orderNumber = 1;
               
                var orderExistList = await _databaseContext.Orders.OrderByDescending(u=>u.OrderNumber).Take(1).ToListAsync();

                if (orderExistList.Count() > 0)
                {
                    orderNumber += orderExistList[0].OrderNumber;
                }

                var order = new Order
                {
                    Order_Id = Guid.NewGuid(),
                    Order_IdUser = idUser,


                    StepOneOrder = new StepOneOrder
                    {
                        TargetAudience = request.StepOne.TargetAudience,
                        CurrentChallengesOrProblems = request.StepOne.Challenges,
                        TypeOfApplication_Id = idTypeOfApplication.TypeOfApplication_Id,
                        MainPurpose = request.StepOne.Purpose
                    },
                    StepTwoOrder = new StepTwoOrder
                    {
                        KeyFeatures = request.StepTwo.KeyFeatures,
                        IntegrationWithExternalSystems = request.StepTwo.IntegrationWithExternalSystems,
                        SuportedPlatform_Id = idSuportedPlatform.SuportedPlatform_Id,
                        SecurityRequirements = request.StepTwo.SecurityRequirements
                    },
                    StepThreeOrder = new StepThreeOrder
                    {
                        AccessibilityNeeds = request.StepThree.AccessibilityNeeds,
                        CustomizationOptions = request.StepThree.CustomizationOptions,
                        DesignPreferences = request.StepThree.DesignPreferences,
                        UsageContext = request.StepThree.UsageContext,
                        EndUserDescription = request.StepThree.EndUserDescription
                    },
                    StepFourOrder = new StepFourOrder
                    {
                        Deadline = utcDateTime,
                        CollaborationWorkflow = request.StepFour.CollaborationWorkflow,
                        HostingPreference_Id = idHostingPreference.HostingPreference_Id,
                        LegalConstraints = request.StepFour.LegalConstraints,
                        PreferredTechnologies = request.StepFour.PreferredTechnologies
                    }
                };

                _databaseContext.Orders.Add(order);

                await _databaseContext.SaveChangesAsync();

                return orderNumber;
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return 0;
            }
        }
    }
}
