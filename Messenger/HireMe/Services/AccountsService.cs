namespace HireMe.Services
{
    using Data;
    using HireMe.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountsService : IAccountsService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly BaseDbContext context;

        public AccountsService(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            BaseDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<bool> Register(string username, string password, string confirmPassword, string email, string firstName, string lastName, DateTime age, bool isemployer)
        {
            if (password != confirmPassword)
            {
                return false;
            }

            var user = new User
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                isEmployer = isemployer
            };

            var result = userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                if (userManager.Users.Count() == 1)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "User");
                }

                signInManager.SignInAsync(user, false).Wait();
            }

            return result.Succeeded;
        }

        public bool Login(string username, string password, bool rememberMe)
        {
            var result = signInManager.PasswordSignInAsync(username, password, rememberMe, true).Result;

            return result.Succeeded;
        }

        public void Logout()
        {
            signInManager.SignOutAsync().Wait();
        }

        public ICollection<User> GetAllUsers()
        {
            var users = context.Users.ToList();

            return users;
        }

        public string LatestUsernames(string orederBy = "username")
        {
            IQueryable<User> query = context.Users;

            if (orederBy == "username")
            {
                query = query.OrderByDescending(x => x.UserName);
            }
            else if (orederBy == "email")
            {
                query = query.OrderByDescending(x => x.Email);
            }

            return query.Select(x => x.UserName).FirstOrDefault();
        }
        public void PromoteUser(string userId)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == userId);

            var role = userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var userRole = context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            context.UserRoles.Remove(userRole);

            userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }

        public void DemoteUser(string userId)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == userId);

            var role = userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var userRole = context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            context.UserRoles.Remove(userRole);

            userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
        }
    }
}