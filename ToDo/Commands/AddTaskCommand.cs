namespace ToDo
{
    public class AddTaskCommand : ICommand
    {
        private readonly ITaskRepository _repository;
        private readonly TaskItem _taskItem;
        private int _addedTaskId;

        public AddTaskCommand(ITaskRepository repository, TaskItem taskItem)
        {
            _repository = repository;
            _taskItem = taskItem;
        }

        public void Execute()
        {
            _repository.Add(_taskItem);
            _addedTaskId = _taskItem.Id;
        }

        public void Undo()
        {
            if (_addedTaskId != 0)
            {
                _repository.Delete(_addedTaskId);
            }
        }
    }
}
