using P360.Repository.Repositories;

namespace P360.Project.Api.Validation;

internal sealed class ValidationErrorBuilder
{
    private readonly Dictionary<string, List<string>> _errors = [];

    public int Count => _errors.Count;

    public void Required(string fieldName, string? value, string displayName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Add(fieldName, $"{displayName} is required.");
        }
    }

    public void ObjectId(string fieldName, string? value, string displayName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Add(fieldName, $"{displayName} is required.");
            return;
        }

        if (!MongoObjectId.IsValid(value))
        {
            Add(fieldName, $"{displayName} must be a valid MongoDB ObjectId.");
        }
    }

    public void NonNegative(string fieldName, int? value, string displayName)
    {
        if (value < 0)
        {
            Add(fieldName, $"{displayName} cannot be negative.");
        }
    }

    public void NonNegative(string fieldName, int value, string displayName)
    {
        if (value < 0)
        {
            Add(fieldName, $"{displayName} cannot be negative.");
        }
    }

    public void Error(string fieldName, string message)
    {
        Add(fieldName, message);
    }

    public IDictionary<string, string[]> ToDictionary()
    {
        return _errors.ToDictionary(error => error.Key, error => error.Value.ToArray());
    }

    private void Add(string fieldName, string message)
    {
        if (!_errors.TryGetValue(fieldName, out var messages))
        {
            messages = [];
            _errors[fieldName] = messages;
        }

        messages.Add(message);
    }
}
