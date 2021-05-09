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
        public decimal TimeCoeficient { get; set; } = 1;
        public decimal ImportanceCoeficient { get; set; } = 3;
        public decimal LengthOfSepSegment { get; set; } = 15;

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
            SettingsWindow sett = new SettingsWindow(ImportanceCoeficient, TimeCoeficient, LengthOfSepSegment);
            sett.ShowDialog();
            if (sett.IsOk)
            {
                ImportanceCoeficient = decimal.Parse(sett.ImportanceText);
                TimeCoeficient = decimal.Parse(sett.TimeText);
                LengthOfSepSegment = decimal.Parse(sett.LengthText);
            }
        }
    }
}