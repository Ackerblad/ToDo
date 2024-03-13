namespace ToDo
{
    public class TaskItemProxy : ITaskItem
    {
        private readonly ITaskRepository taskRepository;
        private readonly int taskId;
        private TaskItem cachedTaskItem;

        public TaskItemProxy(ITaskRepository taskRepository, int taskId)
        {
            this.taskRepository = taskRepository;
            this.taskId = taskId;
        }

        // Lazily loads and caches the TaskItem details from the repository when needed
        private TaskItem FullTaskItem
        {
            get
            {
                if (cachedTaskItem == null)
                {
                    cachedTaskItem = taskRepository.Get(taskId);
                }
                return cachedTaskItem;
            }
        }

        //Properties that lazily load the TaskItem
        public int Id => FullTaskItem.Id;
        public string Title => FullTaskItem.Title;
        public TaskEnums.Priority Priority => FullTaskItem.Priority;
        public TaskEnums.Category Category => FullTaskItem.Category;
        public DateTime CreationDate => FullTaskItem.CreationDate;
        public DateTime? DueDate => FullTaskItem.DueDate;
    }
}
