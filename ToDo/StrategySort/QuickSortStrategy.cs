using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class QuickSortStrategy : ISortingStrategy
    {
        public void Sort(List<ITaskItem> tasks, Func<ITaskItem, ITaskItem, int> comparison)
        {
            QuickSort(tasks, 0, tasks.Count - 1, comparison);
        }

        private void QuickSort(List<ITaskItem> tasks, int low, int high, Func<ITaskItem, ITaskItem, int> comparison)
        {
            if (low < high)
            {
                int piviotIndex = Partition(tasks, low, high, comparison);
                QuickSort(tasks, low, piviotIndex - 1, comparison);
                QuickSort(tasks, piviotIndex + 1, high, comparison);
            }
        }

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

        private void Swap(List<ITaskItem> tasks, int i, int j)
        {
            ITaskItem temp = tasks[i];
            tasks[i] = tasks[j];
            tasks[j] = temp;
        }
    }
}
