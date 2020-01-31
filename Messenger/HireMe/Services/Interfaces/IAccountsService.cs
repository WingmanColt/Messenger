namespace HireMe.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HireMe.Models;

    public interface IAccountsService
    {
        Task<bool> Register(string username, string password, string confirmPassword, string email, string firstName, string lastName, DateTime age, bool isemployer);

        bool Login(string username, string password, bool rememberMe);

        void Logout();

        ICollection<User> GetAllUsers();

        string LatestUsernames(string orederBy);

        void PromoteUser(string userId);

        void DemoteUser(string userId);
    }
}