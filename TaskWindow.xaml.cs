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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Headline { get; set; } = "New Task";
        int packedLine = 2;
        int unpackedLine = 16;
        bool AttemptedToSubmit = false;
        public bool HandedIn = false;
        bool Editing = false;

        //Title line
        public string TitleText { get; set; } = "";
        public int TitleErrHeigth
        {
            get
            {
                if (TitleErrText == "") { return packedLine; }
                else { return unpackedLine; }
            }
        }
        public Visibility TitleErrVis
        {
            get
            {
                if (TitleErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        public string TitleErrText 
        {
            get
            {
                if (AttemptedToSubmit && TitleText.Length < 2) { return "Title must have at least 2 characteers."; }
                if (AttemptedToSubmit && TitleText.Length > 20) { return "Title must have at most 20 characters."; }
                else { return ""; }
            }
        }

        //Description line
        public string DescrText { get; set; } = "";
        public int DescrErrHeigth
        {
            get
            {
                if (DescrErrText == "") { return packedLine; }
                else { return unpackedLine; }
            }
        }
        public Visibility DescrErrVis
        {
            get
            {
                if (DescrErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        public string DescrErrText
        {
            get
            {
                if (AttemptedToSubmit & DescrText.Length > 400) { return "Description must have at most 400 characters."; }
                else { return ""; }
            }
        }

        //Importance line
        public string ImpText { get; set; } = "";
        public int ImpErrHeigth
        {
            get
            {
                if (ImpErrText == "") { return packedLine; }
                else { return unpackedLine; }
            }
        }
        public Visibility ImpErrVis
        {
            get
            {
                if (ImpErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        public string ImpErrText
        {
            get
            {
                if (AttemptedToSubmit && (!int.TryParse(ImpText, out int result) || result > 10 || result < 1)) { return "Importance must be an integer between 1 and 10."; }
                else { return ""; }
            }
        }

        //Deadline line
        public string YearText { get; set; } = "";
        public string MonthText { get; set; } = "";
        public string DayText { get; set; } = "";
        public string HourText { get; set; } = "";
        public int DdlErrHeigth
        {
            get
            {
                if (DdlErrText == "") { return packedLine; }
                else { return unpackedLine * 3; }
            }
        }
        public Visibility DdlErrVis
        {
            get
            {
                if (DdlErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        public string DdlErrText
        {
            get
            {
                if (AttemptedToSubmit && !((int.TryParse(YearText, out int a) && int.TryParse(MonthText, out int b) && 
                    int.TryParse(DayText, out int c) && int.TryParse(HourText, out int d) && new DateTime(a, b, c, d, 0, 0) >= DateTime.Now) ||
                    (HourText == "" && DateTime.TryParse(YearText + "-" + MonthText + "-" + DayText, out DateTime result) && result >= DateTime.Now)))
                { return "Invalid format of year, month, day or hour input.\n(hour can be left blank)\n(date must be from now on)"; }
                else { return ""; }
            }
        }

        public TaskWindow()
        {
            DataContext = this;

            InitializeComponent();
        }
        public TaskWindow(Task ToEdit)
        {
            Headline = "Task";
            Editing = true;
            TitleText = ToEdit.Title;
            DescrText = ToEdit.Description;
            ImpText = ToEdit.Importance.ToString();
            YearText = ToEdit.Deadline.Year.ToString();
            MonthText = ToEdit.Deadline.Month.ToString();
            DayText = ToEdit.Deadline.Day.ToString();
            if (ToEdit.Deadline.Hour != 0) { HourText = ToEdit.Deadline.Hour.ToString(); }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Headline"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImpText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("YearText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MonthText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DayText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HourText"));

            DataContext = this;

            InitializeComponent();
        }

        bool ContainsErrors()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImpErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrText"));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImpErrTextHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrTextHeigth"));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImpErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrVis"));

            if(TitleErrText == "" && DescrErrText == "" && ImpErrText == "" && DdlErrText == "") { return false; }
            else { return true; }
        }

        private void OneOfTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContainsErrors();
        }

        private void ConfirmButt_Click(object sender, RoutedEventArgs e)
        {
            AttemptedToSubmit = true;
            if(ContainsErrors()) { MessageBox.Show("Some fields contain invalid values."); }
            else
            {
                HandedIn = true;
                Close();
            }
        }

        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (!Editing) { base.OnClosed(e); }
            else { MessageBox.Show("Editation of existing Task can't be canceled."); }
        }
    }
}
