using SampleApp.Modules.Todo.Domain.Events;
using SampleApp.Modules.Todo.Domain.ValueObjects;
using SampleApp.Shared.Domain.SeedWork;

namespace SampleApp.Modules.Todo.Domain.Entities;

public class TodoItem : AggregateRoot
{
    public TodoTitle Title { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }

    private TodoItem() { }

    public static TodoItem Create(TodoTitle title, string? description = null)
    {
        var todo = new TodoItem
        {
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTimeOffset.UtcNow
        };
        todo.AddDomainEvent(new TodoItemCreatedEvent(todo.Id, title.Value));
        return todo;
    }

    public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("TodoItem is already completed.");

        IsCompleted = true;
        CompletedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new TodoItemCompletedEvent(Id));
    }

    public void UpdateTitle(TodoTitle title)
    {
        Title = title;
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
    }
}
