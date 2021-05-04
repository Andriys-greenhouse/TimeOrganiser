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
        ObservableCollection<Segment> BarSegments { get; set; } = new ObservableCollection<Segment>();

        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindow()
        {
            InitializeComponent();

            BarSegments.Add(new Segment("sub 1", "", 50, Colors.Red));
            BarSegments.Add(new Segment("sub 2", "", 200, Colors.Green));
            BarSegments.Add(new Segment("sub 3", "", 300, Colors.Blue));
            BarSegments.Add(new Segment("sub 4", "", 400, Colors.Brown));
            BarSegments.Add(new Segment("sub 5", "", 10, Colors.Yellow));

            //put this at end of created bar
            int lenghtOfFinalSegment = 1440;
            foreach(Segment seg in BarSegments)
            {
                lenghtOfFinalSegment -= seg.Lenght;
            }
            BarSegments.Add(new Segment("unspecified", "", lenghtOfFinalSegment, Colors.Gray));
            RealBar.DataContext = BarSegments;
            DetailBar.DataContext = BarSegments;
        }
    }
}