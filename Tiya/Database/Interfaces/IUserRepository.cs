using Microsoft.AspNetCore.Identity;
using Tiya.Database.DomainModels.Account;
using Tiya.Database.ViewModels;

namespace Tiya.Database.Interfaces;

public interface IUserRepository
{
    Task Register(RegisterViewModel model);
    Task Login(LoginViewModel model);
    Task LogOut();
    Task CreateRole();
}


