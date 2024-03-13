using System.Windows;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for TaskDetailsWindow.xaml
    /// </summary>
    public partial class TaskDetailsWindow : Window
    {
        private TaskRepository taskRepository = new TaskRepository();
        private TaskItem taskItem;

        public TaskDetailsWindow(TaskItem taskItem)
        {
            InitializeComponent();
            this.taskItem = taskItem;
            LoadTask();
            InitializeSelectors();
        }

         private void InitializeSelectors()
         {
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(TaskEnums.Priority));
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(TaskEnums.Category));
         }

        private void LoadTask()
        {
            TitleTextBox.Text = taskItem.Title;
            CreatorTextBox.Text = taskItem.Creator;
            PriorityComboBox.SelectedItem = taskItem.Priority;
            CategoryComboBox.SelectedItem = taskItem.Category;
            DueDatePicker.SelectedDate = taskItem.DueDate;
            DescriptionTextBox.Text = taskItem.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            taskItem.Title = TitleTextBox.Text;
            taskItem.Creator = CreatorTextBox.Text;
            taskItem.Priority = (TaskEnums.Priority)PriorityComboBox.SelectedItem;
            taskItem.Category = (TaskEnums.Category)CategoryComboBox.SelectedItem;
            taskItem.DueDate = DueDatePicker.SelectedDate;
            taskItem.Description = DescriptionTextBox.Text;

            taskRepository.Update(taskItem);

            this.Close();
        }
    }
}
