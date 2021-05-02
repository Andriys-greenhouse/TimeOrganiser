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

namespace TimeOrganiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Segment> BarsSegments = new ObservableCollection<Segment>();
        public MainWindow()
        {
            BarsSegments.Add(new Segment("sub 1", "", 5));
            BarsSegments.Add(new Segment("sub 2", "", 10));
            BarsSegments.Add(new Segment("sub 3", "", 15));
            BarsSegments.Add(new Segment("sub 4", "", 20));
            BarsSegments.Add(new Segment("sub 5", "", 5));
            InitializeComponent();
        }
    }
}
