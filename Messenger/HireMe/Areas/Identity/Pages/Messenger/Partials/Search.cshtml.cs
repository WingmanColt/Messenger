using System.Threading.Tasks;
using HireMe.Models;
using HireMe.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HireMe.Web.Areas.Identity.Pages.Messenger.Partials
{
    public class SearchModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageService _messageService;

        public SearchModel(UserManager<User> userManager, IMessageService messageService)
        {
            this._userManager = userManager;
            this._messageService = messageService;
        }

        public string searchString { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Redirect($"/identity/messenger/messageview/{searchString}");
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

            //var content = this._messageService.GetByIdMessage(searchString);
            return Redirect($"/identity/messenger/messageview/{searchString}");
           // return RedirectToPage();
        }
    }
}
