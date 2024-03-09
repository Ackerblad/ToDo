namespace ToDo
{
    public class TaskItem : ITaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Priority { get;set; }
        public string Category { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
    }
}
