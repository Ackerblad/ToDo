using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            InitializePrioritySelector();
            InitializeCategorySelector();
            LoadTask();
        }

        private void InitializePrioritySelector()
        {
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(TaskEnums.Priority));
        }

        private void InitializeCategorySelector()
        {
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
