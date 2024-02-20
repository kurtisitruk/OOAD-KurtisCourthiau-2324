using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TaskManager
{
    public partial class MainWindow : Window
    {
        private Stack<ListBoxItem> removedItems = new Stack<ListBoxItem>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckForm(object sender, RoutedEventArgs e)
        {
            addButton.IsEnabled = !string.IsNullOrWhiteSpace(taskTextBox.Text) && priorityComboBox.SelectedItem != null;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var taskItem = new ListBoxItem();
            taskItem.Content = taskTextBox.Text;
            taskItem.Foreground = Brushes.Black;

            switch (((ComboBoxItem)priorityComboBox.SelectedItem).Content.ToString())
            {
                case "Low":
                    taskItem.Background = Brushes.Green;
                    taskItem.ToolTip = "Low Priority";
                    break;
                case "Medium":
                    taskItem.Background = Brushes.Yellow;
                    taskItem.ToolTip = "Medium Priority";
                    break;
                case "High":
                    taskItem.Background = Brushes.Red;
                    taskItem.ToolTip = "High Priority";
                    break;
            }

            taskListBox.Items.Add(taskItem);

            taskTextBox.Clear();
            priorityComboBox.SelectedIndex = -1;

            CheckForm(null, null); // Clear any error messages
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                var selectedItem = (ListBoxItem)taskListBox.SelectedItem;
                removedItems.Push(selectedItem);
                taskListBox.Items.Remove(selectedItem);
                restoreButton.IsEnabled = true;
                deleteButton.IsEnabled = false;
            }
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (removedItems.Count > 0)
            {
                var removedItem = removedItems.Pop();
                taskListBox.Items.Add(removedItem);
                restoreButton.IsEnabled = removedItems.Count > 0;
                deleteButton.IsEnabled = true;
            }
        }

        private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteButton.IsEnabled = taskListBox.SelectedItem != null;
        }
    }
}