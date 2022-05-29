using MVCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWeb.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
