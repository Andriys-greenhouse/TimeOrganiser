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
        public string Headline { get; set; } = "Task";
        int packedLine = 2;
        int unpackedLine = 16;

        //Title line
        public string TitleText { get; set; } = "";
        public int TitleHeigth
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
                if (TitleErrText != "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        string TitleErrText 
        {
            get
            {
                if (TitleText.Length < 2) { return "Title must have at least 2 characteers."; }
                if (TitleText.Length > 20) { return "Title must have at most 20 characters."; }
                else { return ""; }
            }
        }

        //Description line
        public string DescrText { get; set; } = "";
        public int DescrHeigth
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
                if (DescrErrText != "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        string DescrErrText
        {
            get
            {
                if (DescrText.Length > 400) { return "Description must have at most 400 characters."; }
                else { return ""; }
            }
        }

        //Importance line
        public string ImpText { get; set; } = "";
        public int ImpHeigth
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
                if (ImpErrText != "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        string ImpErrText
        {
            get
            {
                if (!int.TryParse(ImpText, out int result) || result > 10 || result < 1) { return "Importance must be an integer between 1 and 10."; }
                else { return ""; }
            }
        }

        //Deadline line
        public string YearText { get; set; } = "";
        public string MonthText { get; set; } = "";
        public string DayText { get; set; } = "";
        public string HourText { get; set; } = "";
        public int DdlHeigth
        {
            get
            {
                if (DdlErrText == "") { return packedLine; }
                else { return unpackedLine; }
            }
        }
        public Visibility DdlErrVis
        {
            get
            {
                if (DdlErrText != "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        string DdlErrText
        {
            get //ENDED HERE - we need to make verification and set getter accordingly (hour can remain be empty)
            {
                
            }
        }

        public TaskWindow()
        {
            InitializeComponent();
        }
        public TaskWindow(Task ToEdit)
        {
            Headline = "New Task";

            InitializeComponent();
        }
    }
}
