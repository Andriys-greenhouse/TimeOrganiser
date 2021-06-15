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
        int packedLine = 2;
        int unpackedLine = 16;
        int extraUnpackedLine = 37;

        public event PropertyChangedEventHandler PropertyChanged;
        public SettingsWindow(double CurentImportanceCoeficient, double CurentTimeCoeficient, double CurentLengthOfSepSegment)
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
                if ( AtemptedToSubmit && (!double.TryParse(ImportanceText, out double result1) || !(result1 >= 0 && result1 <= 99) ||
                    !double.TryParse(TimeText, out double result2) || !(result2 >= 0 && result2 <= 99)) )
                { return "Coeficient must be number between 0 and 99."; }
                else { return ""; }
            }
        }

        public int WayErrHeight
        {
            get
            {
                if (WayErrText == "") { return packedLine; }
                else { return unpackedLine; }
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
                if (AtemptedToSubmit && (!double.TryParse(LengthText, out double result) || !(result == 0 || (result >= 10 && result <= 30))))
                { return "Separating segment must be either 0 (for non)\nor between 10 and 30 minutes long."; }
                else { return ""; }
            }
        }

        public int LenErrHeight
        {
            get
            {
                if (LenErrText == "") { return packedLine; }
                else { return extraUnpackedLine; }
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
