namespace ToDo
{
    public class TaskSorter
    {
        private ISortingStrategy sortingStrategy;

        //Determines sorting strategy based on the number of tasks
        public void SetSortingStrategy(List<ITaskItem> tasks, Func<ITaskItem, ITaskItem, int> comparison)
        {
            if (tasks.Count <= 20)
            {
                sortingStrategy = new InsertionSortStrategy();
            }
            else
            {
                sortingStrategy = new QuickSortStrategy();
            }

            sortingStrategy.Sort(tasks, comparison);
        }
    }
}
