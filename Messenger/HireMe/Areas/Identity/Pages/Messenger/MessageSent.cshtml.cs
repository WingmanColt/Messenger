using HireMe.Data.Repository.Interfaces;
using HireMe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HireMe.Areas.Identity.Pages.Messenger
{
    public partial class MessageSentModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Message> messageRepository;

        public MessageSentModel(UserManager<User> userManager, IRepository<Message> messageRepository)
        {
            this._userManager = userManager;
            this.messageRepository = messageRepository;
        }

        [BindProperty]
        public Message Message { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool EnablePrevious => CurrentPage > 1;
        public bool EnableNext => CurrentPage < TotalPages;
        public IEnumerable<Message> MessageList { get; set; }

        public async Task<IActionResult> OnGetAsync(int currentPage)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var posterId = _userManager.GetUserId(User);

            Count = this.messageRepository.All()
              .Where(j => j.SenderId == posterId)
              .Count();

            if (Count > 0) // prevent 'SqlException: The offset specified in a OFFSET clause may not be negative.'
            {
                CurrentPage = currentPage == 0 ? 1 : currentPage;

                if (CurrentPage > TotalPages)
                    CurrentPage = TotalPages;

                MessageList = this.messageRepository.All()
                .Where(j => j.SenderId == posterId)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            }
            else return Redirect($"/Identity/Messenger/Errors/NoMessages");

            return Page();

        }
    }
}
