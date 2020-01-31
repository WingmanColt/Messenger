using HireMe.Models;
using HireMe.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace HireMe.Areas.Identity.Pages.Messenger
{
    public partial class SendMessageModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageService;
        
        public SendMessageModel(
            UserManager<User> userManager,
            IMessageService messageService)
        {
            this._userManager = userManager;
            this._messageService = messageService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var senderId = _userManager.GetUserId(User);
            await this._messageService.Create(Message.Title, Message.Description, senderId, Message.ReceiverId);

            StatusMessage = "Your message have been sent";
            return RedirectToPage();
        }

    }

}
