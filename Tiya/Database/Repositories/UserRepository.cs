using Microsoft.AspNetCore.Identity;
using Tiya.Database.Interfaces;
using Tiya.Database.ViewModels;
using Tiya.Database.DomainModels.Account;
using Tiya.Helpers.Enums;

namespace Tiya.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<TiyaUser> _userManager;
    private readonly SignInManager<TiyaUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(UserManager<TiyaUser> userManager, SignInManager<TiyaUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
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

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            var usersCount = _userManager.Users.Count();
            if (usersCount == 1)
                await _userManager.AddToRoleAsync(user, Role.Admin.ToString());

            await _userManager.AddToRoleAsync(user, Role.User.ToString());
            await _signInManager.SignInAsync(user, true);
        }
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

    public async Task CreateRole()
    {
        foreach (var item in Enum.GetValues(typeof(Role)))
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = item.ToString()
            });
        }
    }
}
