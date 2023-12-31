﻿using Core.Context;
using Core.Model;

namespace Business.Context.Resources;

/// <summary>
///     The user context resource, which is used to store the current user related informations.
/// </summary>
public class UserContextResource : DisposableResource
{
    /// <summary>
    ///     Creates a new instance of the <see cref="UserContextResource" /> class.
    /// </summary>
    /// <param name="user">
    ///     The current user.
    /// </param>
    /// <param name="token">
    ///     The current user's token.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when the <paramref name="user" /> or <paramref name="token" /> is null.
    /// </exception>
    public UserContextResource(User user, string token)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Token = token ?? throw new ArgumentNullException(nameof(token));
    }

    /// <summary>
    ///     The current user.
    /// </summary>
    public User User { get; private set; }

    /// <summary>
    ///     The current user's token.
    /// </summary>
    public string Token { get; private set; }

    /// <summary>
    ///     The port on which the user is connected to the servers.
    ///     Default is 5000.
    /// </summary>
    public int Port { get; private set; } = 5000;

    protected override void Dispose(bool disposing)
    {
        if (!disposing) return;

        User = null;
        Token = null;
    }
}