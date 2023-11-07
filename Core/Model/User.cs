﻿namespace Core.Model;

public class User : IEntity
{
    public Guid Id { get; init; }
    /// <summary>
    /// The username of the user.
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// The hashed password of the user.
    /// </summary>
    public string PasswordHash { get; set; }
    /// <summary>
    /// The salt used to hash the password.
    /// </summary>

    public string PasswordSalt { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    /// <summary>
    /// The roles of the user.
    /// </summary>
    public ICollection<Role> Roles { get; init; } = new List<Role>();
    /// <summary>
    /// The servers the user is a member of.
    /// </summary>
    public ICollection<Server> Servers { get; init; } = new List<Server>();
}