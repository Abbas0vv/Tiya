﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tiya.Database.DomainModels;
using Tiya.Database.DomainModels.Account;

namespace Tiya.Database;

public class TiyaDbContext : IdentityDbContext<TiyaUser>
{
    public TiyaDbContext(DbContextOptions<TiyaDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
}
