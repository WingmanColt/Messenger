using HireMe.Mapping;
using HireMe.Models;
using System;

namespace HireMe.ViewModels.Accounts
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureName { get; set; }
        public DateTime Age { get; set; }
        public bool isEmployer { get; set; }

        // Employers
        public string CompanyName { get; set; }
        public string CompanyCheckNumber { get; set; }
        public bool isPrivate { get; set; }


    }
}