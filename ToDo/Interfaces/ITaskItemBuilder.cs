namespace ToDo
{
    public interface ITaskItemBuilder
    {
        ITaskItemBuilder SetTitle(string title);
        ITaskItemBuilder SetCreator(string creator);
        ITaskItemBuilder SetPriority(TaskEnums.Priority priority);
        ITaskItemBuilder SetCategory(TaskEnums.Category category);
        ITaskItemBuilder SetCreationDate(DateTime creationDate);
        ITaskItemBuilder SetDueDate(DateTime dueDate);
        ITaskItemBuilder SetDescription(string description);

        TaskItem Build();
    }
}
