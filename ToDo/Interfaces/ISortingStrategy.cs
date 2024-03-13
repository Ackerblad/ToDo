namespace ToDo
{
    public interface ISortingStrategy
    {
        void Sort(List<ITaskItem> tasks, Func<ITaskItem, ITaskItem, int> comparison);
    }
}
