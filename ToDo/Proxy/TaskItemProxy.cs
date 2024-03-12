namespace ToDo
{
    public class TaskItemProxy : ITaskItem
    {
        private readonly ITaskRepository taskRepository;
        private readonly int taskId;
        private TaskItem fullTaskItem;

        private TaskItem FullTaskItem
        {
            get
            {
                if (fullTaskItem == null)
                {
                    fullTaskItem = taskRepository.Get(taskId);
                }
                return fullTaskItem;
            }
        }

        public TaskItemProxy(ITaskRepository taskRepository, int taskId)
        {
            this.taskRepository = taskRepository;
            this.taskId = taskId;
        }

        public int Id => FullTaskItem.Id;
        public string Title => FullTaskItem.Title;
        public TaskEnums.Priority Priority => FullTaskItem.Priority;
        public TaskEnums.Category Category => FullTaskItem.Category;
        public DateTime CreationDate => FullTaskItem.CreationDate;
        public DateTime? DueDate => FullTaskItem.DueDate;
    }
}
