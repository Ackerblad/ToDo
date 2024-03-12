using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskRepository taskRepository = new TaskRepository();
        private CommandManager commandManager = new CommandManager();
        private TaskSorter taskSorter = new TaskSorter();
        private ObservableCollection<ITaskItem> taskItems = new ObservableCollection<ITaskItem>();
        
        public MainWindow()
        {
            InitializeComponent();
            TaskList.ItemsSource = taskItems;
            LoadTasks();
            InitializePrioritySelector();
            InitializeCategorySelector();

        }

        private void LoadTasks()
        {
            foreach (var task in taskRepository.GetAll())
            {
                var proxy = new TaskItemProxy(taskRepository, task.Id);
                taskItems.Add(proxy);
            }
        }

        private void InitializePrioritySelector()
        {
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(TaskEnums.Priority));
        }
        private void InitializeCategorySelector()
        {
            CategorySelector.ItemsSource = Enum.GetValues(typeof(TaskEnums.Category));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var priority = PrioritySelector.SelectedItem != null ? (TaskEnums.Priority)PrioritySelector.SelectedItem : TaskEnums.Priority.Low;
            var category = CategorySelector.SelectedItem != null ? (TaskEnums.Category)CategorySelector.SelectedItem : TaskEnums.Category.Other;
            var taskItem = new TaskItemBuilder()
                                .SetTitle(TaskDescription.Text)
                                .SetPriority(priority)
                                .SetCategory(category)
                                .SetCreationDate(DateTime.Now)
                                .Build();

            var addCommand = new AddTaskCommand(taskRepository, taskItem);
            commandManager.ExecuteCommand(addCommand);

            TaskDescription.Clear();

            taskItems.Add(new TaskItemProxy(taskRepository, taskItem.Id));
          
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            commandManager.Undo();
            RefreshTaskList();
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            commandManager.Redo();
            RefreshTaskList();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItemProxy selectedTaskProxy)
            {
                var detailedTask = taskRepository.Get(selectedTaskProxy.Id);

                if (detailedTask != null)
                {
                    var taskDetailsWindow = new TaskDetailsWindow(detailedTask);
                    taskDetailsWindow.ShowDialog();

                    taskRepository.Update(detailedTask);
                    RefreshTaskList();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           if (TaskList.SelectedItem is TaskItemProxy selectedTaskProxy)
            {
                var deleteCommand = new DeleteTaskCommand(taskRepository, selectedTaskProxy.Id);
                commandManager.ExecuteCommand(deleteCommand);
                RefreshTaskList();
            }
        }

        private void RefreshTaskList()
        {
            taskItems.Clear();
            LoadTasks();
        }
    }
}