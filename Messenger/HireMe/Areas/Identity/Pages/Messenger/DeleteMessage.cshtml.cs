using HireMe.Models;
using HireMe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HireMe.Areas.Identity.Pages.Messenger
{
    public partial class DeleteMessageModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageService;

        public DeleteMessageModel(
            IMessageService messageService,
            UserManager<User> userManager)
        {
            this._messageService = messageService;
            this._userManager = userManager;
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
                return Redirect($"/Identity/Errors/NotFound");
             }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            this._messageService.Delete(id, user.Id);
            return Redirect($"/Identity/Messenger/");
        }

    }
}
