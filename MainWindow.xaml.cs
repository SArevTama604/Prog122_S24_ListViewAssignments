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

namespace Prog122_S24_ListViewAssignments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Student> students = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
            UpdateListView();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                var student = new Student
                {
                    Name = NameTextBox.Text,
                    Grade1 = double.Parse(Grade1TextBox.Text),
                    Grade2 = double.Parse(Grade2TextBox.Text)
                };
                students.Add(student);
                UpdateListView();
                ClearInputs();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsListView.SelectedItem is Student selectedStudent && ValidateInputs())
            {
                selectedStudent.Name = NameTextBox.Text;
                selectedStudent.Grade1 = double.Parse(Grade1TextBox.Text);
                selectedStudent.Grade2 = double.Parse(Grade2TextBox.Text);
                UpdateListView();
                ClearInputs();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsListView.SelectedItem is Student selectedStudent)
            {
                students.Remove(selectedStudent);
                UpdateListView();
            }
        }

        private void UpdateListView()
        {
            if (students.Count < 5)
            {
                var placeholderStudents = Enumerable.Range(students.Count, 5 - students.Count)
                                                    .Select(i => new Student { Name = $"Placeholder {i}", Grade1 = 0, Grade2 = 0 });
                StudentsListView.ItemsSource = students.Concat(placeholderStudents).Select(s => new
                {
                    s.Name,
                    s.Grade1,
                    s.Grade2,
                    AverageGrade = s.GetAverageGrade()
                }).ToList();
            }
            else
            {
                StudentsListView.ItemsSource = students.Select(s => new
                {
                    s.Name,
                    s.Grade1,
                    s.Grade2,
                    AverageGrade = s.GetAverageGrade()
                }).ToList();
            }
        }

        private void ClearInputs()
        {
            NameTextBox.Clear();
            Grade1TextBox.Clear();
            Grade2TextBox.Clear();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(NameTextBox.Text) || !double.TryParse(Grade1TextBox.Text, out var grade1) || !double.TryParse(Grade2TextBox.Text, out var grade2) || grade1 < 0 || grade1 > 100 || grade2 < 0 || grade2 > 100)
            {
                MessageBox.Show("Invalid input. Please check the values entered.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}