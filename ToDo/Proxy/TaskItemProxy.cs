namespace ToDo
{
    public class TaskItemProxy : ITaskItem
    {
        private readonly ITaskRepository taskRepository;
        private readonly int taskId;
        private TaskItem _fullTaskItem;

        private TaskItem fullTaskItem
        {
            get
            {
                if (_fullTaskItem == null)
                {
                    _fullTaskItem = taskRepository.Get(taskId) as TaskItem;
                }
                return _fullTaskItem;
            }
        }

        public TaskItemProxy(ITaskRepository taskRepository, int taskId)
        {
            this.taskRepository = taskRepository;
            this.taskId = taskId;
        }

        public int Id => fullTaskItem.Id;
        public string Title => fullTaskItem.Title;
        public string Priority => fullTaskItem.Priority;
        public string Category => fullTaskItem.Category;
        public DateTime CreationDate => fullTaskItem.CreationDate;
        public DateTime? DueDate => fullTaskItem.DueDate;
    }
}
