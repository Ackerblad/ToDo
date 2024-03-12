namespace ToDo
{
    public class DeleteTaskCommand : ICommand
    {
        private readonly ITaskRepository repository;
        private TaskItem deletedTask;
        private int deletedTaskId;

        public DeleteTaskCommand(ITaskRepository repository, int taskId)
        {
            this.repository = repository;
            this.deletedTaskId = taskId;
        }

        public void Execute()
        {
            this.deletedTask = this.repository.Get(this.deletedTaskId);
            if (this.deletedTask != null)
            {
                this.repository.Delete(this.deletedTaskId);
            }
        }

        public void Undo()
        {
                this.repository.Add(this.deletedTask);            
        }
    }
}
