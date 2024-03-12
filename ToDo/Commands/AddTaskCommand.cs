namespace ToDo
{
    public class AddTaskCommand : ICommand
    {
        private readonly ITaskRepository repository;
        private readonly TaskItem taskItem;
        private int addedTaskId;

        public AddTaskCommand(ITaskRepository repository, TaskItem taskItem)
        {
            this.repository = repository;
            this.taskItem = taskItem;
        }

        public void Execute()
        {
            this.repository.Add(this.taskItem);
            this.addedTaskId = this.taskItem.Id;
        }

        public void Undo()
        {
            if (this.addedTaskId != 0)
            {
                this.repository.Delete(this.addedTaskId);
            }
        }
    }
}
