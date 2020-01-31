using HireMe.ViewModels.Accounts;
using System.Collections.Generic;

namespace HireMe.Services.Interfaces
{

    public interface IBaseService
    { 

        IEnumerable<UserViewModel> GetUserBy_FirstName(string name);
    }
}