using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public interface ISortingStrategy
    {
        void Sort(List<ITaskItem> tasks, Func<ITaskItem, ITaskItem, int> comparison);
    }
}
