﻿using System.ComponentModel.DataAnnotations;

namespace Tiya.Database.ViewModels;

public class LoginViewModel
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
