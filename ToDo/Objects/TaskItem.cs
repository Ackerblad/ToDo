namespace ToDo
{
    public class TaskItem : ITaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public TaskEnums.Priority Priority { get;set; }
        public TaskEnums.Category Category { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
    }
}
