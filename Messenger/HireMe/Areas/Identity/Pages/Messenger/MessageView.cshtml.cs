using HireMe.Models;
using HireMe.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HireMe.Areas.Identity.Pages.Messenger
{
    public partial class MessageShowModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageService;

        public MessageShowModel(UserManager<User> userManager, IMessageService messageService)
        {
            this._userManager = userManager;
            this._messageService = messageService;
        }

        [BindProperty]
        public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var msg = this._messageService.GetByIdMessage(id);

            if (msg == null)
            {
                return Redirect($"/Identity/Messenger/Errors/NotFound");
            }

            // use instead viewmodel cuz razor page cannot map
            var content = this._messageService.GetByIdMessage(id);
            ViewData["Message"] = content;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
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

            // use instead viewmodel cuz razor page cannot map
            var content = this._messageService.GetByIdMessage(id);
            ViewData["Message"] = content;

            if (content.isRead == false)
                content.isRead = true;

            return RedirectToPage();
        }

    }
}
