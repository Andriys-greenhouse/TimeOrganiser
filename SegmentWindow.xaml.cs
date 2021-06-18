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
using System.Collections.ObjectModel;

namespace TimeOrganiser
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public class ColorWithName
    {
        public static ObservableCollection<ColorWithName> MyColors { get; set; } = new ObservableCollection<ColorWithName>();
        public string Word { get; set; }
        public SolidColorBrush Color { get; set; }
        public ColorWithName(string aWord, SolidColorBrush aColor)
        {
            InitColors();
            Word = aWord;
            Color = aColor;
        }
        static bool initialized = false;
        public static Dictionary<string, SolidColorBrush> ColorsDic { get; set; } = new Dictionary<string, SolidColorBrush>();
        public static void InitColors()
        {
            if (!initialized)
            {
                MyColors.Add(new ColorWithName("Red", new SolidColorBrush(Colors.Red)));
                ColorsDic.Add("Red", new SolidColorBrush(Colors.Red));
                MyColors.Add(new ColorWithName("Green", new SolidColorBrush(Colors.Green)));
                ColorsDic.Add("Green", new SolidColorBrush(Colors.Green));
                MyColors.Add(new ColorWithName("Blue", new SolidColorBrush(Colors.Blue)));
                ColorsDic.Add("Blue", new SolidColorBrush(Colors.Blue));
                MyColors.Add(new ColorWithName("Yellow", new SolidColorBrush(Colors.Yellow)));
                ColorsDic.Add("Yellow", new SolidColorBrush(Colors.Yellow));
                MyColors.Add(new ColorWithName("Orange", new SolidColorBrush(Colors.Orange)));
                ColorsDic.Add("Orange", new SolidColorBrush(Colors.Orange));
                MyColors.Add(new ColorWithName("Brown", new SolidColorBrush(Colors.Brown)));
                ColorsDic.Add("Brown", new SolidColorBrush(Colors.Brown));
                MyColors.Add(new ColorWithName("Aqua", new SolidColorBrush(Colors.Aqua)));
                ColorsDic.Add("Aqua", new SolidColorBrush(Colors.Aqua));
                MyColors.Add(new ColorWithName("Olive", new SolidColorBrush(Colors.Olive)));
                ColorsDic.Add("Olive", new SolidColorBrush(Colors.Olive));
                MyColors.Add(new ColorWithName("Azure", new SolidColorBrush(Colors.Azure)));
                ColorsDic.Add("Azure", new SolidColorBrush(Colors.Azure));
                MyColors.Add(new ColorWithName("Pink", new SolidColorBrush(Colors.Pink)));
                ColorsDic.Add("Pink", new SolidColorBrush(Colors.Pink));
                MyColors.Add(new ColorWithName("Violet", new SolidColorBrush(Colors.Violet)));
                ColorsDic.Add("Violet", new SolidColorBrush(Colors.Violet));
                initialized = true;
            }
        }
    }
    public partial class SegmentWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Headline { get; set; } = "New Segment";
        int packedLine = 2;
        int unpackedLine = 18;
        bool AttemptedToSubmit = false;
        public bool HandedIn = false;

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
                if (AttemptedToSubmit && TitleText.Length < 2) { return "Title must have at least 2 characters."; }
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

        //Duration line
        public string DurText { get; set; } = "";
        public int DurErrHeigth
        {
            get
            {
                if (DurErrText == "") { return packedLine; }
                else { return unpackedLine; }
            }
        }
        public Visibility DurErrVis
        {
            get
            {
                if (DurErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        public string DurErrText
        {
            get
            {
                if (AttemptedToSubmit && (!int.TryParse(DurText, out int result) || result > 1439 || result < 10)) { return "Duration must be an integer between 10 and 1439."; }
                else { return ""; }
            }
        }

        public SegmentWindow()
        {
            ColorWithName.InitColors();
            DataContext = this;
            ColorPick.DataContext = ColorWithName.MyColors;

            InitializeComponent();
        }
        public SegmentWindow(Segment ToEdit)
        {
            Headline = "Segment";
            TitleText = ToEdit.Title;
            DescrText = ToEdit.Description;
            DurText = ToEdit.Duration.ToString();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Headline"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DurText"));

            ColorWithName.InitColors();
            DataContext = this;
            ColorPick.DataContext = ColorWithName.MyColors;

            InitializeComponent();
        }

        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrVis"));
        }

        private void DescrTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrVis"));
        }

        private void DurTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DurErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DurErrTextHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DurErrVis"));
        }

        private void OneOfDdlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrTextHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrVis"));
        }

        private void ConfirmButt_Click(object sender, RoutedEventArgs e)
        {
            AttemptedToSubmit = true;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TitleErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescrErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DurErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DurErrTextHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DurErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrTextHeigth"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DdlErrVis"));

            if (TitleErrText == "" && DescrErrText== "" && DurErrText == "")
            {
                HandedIn = true;
                Close();
            }
            else { MessageBox.Show("Some fields contain invalid values.", "Warning"); }
        }

        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
