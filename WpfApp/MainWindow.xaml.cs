using System;
using System.Windows;
using WpfApp;
using Xceed.Wpf.Toolkit;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private DataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();
            dataContext = new DataContext();
            DataContext = dataContext;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var task = new TaskItem
            {
                Name = nameTextBox.Text,
                Schedule = datePicker.SelectedDate?.ToString("dd/MM/yyyy") + " " + hourComboBox.SelectedItem?.ToString(),
                IsComplete = false
            };

            dataContext.AddTask(task);

            nameTextBox.Clear();
            datePicker.SelectedDate = null;
            hourComboBox.SelectedItem = null;
        }
    }
}
