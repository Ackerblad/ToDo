namespace ToDo
{
    public class QuickSortStrategy : ISortingStrategy
    {
        //Sorts tasks using QuickSort based on the given comparison criteria
        public void Sort(List<ITaskItem> tasks, Func<ITaskItem, ITaskItem, int> comparison)
        {
            QuickSort(tasks, 0, tasks.Count - 1, comparison);
        }

        //Repeatedly sorts a portion of the list
        private void QuickSort(List<ITaskItem> tasks, int low, int high, Func<ITaskItem, ITaskItem, int> comparison)
        {
            if (low < high)
            {
                int piviotIndex = Partition(tasks, low, high, comparison);
                QuickSort(tasks, low, piviotIndex - 1, comparison);
                QuickSort(tasks, piviotIndex + 1, high, comparison);
            }
        }

        //Partitions the tasklist around a pivot
        private int Partition(List<ITaskItem> tasks, int low, int high, Func<ITaskItem, ITaskItem, int> comparison)
        {
            ITaskItem pivot = tasks[high];
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (comparison(tasks[j], pivot) < 0)
                {
                    i++;
                    Swap(tasks, i, j);
                }
            }

            Swap(tasks, i + 1, high);

            return i + 1;
        }

        //Swaps two elements in the tasklist
        private void Swap(List<ITaskItem> tasks, int i, int j)
        {
            ITaskItem temp = tasks[i];
            tasks[i] = tasks[j];
            tasks[j] = temp;
        }
    }
}
