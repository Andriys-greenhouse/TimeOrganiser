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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TimeOrganiser
{
    /// <summary>
    /// Interaction logic for NewBarWindow.xaml
    /// </summary>
    public partial class NewBarWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Bar ThisBar { get; set; } = new Bar(new DateTime(), new ObservableCollection<Segment>());
        bool AttemptedToSubmit = false;
        public bool HandedIn = false;
        int packedLine = 2;
        int unpackedLine = 18;
        SegmentWindow actualSegmentWindow;
        static ObservableCollection<Segment> ExistingSegments = new ObservableCollection<Segment>();
        static int lenOfSepSeg;

        //Start line
        public string HourText { get; set; } = "";
        public string MinuteText { get; set; } = "";
        public int StartErrHeigth
        {
            get
            {
                if (StartErrText == "") { return packedLine; }
                else { return unpackedLine; }
            }
        }
        public Visibility StartErrVis
        {
            get
            {
                if (StartErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        public string StartErrText
        {
            get
            {
                if (AttemptedToSubmit && !((int.TryParse(MinuteText, out int min) && min >= 0 && min < 60 && int.TryParse(HourText, out int hr) && hr >= 0 && hr <= 23) ||
                    (MinuteText.Length == 0 && int.TryParse(HourText, out int hr2) && hr2 >= 0 && hr2 <= 23)))
                { return "Invalid time format"; }
                else { return ""; }
            }
        }

        public NewBarWindow(ObservableCollection<Segment> aExistingSegments, int aLengthOfSeparatingSegment)
        {
            ExistingSegments = aExistingSegments;
            lenOfSepSeg = aLengthOfSeparatingSegment;
            InitializeComponent();
            ExistingSegmentsView.DataContext = ExistingSegments;
            InsideSegmentsView.DataContext = ThisBar.Content;
        }

        private void RightButt_Click(object sender, RoutedEventArgs e)
        {
            if (ExistingSegmentsView.SelectedIndex != -1) { ThisBar.Content.Add(ExistingSegments[ExistingSegmentsView.SelectedIndex]) ; }
        }

        private void LeftButt_Click(object sender, RoutedEventArgs e)
        {
            if (InsideSegmentsView.SelectedIndex != -1) { ThisBar.Content.RemoveAt(InsideSegmentsView.SelectedIndex); }
        }

        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            actualSegmentWindow = new SegmentWindow();
            actualSegmentWindow.ShowDialog();
            if (actualSegmentWindow.HandedIn)
            {
                ExistingSegments.Add(new Segment(actualSegmentWindow.TitleText, actualSegmentWindow.DescrText,
                    int.Parse(actualSegmentWindow.DurText), ColorWithName.MyColors[actualSegmentWindow.ColorPick.SelectedIndex].Word,
                    ColorWithName.MyColors[actualSegmentWindow.ColorPick.SelectedIndex].Color.Color));
            }
        }

        private void DelButt_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show($"Do you really want to delete {ExistingSegments[ExistingSegmentsView.SelectedIndex].Title} item?", "Confirmation", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    ExistingSegments.Remove(ExistingSegments[ExistingSegmentsView.SelectedIndex]);
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }

        private void CreateButt_Click(object sender, RoutedEventArgs e)
        {
            AttemptedToSubmit = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartErrHeigth"));

            if (StartErrText == "")
            {
                int overallDuration = 0;
                foreach (Segment seg in ThisBar.Content)
                {
                    overallDuration += seg.Duration;
                }

                if (overallDuration + int.Parse(MinuteText) + int.Parse(HourText) * 60 + (ThisBar.Content.Count - 1) * lenOfSepSeg <= 1440)
                {
                    HandedIn = true;
                    Close();
                }
                else { MessageBox.Show("Sum of durations of new Bar's segments (starting at given start point of the day)\nexceeds 24 hours of Earth's rotation (of the day)."); } 
            } 
            else { MessageBox.Show("Some text box in Start field contain invalid values."); }
        }

        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            HandedIn = false;
            Close();
        }

        private void ExistingSegmentsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            actualSegmentWindow = new SegmentWindow(ExistingSegments[ExistingSegmentsView.SelectedIndex]);
            actualSegmentWindow.ShowDialog();
            if (actualSegmentWindow.HandedIn)
            {
                ExistingSegments[ExistingSegmentsView.SelectedIndex].Title = actualSegmentWindow.TitleText;
                ExistingSegments[ExistingSegmentsView.SelectedIndex].Description = actualSegmentWindow.DescrText;
                ExistingSegments[ExistingSegmentsView.SelectedIndex].Duration = int.Parse(actualSegmentWindow.DurText);
                ExistingSegments[ExistingSegmentsView.SelectedIndex].BackgroundColor = ColorWithName.MyColors[actualSegmentWindow.ColorPick.SelectedIndex];
            }
        }

        private void SomeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartErrHeigth"));
        }
    }
}
