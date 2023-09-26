using Ardalis.GuardClauses;

namespace SharedKernel;

public static class GuardExtensions
{
    public static string MaxLength(this IGuardClause guardClause, string input, string parameterName, uint maxLength, string? message = null)
    {
        if (input.Length > maxLength)
        {
            throw new ArgumentException(message ?? $"Required input {parameterName} cannot be longer than {maxLength} character(s).");
        }

        return input;
    }

    public static string NullOrEmptyOrMaxLength(this IGuardClause guardClause, string input, string parameterName, uint maxLength, string? message = null)
    {
        Guard.Against.NullOrEmpty(input, parameterName, message);
        Guard.Against.MaxLength(input, parameterName, maxLength, message);

        return input;
    }
}