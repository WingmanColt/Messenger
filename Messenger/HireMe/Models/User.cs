namespace HireMe.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureName { get; set; }
        public DateTime Age { get; set; }
        public bool isEmployer { get; set; }


    }


}
