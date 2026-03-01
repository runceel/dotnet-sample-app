namespace SampleApp.Modules.Todo.Domain.ValueObjects;

public sealed class TodoTitle
{
    public const int MaxLength = 200;

    public string Value { get; }

    private TodoTitle(string value)
    {
        Value = value;
    }

    public static TodoTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Title cannot be empty.", nameof(value));

        if (value.Length > MaxLength)
            throw new ArgumentException($"Title cannot exceed {MaxLength} characters.", nameof(value));

        return new TodoTitle(value.Trim());
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj) =>
        obj is TodoTitle other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
