namespace ToDo
{
    public interface ITaskItem
    {
        int Id { get;}
        string Title { get;}
        string Priority { get;}
        string Category { get;}
        DateTime CreationDate { get;}
        DateTime? DueDate { get;}
    }
}
