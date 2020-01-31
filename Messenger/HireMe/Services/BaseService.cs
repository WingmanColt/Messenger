using HireMe.Data.Repository.Interfaces;
using HireMe.Services.Interfaces;
using HireMe.Models;
using System;
using System.Linq;
using HireMe.ViewModels.Accounts;
using System.Collections.Generic;
using HireMe.Mapping;
using HireMe.Models.Enums;
using Microsoft.CodeAnalysis;

namespace HireMe.Services
{
    public class BaseService : IBaseService
    {
        private readonly IRepository<User> _userRepository;

        public BaseService(
              IRepository<User> userRepository)
        {
             this._userRepository = userRepository;
        }
         public IEnumerable<UserViewModel> GetUserBy_FirstName(string name)
        {
            var users = _userRepository.All()
                .Where(p => p.FirstName.Contains(name))
                .Select(p => p.FirstName)
                .To<UserViewModel>()
                .ToList();

            return users;
        }

    }
}
