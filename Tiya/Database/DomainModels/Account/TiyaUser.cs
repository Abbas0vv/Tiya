using Microsoft.AspNetCore.Identity;

namespace Tiya.Database.DomainModels.Account;

public class TiyaUser : IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
