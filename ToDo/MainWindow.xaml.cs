using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

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
            InitializeSelectors();
        }

        private void LoadTasks()
        {
            foreach (var task in taskRepository.GetAll())
            {
                var proxy = new TaskItemProxy(taskRepository, task.Id);
                taskItems.Add(proxy);
            }
        }

        private void InitializeSelectors()
        {
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(TaskEnums.Priority));
            CategorySelector.ItemsSource = Enum.GetValues(typeof(TaskEnums.Category));
        }

        private void InstructionButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. Add a new task by entering the title, priority and category.\n" +
                            "2. Use the 'Details' button to view and edit details of a task.\n" +
                            "3. Use the 'Delete' button to remove a task from the list.\n" +
                            "4. Use the 'Undo' button to revert accidental deletions.\n" +
                            "5. Sort tasks using the buttons next to each column title.",
                            "Instructions", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TaskDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TaskDescription.Text == "Enter Title")
            {
                TaskDescription.Text = "";
                TaskDescription.HorizontalContentAlignment = HorizontalAlignment.Left;
                TaskDescription.FontStyle = FontStyles.Normal;
                TaskDescription.Foreground = Brushes.Black;
            }
        }

        private void TaskDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskDescription.Text))
            {
                TaskDescription.Text = "Enter Title";
                TaskDescription.HorizontalContentAlignment = HorizontalAlignment.Center;
                TaskDescription.FontStyle = FontStyles.Italic;
                TaskDescription.Foreground = Brushes.LightSlateGray;
            }
        }

        //Create a new task and add it to the repository and ObservableCollection
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskDescription.Text) || TaskDescription.Text == "Enter Title")
            {
                MessageBox.Show("Please enter a title for the task.");
                return;
            }

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

            ResetDesign();

            taskItems.Add(new TaskItemProxy(taskRepository, taskItem.Id));      
        }

        //Reset the design in UI
        private void ResetDesign()
        {
            TaskDescription.Text = "Enter Title";
            TaskDescription.HorizontalContentAlignment = HorizontalAlignment.Center;
            TaskDescription.FontStyle = FontStyles.Italic;
            TaskDescription.Foreground = Brushes.LightSlateGray;

            PrioritySelector.SelectedIndex = -1;
            CategorySelector.SelectedIndex = -1;
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

        //Open the TaskDetailsWindow for the selected task, to let user view and add details
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

        //Refresh task list by reloading tasks from the repository
        private void RefreshTaskList()
        {
            taskItems.Clear();
            LoadTasks();
        }

        //Functions for sorting the task list
        //UpButtons sorts in ascending order
        //DownButtons sorts in descending, descending is true
        private void TitleUpButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) => string.Compare(x.Title, y.Title));
        }

        private void TitleDownButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) => string.Compare(x.Title, y.Title), true);
        }

        private void PriorityUpButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) => x.Priority.CompareTo(y.Priority));
        }

        private void PriorityDownButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) => x.Priority.CompareTo(y.Priority), true);
        }

        private void CategoryUpButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) => x.Category.CompareTo(y.Category));
        }

        private void CategoryDownButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) => x.Category.CompareTo(y.Category), true);
        }

        private void CreatedUpButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) => DateTime.Compare(x.CreationDate, y.CreationDate));
        }

        private void CreatedDownButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) => DateTime.Compare(x.CreationDate, y.CreationDate), true);
        }

        private void DueDateUpButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x,y) =>
            {
                if (!x.DueDate.HasValue) return 1;
                if (!y.DueDate.HasValue) return -1;

                return DateTime.Compare(x.DueDate.Value, y.DueDate.Value);
            });
        }

        private void DueDateDownButton_Click(object sender, RoutedEventArgs e)
        {
            SortAndUpdate((x, y) =>
            {
                if (!x.DueDate.HasValue) return -1;
                if (!y.DueDate.HasValue) return 1;

                return DateTime.Compare(x.DueDate.Value, y.DueDate.Value);
            }, true);
        }

        //Sort the task list based on the provided comparison function and update the ListView
        private void SortAndUpdate(Func<ITaskItem, ITaskItem, int> comparison, bool descending = false)
        {
            List<ITaskItem> tasksList = taskItems.ToList();

            if (descending)
            {
                taskSorter.SetSortingStrategy(tasksList, (x, y) => -comparison(x, y));
            }
            else
            {
                taskSorter.SetSortingStrategy(tasksList, comparison);
            }

            UpdateObservableCollection(tasksList);
        }

        private void UpdateObservableCollection(List<ITaskItem> sortedTasks)
        {
            taskItems.Clear();
            foreach (var task in sortedTasks)
            {
                taskItems.Add(task);
            }
        }
    }
}