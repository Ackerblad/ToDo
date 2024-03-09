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
            LoadTask();
        }

        private void LoadTask()
        {
            TitleTextBox.Text = taskItem.Title;
            CreatorTextBox.Text = taskItem.Creator;
            PriorityComboBox.SelectedItem = PriorityComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == taskItem.Priority);
            CategoryComboBox.SelectedItem = PriorityComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == taskItem.Category);
            DueDatePicker.SelectedDate = taskItem.DueDate;
            DescriptionTextBox.Text = taskItem.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            taskItem.Title = TitleTextBox.Text;
            taskItem.Creator = CreatorTextBox.Text;
            taskItem.Priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString();
            taskItem.Category = ((ComboBoxItem)CategoryComboBox.SelectedItem)?.Content.ToString();
            taskItem.DueDate = DueDatePicker.SelectedDate;
            taskItem.Description = DescriptionTextBox.Text;

            taskRepository.Update(taskItem);

            this.Close();
        }
    }
}
