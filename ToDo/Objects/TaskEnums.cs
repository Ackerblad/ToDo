using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public static class TaskEnums
    {
        public enum Priority
        {
            Low = 1,
            Medium = 2,
            High = 3
        }

        public enum Category
        {
            Personal,
            Work,
            School,
            Other
        }
    }
}
