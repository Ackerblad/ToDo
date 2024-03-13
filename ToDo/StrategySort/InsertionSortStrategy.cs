namespace ToDo
{
    public class InsertionSortStrategy : ISortingStrategy
    {
        //Sorts tasks using InsertionSort based on the given comparison criteria
        public void Sort(List<ITaskItem> tasks, Func<ITaskItem, ITaskItem, int> comparison)
        {
            for (int i = 1; i < tasks.Count; i++)
            {
                ITaskItem key = tasks[i];
                int j = i - 1;

                while (j >= 0 && comparison(tasks[j], key) > 0)
                {
                    tasks[j + 1] = tasks[j];
                    j = j - 1;
                }
                tasks[j + 1] = key;
            }
        }
    }
}
