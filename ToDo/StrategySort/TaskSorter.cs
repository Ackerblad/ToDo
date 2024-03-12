using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class TaskSorter
    {
        private ISortingStrategy sortingStrategy;

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
