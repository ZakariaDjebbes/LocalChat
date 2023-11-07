using Core.Model;

namespace Core.Context;

public record UserContextResource(User User, string Token);