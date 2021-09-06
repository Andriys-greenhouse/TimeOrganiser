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
using System.IO;

namespace TimeOrganiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Bar CurentBar { get; set; } = new Bar(new DateTime(2021, 5, 9, 8, 0, 0), new ObservableCollection<Segment>());
        ObservableCollection<Task> Tasks { get; set; } = new ObservableCollection<Task>();
        ObservableCollection<Segment> Segments { get; set; } = new ObservableCollection<Segment>();
        public int ImportanceFactor { get; set; } = 3;
        public int TimeFactor { get; set; } = 1;
        public int LengthOfSepSegment { get; set; } = 15;
        TaskWindow CurentTaskWindow;
        SettingsWindow CurentSettingsWindow;
        NewBarWindow CurentNewBarWindow;
        ManualWindow CurentManualWindow = new ManualWindow();
        LoadingResult CurentLoadingResult = new LoadingResult();

        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindow()
        {
            InitializeComponent();

            CurentLoadingResult = LoadAll();
            CurentBar = CurentLoadingResult.InsideBar;
            Tasks = CurentLoadingResult.InsideTasks;
            Segments = CurentLoadingResult.InsideSegments;
            ImportanceFactor = CurentLoadingResult.InsideImportanceFactor;
            TimeFactor = CurentLoadingResult.InsideTimeFactor;
            LengthOfSepSegment = CurentLoadingResult.InsideLengthOfSepSegment;


            if (CurentBar.Content.Count == 0)
            {
                CurentBar.Content.Add(new Segment("exmp 1", "", 50, "Red", Colors.Red));
                CurentBar.Content.Add(new Segment("exmp 2", "", 200, "Green", Colors.Green));
                CurentBar.Content.Add(new Segment("exmp 3", "", 300, "Blue", Colors.Blue));
                CurentBar.Content.Add(new Segment("exmp 4", "", 400, "Brown", Colors.Brown));
                CurentBar.Content.Add(new Segment("exmp 5", "", 10, "Yellow", Colors.Yellow));

                //put this at end of created bar
                int lenghtOfFinalSegment = 1440;
                foreach (Segment seg in CurentBar.Content)
                {
                    lenghtOfFinalSegment -= seg.Duration;
                }
                CurentBar.Content.Add(new Segment("unspecified", "", lenghtOfFinalSegment, "Gray", Colors.Gray));
            }

            if (Tasks.Count == 0)
            {
                Tasks.Add(new Task("Hello", "", 5, new DateTime(2021, 5, 10)));
                Tasks.Add(new Task("World", "", 7, new DateTime(2021, 6, 10, 12, 0, 0)));
            }

            DataContext = this;
            RealBar.DataContext = CurentBar;
            DetailBar.DataContext = CurentBar;
            TaskView.DataContext = Tasks;

            ColorWithName.InitColors();
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

            SaveAll(Tasks, Segments, ImportanceFactor, TimeFactor, LengthOfSepSegment, CurentBar);
            CurentLoadingResult = LoadAll();
            CurentBar = CurentLoadingResult.InsideBar;
            Tasks = CurentLoadingResult.InsideTasks;
            Segments = CurentLoadingResult.InsideSegments;
            ImportanceFactor = CurentLoadingResult.InsideImportanceFactor;
            TimeFactor = CurentLoadingResult.InsideTimeFactor;
            LengthOfSepSegment = CurentLoadingResult.InsideLengthOfSepSegment;
        }

        private void Bar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CuretnBar.BarSelectedIndex"));
        }

        private void SettingsButt_Click(object sender, RoutedEventArgs e)
        {
            CurentSettingsWindow = new SettingsWindow(ImportanceFactor, TimeFactor, LengthOfSepSegment);
            CurentSettingsWindow.ShowDialog();
            if (CurentSettingsWindow.IsOk)
            {
                ImportanceFactor = int.Parse(CurentSettingsWindow.ImportanceText);
                TimeFactor = int.Parse(CurentSettingsWindow.TimeText);
                LengthOfSepSegment = int.Parse(CurentSettingsWindow.LengthText);
            }

            SaveAll(Tasks, Segments, ImportanceFactor, TimeFactor, LengthOfSepSegment, CurentBar);
            CurentLoadingResult = LoadAll();
            CurentBar = CurentLoadingResult.InsideBar;
            Tasks = CurentLoadingResult.InsideTasks;
            Segments = CurentLoadingResult.InsideSegments;
            ImportanceFactor = CurentLoadingResult.InsideImportanceFactor;
            TimeFactor = CurentLoadingResult.InsideTimeFactor;
            LengthOfSepSegment = CurentLoadingResult.InsideLengthOfSepSegment;
        }

        private void TaskView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CurentTaskWindow = new TaskWindow(Tasks[TaskView.SelectedIndex]);
            CurentTaskWindow.ShowDialog();
            if (CurentTaskWindow.HandedIn)
            {
                Tasks[TaskView.SelectedIndex].Title = CurentTaskWindow.TitleText;
                Tasks[TaskView.SelectedIndex].Description = CurentTaskWindow.DescrText;
                Tasks[TaskView.SelectedIndex].Importance = int.Parse(CurentTaskWindow.ImpText);
                Tasks[TaskView.SelectedIndex].Deadline = new DateTime
                    (
                    int.Parse(CurentTaskWindow.YearText),
                    int.Parse(CurentTaskWindow.MonthText),
                    int.Parse(CurentTaskWindow.DayText),
                    CurentTaskWindow.HourText == "" ? 0 : int.Parse(CurentTaskWindow.HourText),
                    0, 0
                    );
            }

            SaveAll(Tasks, Segments, ImportanceFactor, TimeFactor, LengthOfSepSegment, CurentBar);
            CurentLoadingResult = LoadAll();
            CurentBar = CurentLoadingResult.InsideBar;
            Tasks = CurentLoadingResult.InsideTasks;
            Segments = CurentLoadingResult.InsideSegments;
            ImportanceFactor = CurentLoadingResult.InsideImportanceFactor;
            TimeFactor = CurentLoadingResult.InsideTimeFactor;
            LengthOfSepSegment = CurentLoadingResult.InsideLengthOfSepSegment;
        }

        private void NewTaskButt_Click(object sender, RoutedEventArgs e)
        {
            CurentTaskWindow = new TaskWindow();
            CurentTaskWindow.ShowDialog();
            if (CurentTaskWindow.HandedIn)
            {
                Tasks.Add(new Task(CurentTaskWindow.TitleText, CurentTaskWindow.DescrText, int.Parse(CurentTaskWindow.ImpText), new DateTime
                    (
                        int.Parse(CurentTaskWindow.YearText),
                        int.Parse(CurentTaskWindow.MonthText),
                        int.Parse(CurentTaskWindow.DayText),
                        CurentTaskWindow.HourText == "" ? 0 : int.Parse(CurentTaskWindow.HourText),
                        0, 0
                    )));
            }

            SaveAll(Tasks, Segments, ImportanceFactor, TimeFactor, LengthOfSepSegment, CurentBar);
            CurentLoadingResult = LoadAll();
            CurentBar = CurentLoadingResult.InsideBar;
            Tasks = CurentLoadingResult.InsideTasks;
            Segments = CurentLoadingResult.InsideSegments;
            ImportanceFactor = CurentLoadingResult.InsideImportanceFactor;
            TimeFactor = CurentLoadingResult.InsideTimeFactor;
            LengthOfSepSegment = CurentLoadingResult.InsideLengthOfSepSegment;
        }

        private void NewBarButt_Click(object sender, RoutedEventArgs e)
        {
            CurentNewBarWindow = new NewBarWindow(Segments, LengthOfSepSegment);
            CurentNewBarWindow.ShowDialog();
            if (CurentNewBarWindow.HandedIn)
            {
                CurentBar.Begening = CurentNewBarWindow.ThisBar.Begening;
                CurentBar.Content = CurentNewBarWindow.ThisBar.Content;

                int lenghtOfFinalSegment = 1440;
                foreach (Segment seg in CurentBar.Content)
                {
                    lenghtOfFinalSegment -= seg.Duration;
                }
                CurentBar.Content.Add(new Segment("unspecified", "", lenghtOfFinalSegment, "Gray", Colors.Gray));
            }

            SaveAll(Tasks, Segments, ImportanceFactor, TimeFactor, LengthOfSepSegment, CurentBar);
            CurentLoadingResult = LoadAll();
            CurentBar = CurentLoadingResult.InsideBar;
            Tasks = CurentLoadingResult.InsideTasks;
            Segments = CurentLoadingResult.InsideSegments;
            ImportanceFactor = CurentLoadingResult.InsideImportanceFactor;
            TimeFactor = CurentLoadingResult.InsideTimeFactor;
            LengthOfSepSegment = CurentLoadingResult.InsideLengthOfSepSegment;
        }

        private void ManualButt_Click(object sender, RoutedEventArgs e)
        {
            CurentManualWindow.ShowDialog();
        }









        public static string Crypt(string aInput, string aKey)
        {
            string final = "";
            string keyToUse = aKey;

            while (aInput.Length > keyToUse.Length)
            {
                keyToUse += aKey;
            }

            for (int i = 0; i < aInput.Length; i++)
            {
                final += (char)(aInput[i] ^ keyToUse[i]);
            }

            return final;
        }

        //Task
        public static void SaveTasks(string aPath, ObservableCollection<Task> aTasks, char aSeparator, string aKey)
        {
            StringBuilder sb = new StringBuilder(""); //it is faster than commutation of strings
            StringBuilder SBOfLines = new StringBuilder("");

            foreach (Task task in aTasks)
            {
                sb.Clear();

                sb.Append(aSeparator.ToString());
                sb.Append(task.Title);

                sb.Append(aSeparator.ToString());
                sb.Append(task.Description);

                sb.Append(aSeparator.ToString());
                sb.Append(task.Importance.ToString());

                sb.Append(aSeparator.ToString());
                sb.Append(task.Deadline.ToString("yyyy-MM-dd-HH-mm-ss"));

                sb.Append("\n");

                SBOfLines.Append(sb.ToString());
            }

            using (StreamWriter sr = new StreamWriter(aPath))
            {
                sr.Write(Crypt(SBOfLines.ToString(), aKey));
            }
        }

        public static ObservableCollection<Task> LoadTasks(string aFilename, char aSeparator, string aKey) //filename means path
        {
            //pokračovat zde
            if (!File.Exists(aFilename))
            {
                using (StreamWriter sr = new StreamWriter(aFilename)) { }
                throw new ArgumentException("Tasks file not found in the same folder.\n(so I created new one)");
            }
            List<int> faulty = new List<int>();
            List<string> lines;
            string afterCheck = "";
            using (StreamReader sr = File.OpenText(aFilename))//from https://docs.microsoft.com/cs-cz/dotnet/api/system.io.file.createtext?view=net-5.0
            {
                lines = Crypt(sr.ReadToEnd(), aKey).Split('\n').ToList();
                lines.RemoveAll(something => something == "");
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Count(something => something == aSeparator) != 4) { faulty.Add(i); }
                    else
                    {
                        string[] insides = lines[i].Substring(1).Split(aSeparator);
                        if (insides[0].Length < 2 || insides[0].Length > 20 || 
                            insides[1].Length > 400 || 
                            !int.TryParse(insides[2], out int value)  || value < 1 || value > 10 ||
                            !DateTime.TryParse(insides[3], out DateTime date))
                        { faulty.Add(i); }
                    }
                }
                for (int i = 0; i < lines.Count; i++)
                {
                    if (!faulty.Any(item => item == i))
                    { afterCheck += (i != lines.Count - 1) ? $"{lines[i]}\n" : lines[i].ToString(); }
                }
            }

            ObservableCollection<Task> final = new ObservableCollection<Task>();
            if (afterCheck != "")
            {
                foreach (string line in afterCheck.Split('\n'))
                {
                    string[] curent = line.Substring(1).Split(aSeparator);
                    curent[curent.Length - 1] = curent[curent.Length - 1].Trim('\n');
                    final.Add(new Task(curent[0], curent[1], int.Parse(curent[2]), DateTime.Parse(curent[3])));
                }
            }

            if (faulty.Count != 0)
            {
                if (afterCheck == "")
                {
                    return final;
                }
                SaveTasks(aFilename, final, aSeparator, aKey);
                throw new ArgumentException($"{faulty.Count} corupted tasks found (will be deleted).\nPlease check your tasks to see which are missing.");
            }

            return final;
        }

        //Segments
        public static void SaveSegments(string aPath, ObservableCollection<Segment> aSegments, char aSeparator, string aKey)
        {
            StringBuilder sb = new StringBuilder(); //it is faster than commutation of strings
            StringBuilder complete = new StringBuilder();

            foreach (Segment segment in aSegments)
            {
                sb.Clear();

                sb.Append(aSeparator.ToString());
                sb.Append(segment.Title);

                sb.Append(aSeparator.ToString());
                sb.Append(segment.Description);

                sb.Append(aSeparator.ToString());
                sb.Append(segment.Duration.ToString());

                sb.Append(aSeparator.ToString());
                sb.Append(segment.BackgroundColor.ToString());

                sb.Append("\n");
                complete.Append(sb.ToString());
            }

            using (StreamWriter sr = new StreamWriter(aPath))
            {
                sr.Write(Crypt(complete.ToString(), aKey));
            }
        }

        public static ObservableCollection<Segment> LoadSegments(string aFilename, char aSeparator, string aKey) //filename means path
        {
            if (!File.Exists(aFilename))
            {
                using (StreamWriter sr = new StreamWriter(aFilename)) { }
                throw new ArgumentException("Segments file not found in the same folder.\n(so I created new one)");
            }
            List<int> faulty = new List<int>();
            List<string> lines;
            string afterCheck = "";
            using (StreamReader sr = File.OpenText(aFilename))//from https://docs.microsoft.com/cs-cz/dotnet/api/system.io.file.createtext?view=net-5.0
            {
                lines = Crypt(sr.ReadToEnd(), aKey).Split('\n').ToList();
                lines.RemoveAll(something => something == "");
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Count(something => something == aSeparator) != 4) { faulty.Add(i); }
                    else
                    {
                        string[] insides = lines[i].Substring(1).Split(aSeparator);
                        if (insides[0].Length < 2 || insides[0].Length > 20 ||
                            insides[1].Length > 400 ||
                            !int.TryParse(insides[2], out int value) || value < 10 || value > 1440 ||
                            !ColorWithName.ColorsDic.ContainsKey(insides[3]))
                        { faulty.Add(i); }
                    }
                }
                for (int i = 0; i < lines.Count; i++)
                {
                    if (!faulty.Any(item => item == i))
                    { afterCheck += (i != lines.Count - 1) ? $"{lines[i]}\n" : lines[i].ToString(); }
                }
            }

            ObservableCollection<Segment> final = new ObservableCollection<Segment>();
            if (afterCheck != "")
            {
                foreach (string line in afterCheck.Split('\n'))
                {
                    string[] curent = line.Substring(1).Split(aSeparator);
                    curent[curent.Length - 1] = curent[curent.Length - 1].Trim('\n');
                    final.Add(new Segment(curent[0], curent[1], int.Parse(curent[2]), curent[3], ColorWithName.ColorsDic[curent[3]].Color));
                }
            }

            if (faulty.Count != 0)
            {
                if (afterCheck == "")
                {
                    return final;
                }
                SaveSegments(aFilename, final, aSeparator, aKey);
                throw new ArgumentException($"{faulty.Count} corupted segments found (will be deleted).\nPlease check your segments to see which are missing.");
            }

            return final;
        }

        //Settings
        public static void SaveSettings(string aPath, int aImportanceFactor, int aTimeFactor,int aLenOfSepSeg, char aSeparator, string aKey)
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append(aSeparator.ToString());
            sb.Append(aImportanceFactor.ToString());
            sb.Append(aSeparator.ToString());
            sb.Append(aTimeFactor.ToString());
            sb.Append(aSeparator.ToString());
            sb.Append(aLenOfSepSeg.ToString());

            using (StreamWriter sr = new StreamWriter(aPath))
            {
                sr.Write(Crypt(sb.ToString(), aKey));
            }
        }

        public static int[] LoadSettings(string aFilename, char aSeparator, string aKey) //filename means path
        {
            if (!File.Exists(aFilename))
            {
                using (StreamWriter sr = new StreamWriter(aFilename)) { }
                throw new ArgumentException("Settings file not found in the same folder.\n(so I created new one)");
            }
            List<int> faulty = new List<int>();
            List<string> lines = new List<string>();
            string afterCheck = "";
            using (StreamReader sr = File.OpenText(aFilename))//from https://docs.microsoft.com/cs-cz/dotnet/api/system.io.file.createtext?view=net-5.0
            {
                lines.Add(Crypt(sr.ReadToEnd(), aKey));
                lines.RemoveAll(something => something == "");
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Count(something => something == aSeparator) != 3) { faulty.Add(i); }
                    else
                    {
                        string[] insides = lines[i].Substring(1).Split(aSeparator);
                        if (!int.TryParse(insides[0], out int one) || one > 99 || one < 0 ||
                            !int.TryParse(insides[1], out int two) || two > 99 || two < 0 ||
                            !int.TryParse(insides[2], out int three) || !(three == 0 || (three != 0 && (three >= 10 && three <= 30))))
                        { faulty.Add(i); }
                    }
                }
                for (int i = 0; i < lines.Count; i++)
                {
                    if (!faulty.Any(item => item == i))
                    { afterCheck += (i != lines.Count - 1) ? $"{lines[i]}\n" : lines[i].ToString(); }
                }
            }

            int[] final = new int[3];
            if (afterCheck != "")
            {
                final[0] = int.Parse(afterCheck.Substring(1).Split(aSeparator)[0]);
                final[1] = int.Parse(afterCheck.Substring(1).Split(aSeparator)[1]);
                final[2] = int.Parse(afterCheck.Substring(1).Split(aSeparator)[2]);
            }

            if (faulty.Count != 0)
            {
                if (afterCheck == "")
                {
                    return new int[] { 3, 1, 15 };
                }
                SaveSettings(aFilename, 3, 1, 15, aSeparator, aKey);
                throw new ArgumentException("Corrupted settings detected (and deleted).\nSettings set to default values.");
            }

            return final;
        }

        //Bar
        public static void SaveBar(string aPath, Bar aBar, char aSeparator, string aKey)
        {
            string line = "";
            StringBuilder sb = new StringBuilder(line); //it is faster than commutation of strings
            string listOfLines = "";

            listOfLines += aBar.Begening.ToString("yyyy-MM-dd-HH-mm-ss") + "\n";
            foreach (Segment segment in aBar.Content)
            {
                sb.Clear();

                sb.Append(aSeparator.ToString());
                sb.Append(segment.Title);

                sb.Append(aSeparator.ToString());
                sb.Append(segment.Description);

                sb.Append(aSeparator.ToString());
                sb.Append(segment.Duration.ToString());

                sb.Append(aSeparator.ToString());
                sb.Append(segment.BackgroundColor.ToString());

                sb.Append("\n");
                listOfLines += sb.ToString();
            }

            listOfLines = Crypt(listOfLines, aKey);

            using (StreamWriter sr = new StreamWriter(aPath))
            {
                sr.Write(listOfLines);
            }
        }

        public static Bar LoadBar(string aFilename, char aSeparator, string aKey) //filename means path
        {
            if (!File.Exists(aFilename))
            {
                using (StreamWriter sr = new StreamWriter(aFilename)) { }
                throw new ArgumentException("Bar file not found in the same folder.\n(so I created new one)");
            }
            List<int> faulty = new List<int>();
            List<string> lines;
            string afterCheck = "";
            DateTime start = new DateTime();
            using (StreamReader sr = File.OpenText(aFilename))//from https://docs.microsoft.com/cs-cz/dotnet/api/system.io.file.createtext?view=net-5.0
            {
                lines = Crypt(sr.ReadToEnd(), aKey).Split('\n').ToList();
                lines.RemoveAll(something => something == "");
                if (lines.Count != 0)
                {
                    if (!DateTime.TryParseExact(lines[0], "yyyy-MM-dd-HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out start)) { throw new ArgumentException("In Bar file I found no valid Bar, creating a default one."); }
                    else { lines.RemoveAt(0); }
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Count(something => something == aSeparator) != 4) { faulty.Add(i); }
                        else
                        {
                            string[] insides = lines[i].Substring(1).Split(aSeparator);
                            if (insides[0].Length < 2 || insides[0].Length > 20 ||
                                insides[1].Length > 400 ||
                                !int.TryParse(insides[2], out int value) || value < 10 || value > 1440 ||
                                !ColorWithName.ColorsDic.ContainsKey(insides[3]))
                            { faulty.Add(i); }
                        }
                    }
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (!faulty.Any(item => item == i))
                        { afterCheck += (i != lines.Count - 1) ? $"{lines[i]}\n" : lines[i].ToString(); }
                    }
                }
            }

            ObservableCollection<Segment> final = new ObservableCollection<Segment>();
            if (afterCheck != "")
            {
                foreach (string line in afterCheck.Split('\n'))
                {
                    string[] curent = line.Substring(1).Split(aSeparator);
                    curent[curent.Length - 1] = curent[curent.Length - 1].Trim('\n');
                    final.Add(new Segment(curent[0], curent[1], int.Parse(curent[2]), curent[3], ColorWithName.ColorsDic[curent[3]].Color));
                }
            }

            Bar result = new Bar(start, final);
            if (faulty.Count != 0)
            {
                if (afterCheck == "")
                {
                    return result;
                }
                SaveBar(aFilename, result, aSeparator, aKey);
                throw new ArgumentException($"While chacking Bar file I found {faulty.Count} corupted segments (and deleted them).\nPlease check your tasks to see which are missing.");
            }

            return result;
        }

        public static void SaveAll(ObservableCollection<Task> aTasks, ObservableCollection<Segment> aSegments, int aImportanceFactor, int aTimeFactor, int aLenOfSepSeg, Bar aBar)
        {
            char separator = '|';
            string key = "L2IMwT16HGjbjbc6W9QUxabHCpJTWqY9qQEqPKx6VhziM21vqorfzVgqHX5nSjojLDI5QXl0bJ2FCmYAFQqErgRKhZwzfUmrFma6t0SNcOkU0N5mW2TlQ4PyqP9r5vJdt67yfe5EesvyvE9tTCd1DoU2w" +
                "j9WhRE597BJPKI13LPLvXMS4WkUpermDKx3hnBZXoVOl92HH9SkADCyoXpkupoY51FikLZ94ow8tS393wKj0S3vZR7y9gzcVp8OVCfBI2WadRlYX499cvE8CJLbSqQci5c5BFVZWdS6mlSQdbwPvrwcdGc83yjCwXND" +
                "PTOQD6FmeS3zMi6H1RvzvEuGSRObqYTiMEu8pxTkevhvZcceXxTQSAd4ykWeZ68gXgKCGe1RyVJR5qfgY5fA5FVkdCX4hWrEuzYaNoivHLi7604K7l7GuMR7ccY4CY9kQghKeoOiM00jEZ7b4hVHIeM7Qy8EsAtwf3W" +
                "TqfEoAOJ84ickfnmK24BBDyOuMcQRQtyzMpEZuum1A88PfhFI4isk4sLiBvCvx7uRgNebnrDATCKg9VhPRgyZnWlTrm1pzMJ8SdBU9ja4EXrwoljD9fnwJmto0Buo35Igr7qvlFgeO6KIS9Nm6cE4VylVwJ8e8u7oMg" +
                "n5toUVlbZhTMYwGYX8qEZhi0GNOIfekLutEw6yi99RGg9pp7LZZjBZSOiq9s7dsNNuJbE2eUNiUing8N6R3clw0m5WSICScz4UymFyNhC1TK8suqfblzfz6wsJNS6RjnyNDCk5VaWyPiFMfXyvqWtujfwXuv0tzv3tgD" +
                "OnzyWK9TjVECDKraO4ftu3KTcv8H0EoV7cEVa7g7t6iil4GJqFafNvBgTPdAx9v39RxiIjb6PruNecjEyitqIcjKtFdoKTyfIBopmAQj8DCwPA5VH8898jjZqyIayTPMKD3KP0sjFikmzWLZGePr4dh82KFnXPgoCbfmTqfbzhETYXnZL1rVV0FzVeyJqigIp9";
            SaveTasks("Tasks-TimeOrganiser.txt", aTasks, separator, key);
            SaveSegments("Segments-TimeOrganiser.txt", aSegments, separator, key);
            SaveSettings("Settings-TimeOrganiser.txt", aImportanceFactor, aTimeFactor, aLenOfSepSeg, separator, key);
            SaveBar("Bar-TimeOrganiser.txt", aBar, separator, key);
        }

        public static LoadingResult LoadAll()
        {
            LoadingResult final = new LoadingResult();
            char separator = '|';
            string key = "L2IMwT16HGjbjbc6W9QUxabHCpJTWqY9qQEqPKx6VhziM21vqorfzVgqHX5nSjojLDI5QXl0bJ2FCmYAFQqErgRKhZwzfUmrFma6t0SNcOkU0N5mW2TlQ4PyqP9r5vJdt67yfe5EesvyvE9tTCd1DoU2w" +
                "j9WhRE597BJPKI13LPLvXMS4WkUpermDKx3hnBZXoVOl92HH9SkADCyoXpkupoY51FikLZ94ow8tS393wKj0S3vZR7y9gzcVp8OVCfBI2WadRlYX499cvE8CJLbSqQci5c5BFVZWdS6mlSQdbwPvrwcdGc83yjCwXND" +
                "PTOQD6FmeS3zMi6H1RvzvEuGSRObqYTiMEu8pxTkevhvZcceXxTQSAd4ykWeZ68gXgKCGe1RyVJR5qfgY5fA5FVkdCX4hWrEuzYaNoivHLi7604K7l7GuMR7ccY4CY9kQghKeoOiM00jEZ7b4hVHIeM7Qy8EsAtwf3W" +
                "TqfEoAOJ84ickfnmK24BBDyOuMcQRQtyzMpEZuum1A88PfhFI4isk4sLiBvCvx7uRgNebnrDATCKg9VhPRgyZnWlTrm1pzMJ8SdBU9ja4EXrwoljD9fnwJmto0Buo35Igr7qvlFgeO6KIS9Nm6cE4VylVwJ8e8u7oMg" +
                "n5toUVlbZhTMYwGYX8qEZhi0GNOIfekLutEw6yi99RGg9pp7LZZjBZSOiq9s7dsNNuJbE2eUNiUing8N6R3clw0m5WSICScz4UymFyNhC1TK8suqfblzfz6wsJNS6RjnyNDCk5VaWyPiFMfXyvqWtujfwXuv0tzv3tgD" +
                "OnzyWK9TjVECDKraO4ftu3KTcv8H0EoV7cEVa7g7t6iil4GJqFafNvBgTPdAx9v39RxiIjb6PruNecjEyitqIcjKtFdoKTyfIBopmAQj8DCwPA5VH8898jjZqyIayTPMKD3KP0sjFikmzWLZGePr4dh82KFnXPgoCbfmTqfbzhETYXnZL1rVV0FzVeyJqigIp9";

            try
            {
                int[] res = LoadSettings("Settings-TimeOrganiser.txt", separator, key);
                final.InsideImportanceFactor = res[0];
                final.InsideTimeFactor = res[1];
                final.InsideLengthOfSepSegment = res[2];
            }
            catch (ArgumentException e) { MessageBox.Show(e.Message); }
            finally
            {
                int[] res = LoadSettings("Settings-TimeOrganiser.txt", separator, key);
                final.InsideImportanceFactor = res[0];
                final.InsideTimeFactor = res[1];
                final.InsideLengthOfSepSegment = res[2];
            }

            try 
            {
                List<Task> tasks = new List<Task>();
                final.InsideTasks = LoadTasks("Tasks-TimeOrganiser.txt", separator, key);
                tasks = final.InsideTasks.ToList();
                tasks.Sort((x, y) => y.ValueToCompare(final.InsideImportanceFactor, final.InsideTimeFactor).CompareTo(x.ValueToCompare(final.InsideImportanceFactor, final.InsideTimeFactor)));
                final.InsideTasks = new ObservableCollection<Task>(tasks);
            }
            catch (ArgumentException e) { MessageBox.Show(e.Message); }
            finally 
            {
                List<Task> tasks = new List<Task>();
                final.InsideTasks = LoadTasks("Tasks-TimeOrganiser.txt", separator, key);
                tasks = final.InsideTasks.ToList();
                tasks.Sort((x, y) => y.ValueToCompare(final.InsideImportanceFactor, final.InsideTimeFactor).CompareTo(x.ValueToCompare(final.InsideImportanceFactor, final.InsideTimeFactor)));
                final.InsideTasks = new ObservableCollection<Task>(tasks);
            }

            try { final.InsideSegments = LoadSegments("Segments-TimeOrganiser.txt", separator, key); }
            catch (ArgumentException e) { MessageBox.Show(e.Message); }
            finally { final.InsideSegments = LoadSegments("Segments-TimeOrganiser.txt", separator, key); }

            try { final.InsideBar = LoadBar("Bar-TimeOrganiser.txt", separator, key); }
            catch (ArgumentException e) { MessageBox.Show(e.Message); }
            finally { final.InsideBar = LoadBar("Bar-TimeOrganiser.txt", separator, key); }

            return final;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveAll(Tasks, Segments, ImportanceFactor, TimeFactor, LengthOfSepSegment, CurentBar);
            base.OnClosing(e);
        }
    }
}

/*
 To do:
fix dispaying of error texts in Settings window
add windows,
saving
 */