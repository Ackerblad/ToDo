namespace ToDo
{
    public interface ITaskItem
    {
        int Id { get;}
        string Title { get;}
        TaskEnums.Priority Priority { get;}
        TaskEnums.Category Category { get;}
        DateTime CreationDate { get;}
        DateTime? DueDate { get;}
    }
}
