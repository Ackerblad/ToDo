using System.Text.Json;
using System.IO;

namespace ToDo
{
    public class TaskRepository : ITaskRepository
    {
        private string filePath = "tasks.json";
        private List<TaskItem> tasks;

        public TaskRepository()
        {
            tasks = LoadTasks() ?? new List<TaskItem>();
        }

        private List<TaskItem> LoadTasks()
        {
            if (!File.Exists(filePath))
            {
                return new List<TaskItem>();
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json);
        }

        public void Add(TaskItem task)
        {
            int nextId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
            task.Id = nextId;
            tasks.Add(task);
            SaveTasks();
        }

        public void Update(TaskItem task)
        {
            var index = tasks.FindIndex(t => t.Id == task.Id);
            if (index != -1)
            {
                tasks[index] = task;
                SaveTasks();
            }
        }

        public void Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                SaveTasks();
            }
        }

        public TaskItem Get(int id)
        {
            return tasks.FirstOrDefault(task => task.Id == id);
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return tasks;
        }

        private void SaveTasks()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(filePath, json);
        }
    }
}
