using API_Portofolio.Database.Entities;
using API_Portofolio.Interface;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Portofolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatServices _chatServices;
        private readonly UserManager<Account> _userManager;

        public ChatController(IChatServices chatServices, UserManager<Account> userManager)
        {
            _chatServices = chatServices;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("get-chats")]
        public async Task<IActionResult> GetChats()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var user = await _userManager.FindByNameAsync(username ?? string.Empty);

            if (user == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var result = await _chatServices.GetChatsAsync(user);

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

            return Ok(result.Value);
        }
    }
}
