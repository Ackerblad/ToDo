namespace ToDo
{
    public class DeleteTaskCommand : ICommand
    {
        private readonly ITaskRepository _repository;
        private TaskItem _deletedTask;
        private int _deletedTaskId;

        public DeleteTaskCommand(ITaskRepository repository, int taskId)
        {
            _repository = repository;
            _deletedTaskId = taskId;
        }

        public void Execute()
        {
            _deletedTask = _repository.Get(_deletedTaskId);
            if (_deletedTask != null)
            {
                _repository.Delete(_deletedTaskId);
            }
        }

        public void Undo()
        {
            if (_deletedTaskId != null)
            {
                _repository.Add(_deletedTask);
            }
        }
    }
}
