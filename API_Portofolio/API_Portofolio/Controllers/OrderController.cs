using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;
using API_Portofolio.Models.Order.Request;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Portofolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IChatServices _chatServices;
        private readonly UserManager<Account> _userManager;
        public OrderController(IOrderServices orderServices, UserManager<Account> userManager, IChatServices chatServices)
        {
            _orderServices = orderServices;
            _userManager = userManager;
            _chatServices = chatServices;
        }

        [HttpGet("get-typesOfApplication")]
        public async Task<IActionResult> GetTypesOfApplication()
        {
            var response = await _orderServices.GetTypesOfApplicationAsync();

            if (response.IsError)
            {
                var firstError = response.Errors.First();

                return firstError.Type switch
                {
                    ErrorType.NotFound => NotFound(new { message = firstError.Description }),
                    ErrorType.Failure => StatusCode(500, new { message = firstError.Description }),
                    _ => StatusCode(500, new { message = "A apărut o eroare necunoscută" })
                };
            }

            return Ok(response.Value);
        }

        [HttpGet("get-suportedPlatforms")]
        public async Task<IActionResult> GetSuportedPlatforms()
        {
            var response = await _orderServices.GetSuportedPlatformsAsync();

            if (response.IsError)
            {
                var firstError = response.Errors.First();

                return firstError.Type switch
                {
                    ErrorType.NotFound => NotFound(new { message = firstError.Description }),
                    ErrorType.Failure => StatusCode(500, new { message = firstError.Description }),
                    _ => StatusCode(500, new { message = "A apărut o eroare necunoscută" })
                };
            }

            return Ok(response.Value);
        }

        [HttpGet("get-hostingPreferences")]
        public async Task<IActionResult> GetHostingPreferences()
        {
            var response = await _orderServices.GetHostingPreferencesAsync();

            if (response.IsError)
            {
                var firstError = response.Errors.First();

                return firstError.Type switch
                {
                    ErrorType.NotFound => NotFound(new { message = firstError.Description }),
                    ErrorType.Failure => StatusCode(500, new { message = firstError.Description }),
                    _ => StatusCode(500, new { message = "A apărut o eroare necunoscută" })
                };
            }

            return Ok(response.Value);
        }

        [Authorize]
        [HttpPost("save-order")]
        public async Task<IActionResult> SendOrder([FromBody] SendOrder_DTO request)
        {
            if (!ModelState.IsValid)
            {
                //_logger.LogWarning("Numele de utilizator si parola nu sunt completate");
                return BadRequest(ModelState);
            }

            // Accesarea revendicărilor utilizatorului din HttpContext
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(username == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var user = await _userManager.FindByNameAsync(username ?? string.Empty);

            if (user==null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _orderServices.SendOrderAsync(request,user.Id);

            if (result.IsError)
            {
                var firstError = result.Errors.First();

                return firstError.Type switch
                {
                    ErrorType.NotFound => NotFound(new { message = firstError.Description }),
                    ErrorType.Failure => StatusCode(500, new { message = firstError.Description }),
                    _ => StatusCode(500, new { message = "A apărut o eroare necunoscută" })
                };
            }

            var chatResult = _chatServices.CreateOrderChatAsync(user);

            return Ok("Comanda a fost trimisa cu succes");
        }


        [HttpPost("contact-send")]
        public async Task<IActionResult> SendMailContact([FromBody] SendMailContact_DTO request)
        {
            if (!ModelState.IsValid)
            {
                //_logger.LogWarning("Numele de utilizator si parola nu sunt completate");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}



