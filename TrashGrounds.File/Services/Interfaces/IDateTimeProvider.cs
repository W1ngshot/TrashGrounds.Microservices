﻿namespace TrashGrounds.Template.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}