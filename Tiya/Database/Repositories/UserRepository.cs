using Microsoft.AspNetCore.Identity;
using Tiya.Database.DomainModels.Account;
using Tiya.Database.Interfaces;
using Tiya.Database.ViewModels;

namespace Tiya.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<TiyaUser> _userManager;
    private readonly SignInManager<TiyaUser> _signInManager;

    public UserRepository(UserManager<TiyaUser> userManager, SignInManager<TiyaUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task Register(RegisterViewModel model)
    {
        var user = new TiyaUser()
        {
            Name = model.Name,
            Surname = model.Surname,
            UserName = model.UserName,
            Email = model.Email,
        };

        await _userManager.CreateAsync(user, model.Password);
        await _signInManager.SignInAsync(user,true);
    }

    public async Task Login(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null) return;
        var result = await _userManager.CheckPasswordAsync(user, model.Password);

        if (result) await _signInManager.SignInAsync(user, true);
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }
}
