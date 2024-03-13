using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToDo
{
    public class TaskRepository : ITaskRepository
    {
        private string filePath = "tasks.json";
        private List<TaskItem> tasks;

        public TaskRepository()
        {
            tasks = LoadTasks();
        }

        private List<TaskItem> LoadTasks()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<TaskItem>();
                }

                string json = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                };
                return JsonSerializer.Deserialize<List<TaskItem>>(json, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}");
                return new List<TaskItem>();
            }
        }

        //Adds a new task with a unique ID 
        public void Add(TaskItem task)
        {
            try
            {
                if (task != null)
                {
                    int nextId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
                    task.Id = nextId;
                    tasks.Add(task);
                    SaveTasks();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding task: {ex.Message}");
            }
        }

        //Updates an existing task
        public void Update(TaskItem task)
        {
            try
            {
                var index = tasks.FindIndex(t => t.Id == task.Id);
                if (index != -1)
                {
                    tasks[index] = task;
                    SaveTasks();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating task: {ex.Message}");
            }
        }

        //Deletes a task by its ID
        public void Delete(int id)
        {
            try
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    tasks.Remove(task);
                    SaveTasks();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task: {ex.Message}");
            }
        }

        // Retrieves a single task by its ID
        public TaskItem Get(int id)
        {
            try
            {
                return tasks.FirstOrDefault(task => task.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving task: {ex.Message}");
                return null;
            }
        }

        // Retrieves all tasks
        public IEnumerable<TaskItem> GetAll()
        {
            try
            {
                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all tasks: {ex.Message}");
                return new List<TaskItem>();
            }
        }

        private void SaveTasks()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }

                };
                string json = JsonSerializer.Serialize(tasks, options);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }
    }
}
