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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TimeOrganiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Bar CurentBar { get; set; } = new Bar(new DateTime(2021, 5, 9, 8, 0, 0), new ObservableCollection<Segment>());
        ObservableCollection<Task> Tasks { get; set; } = new ObservableCollection<Task>();
        public double TimeFactor { get; set; } = 1;
        public double ImportanceFactor { get; set; } = 3;
        public double LengthOfSepSegment { get; set; } = 15;
        TaskWindow CurentTaskWindow;

        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindow()
        {
            InitializeComponent();

            CurentBar.Content.Add(new Segment("sub 1", "", 50, Colors.Red));
            CurentBar.Content.Add(new Segment("sub 2", "", 200, Colors.Green));
            CurentBar.Content.Add(new Segment("sub 3", "", 300, Colors.Blue));
            CurentBar.Content.Add(new Segment("sub 4", "", 400, Colors.Brown));
            CurentBar.Content.Add(new Segment("sub 5", "", 10, Colors.Yellow));

            //put this at end of created bar
            int lenghtOfFinalSegment = 1440;
            foreach(Segment seg in CurentBar.Content)
            {
                lenghtOfFinalSegment -= seg.Lenght;
            }
            CurentBar.Content.Add(new Segment("unspecified", "", lenghtOfFinalSegment, Colors.Gray));

            Tasks.Add(new Task("Hello", "", 5, new DateTime(2021, 5, 10)));
            Tasks.Add(new Task("World", "", 7, new DateTime(2021, 6, 10, 12, 0, 0)));
            Tasks.Add(new Task("Kvarteto", "udělat obrázky", 8, new DateTime(2021, 5, 31)));

            RealBar.DataContext = CurentBar;
            DetailBar.DataContext = CurentBar;
            TaskView.DataContext = Tasks;
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskDetails.DataContext = TaskView.SelectedItem;
        }

        private void DeleteAllButt_Click(object sender, RoutedEventArgs e)
        {
            List<Task> toDelete = new List<Task>();
            foreach(Task member in Tasks)
            {
                if (member.Solved) { toDelete.Add(member); }
            }

            switch (MessageBox.Show($"Do you really want to delete {toDelete.Count} items?", "Confirmation", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    foreach (Task member in toDelete) { Tasks.Remove(member); }
                    break;
                case MessageBoxResult.No:
                    return;

            }
        }

        private void Bar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CuretnBar.BarSelectedIndex"));
        }

        private void SettingsButt_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sett = new SettingsWindow(ImportanceFactor, TimeFactor, LengthOfSepSegment);
            sett.ShowDialog();
            if (sett.IsOk)
            {
                ImportanceFactor = double.Parse(sett.ImportanceText);
                TimeFactor = double.Parse(sett.TimeText);
                LengthOfSepSegment = double.Parse(sett.LengthText);
            }
        }

        private void TaskView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CurentTaskWindow = new TaskWindow(Tasks[TaskView.SelectedIndex]);
            CurentTaskWindow.ShowDialog();
            Tasks[TaskView.SelectedIndex].Title = CurentTaskWindow.TitleText;
            Tasks[TaskView.SelectedIndex].Description = CurentTaskWindow.DescrText;
            Tasks[TaskView.SelectedIndex].Importance = int.Parse(CurentTaskWindow.ImpText);
            Tasks[TaskView.SelectedIndex].Deadline = new DateTime
                (
                int.Parse(CurentTaskWindow.YearText), 
                int.Parse(CurentTaskWindow.MonthText),
                int.Parse(CurentTaskWindow.DayText),
                CurentTaskWindow.HourText == "" ? 0 : int.Parse(CurentTaskWindow.HourText), 
                0, 0
                );
        }

        private void NewTaskButt_Click(object sender, RoutedEventArgs e)
        {
            CurentTaskWindow = new TaskWindow();
            CurentTaskWindow.ShowDialog();
            if (CurentTaskWindow.HandedIn)
            {
                Tasks.Add(new Task(CurentTaskWindow.TitleText, CurentTaskWindow.DescrText, int.Parse(CurentTaskWindow.ImpText), new DateTime
                    (
                        int.Parse(CurentTaskWindow.YearText),
                        int.Parse(CurentTaskWindow.MonthText),
                        int.Parse(CurentTaskWindow.DayText),
                        CurentTaskWindow.HourText == "" ? 0 : int.Parse(CurentTaskWindow.HourText),
                        0, 0
                    )));
            }
        }
    }
}

/*
 To do:
fix dispaying of error texts in Settings window
add windows,
saving
 */