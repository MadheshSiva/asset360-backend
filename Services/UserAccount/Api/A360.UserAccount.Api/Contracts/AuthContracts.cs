namespace A360.UserAccount.Api.Contracts;

public sealed record LoginRequest(string? UserName, string? Password);

public sealed record LoginResponse(
    bool TwoFactorRequired,
    string Message,
    string MaskedEmail,
    int OtpExpiresInSeconds);

public sealed record VerifyOtpRequest(string? UserName, string? Otp);

public sealed record RefreshTokenRequest(string? RefreshToken);

public sealed record AuthUserResponse(
    string Id,
    string UserId,
    string UserName,
    string Email,
    string RoleName,
    string UserRoleId,
    string? ClientId,
    string? TenantId);

public sealed record AuthTokenResponse(
    string Message,
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt,
    string TokenType,
    AuthUserResponse User);
