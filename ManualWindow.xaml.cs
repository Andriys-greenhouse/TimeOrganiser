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

namespace TimeOrganiser
{
    /// <summary>
    /// Interaction logic for ManualWindow.xaml
    /// </summary>
    public partial class ManualWindow : Window
    {
        public string AboutDescription { get; set; } = 
            "This application is designed to help you to schedule your time and to track your tasks. " +
            "Time planning is done by breaking your time to segments (like: breakfast, school, prayer time,...) which user arranges to order he likes." +
            "Tasks are evaluated and ordered by criteria given by the user.";
        public string MainWindowDescription { get; set; } = "Main window is like a hub from which user is able to access all features." +
            "by double clicking";
        public string NewBarWindowDescription { get; set; } = "";
        public string SettingsWindowDescription { get; set; } = "";
        public string TaskWindowDescription { get; set; } = "";
        public string SegmentWindowDescription { get; set; } = "";

        public ManualWindow()
        {
            InitializeComponent();
        }

        private void OkButt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
