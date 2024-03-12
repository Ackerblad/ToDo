namespace ToDo
{
    public class TaskItemBuilder : ITaskItemBuilder
    {
        private TaskItem taskItem = new TaskItem();

        public ITaskItemBuilder SetTitle(string title)
        {
            taskItem.Title = title;
            return this;
        }

        public ITaskItemBuilder SetCreator(string creator)
        {
            taskItem.Creator = creator;
            return this;
        }

        public ITaskItemBuilder SetPriority(TaskEnums.Priority priority)
        {
            taskItem.Priority = priority;
            return this;
        }

        public ITaskItemBuilder SetCategory(TaskEnums.Category category)
        {
            taskItem.Category = category;
            return this;
        }

        public ITaskItemBuilder SetCreationDate(DateTime creationDate)
        {
            taskItem.CreationDate = creationDate;
            return this;
        }

        public ITaskItemBuilder SetDueDate(DateTime dueDate)
        {
            taskItem.DueDate = dueDate;
            return this;
        }

        public ITaskItemBuilder SetDescription(string description)
        {
            taskItem.Description = description;
            return this;
        }

        public TaskItem Build()
        {
            return taskItem;
        }
    }
}
