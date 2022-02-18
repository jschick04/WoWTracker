﻿using System.ComponentModel.DataAnnotations;
using Tracker.Api.Contracts.Helpers;

namespace Tracker.Api.Contracts.Identity.Requests;

public class AuthenticationRequest {

    [Required]
    [EmailAddress]
    public string Username { get; set; } = null!;

    [Required]
    [Password]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

}