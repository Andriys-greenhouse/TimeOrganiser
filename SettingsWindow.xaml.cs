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
using System.ComponentModel;

namespace TimeOrganiser
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        bool AtemptedToSubmit { get; set; } = false;
        public bool IsOk { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public SettingsWindow(decimal CurentImportanceCoeficient, decimal CurentTimeCoeficient, decimal CurentLengthOfSepSegment)
        {
            InitializeComponent();
            ImportanceText = CurentImportanceCoeficient.ToString();
            TimeText = CurentTimeCoeficient.ToString();
            LengthText = CurentLengthOfSepSegment.ToString();
            DataContext = this;
        }

        public string ImportanceText { get; set; }
        public string TimeText { get; set; }
        public string LengthText { get; set; }

        //Sorting
        public string WayErrText
        {
            get
            {
                if ( AtemptedToSubmit && (!decimal.TryParse(ImportanceText, out decimal result1) || !(result1 >= 0 && result1 <= 99) ||
                    !decimal.TryParse(TimeText, out decimal result2) || !(result2 >= 0 && result2 <= 99)) )
                { return "Coeficient must be number between 0 and 99"; }
                else { return ""; }
            }
        }

        public int WayErrHeight
        {
            get
            {
                if (WayErrText == "") { return 2; }
                else { return 15; }
            }
        }

        public Visibility WayErrVis
        {
            get
            {
                if (WayErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }

        private void ImportanceBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrVis"));
        }

        private void TimeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrVis"));
        }




        //Length of separating segment
        public string LenErrText
        {
            get
            {
                if (AtemptedToSubmit && (!decimal.TryParse(LengthText, out decimal result) || !(result == 0 || (result >= 10 && result <= 30))))
                { return "Separating segment must be either 0 (for non)\nor between 10 and 30 minutes long."; }
                else { return ""; }
            }
        }

        public int LenErrHeight
        {
            get
            {
                if (LenErrText == "") { return 2; }
                else { return 37; }
            }
        }

        public Visibility LenErrVis
        {
            get
            {
                if (LenErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        private void LengthBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LenErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LenErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LenErrVis"));
        }

        private void ConfirmButt_Click(object sender, RoutedEventArgs e)
        {
            AtemptedToSubmit = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LenErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LenErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LenErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WayErrVis"));

            if (LenErrText != "" || WayErrText != "") { MessageBox.Show("Some fields contains invalid values.","Warning"); }
            else
            {
                IsOk = true;
                Close();
            }
        }

        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
