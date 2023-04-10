﻿namespace TrashGrounds.User.Models.Main;

public record RefreshToken(string Token, DateTime CreatedAt, DateTime Expires, string CreatedByIp, string UserAgent)
{
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => !IsExpired;
}