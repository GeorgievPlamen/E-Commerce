namespace Users.Core.DTO;

public record LoginRequest(
    string? Email,
    string? Password);