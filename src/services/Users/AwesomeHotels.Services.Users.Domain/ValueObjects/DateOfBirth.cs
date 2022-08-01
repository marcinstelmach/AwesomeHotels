﻿using AwesomeHotels.Services.Users.Domain.Exceptions;
using BuildingBlocks.Domain;

namespace AwesomeHotels.Services.Users.Domain.ValueObjects;

public class DateOfBirth : ValueObject
{
    private DateOfBirth(DateOnly value)
    {
        Value = value;
        CheckInvariants();
    }

    public DateOnly Value { get; private set; }

    public static DateOfBirth Create(DateOnly dateOnly) => new(dateOnly);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private void CheckInvariants()
    {
        if (Value == default)
        {
            throw new InvalidDateOfBirthValueException("Default value is not correct for DateOfBirth", Value);
        }

        if (Value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidDateOfBirthValueException("Value of birth cannot be in feature", Value);
        }

        var difference = DateTime.UtcNow.Subtract(Value.ToDateTime(TimeOnly.MinValue));
        if (difference > TimeSpan.FromDays(365 * 200))
        {
            throw new InvalidDateOfBirthValueException("People does not live over 200 years", Value);
        }
    }
}