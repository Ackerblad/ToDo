namespace ToDo
{
    public interface ITaskRepository
    {
        void Add(TaskItem task);
        void Update(TaskItem task);
        void Delete(int id);
        TaskItem Get(int id);
        IEnumerable<TaskItem> GetAll();
    }
}
