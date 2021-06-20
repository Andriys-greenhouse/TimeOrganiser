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
        public string AboutDescr { get; set; } = 
            "This application is designed to help you to schedule your time and to track your tasks. " +
            "Time planning is done by breaking your time to segments (like: breakfast, school, prayer time,...) which user arranges to order he likes." +
            "Tasks are evaluated and ordered by criteria given by the user.";
        public string TaskDescr { get; set; } = "Task have title, description, importance and deadline. Their list is going to be displayed in main window in order determined by user in settings.";
        public string BarDescr { get; set; } = "Bar have start (hour and minute of the day) and list of segments. New one can be created by user in New Bar window (reachable from main window).";
        public string SegmentDescr { get; set; } = "Segment is used to record a block of aktivity (like breakfast, school, excercise or prayer time). It have Title, description, duration (in minutes) and background color.";
        public string SettingsDescr { get; set; } = "In settings window user can set integers (from 0 to 99) by which app will multiply importance and number of days let to deadline of a task in order to get its \"score\" (to determine order of tasks). Also user can set length of separating segment (segment automatically put between user specified segments to giving time to prepare for next segment) to 0 (for non) or to integer from 10 to 30.";
        public string MainWindowDescr { get; set; } = "Main window packs most of the functions and the rest servs onely as pleces, where user inputs data.\nAt the top, user can see details about segments in his Bar(if user double clicks on any of them he can edit it in window which will appear) and under it he can see segments of Bar with real length in comparison to one another.\nOn the left, list of tasks can be seen displaying title, importance, deadline and \"completness\" checkbook.In the middle, details about selected task are displayed. If user double clicks on some of tasks, window for editation will be opened.\nOther controll buttons are on the right.";
        public string NewBarWindowDescr { get; set; } = "On the left is displayed list of existing segments, on the right, list of segments belonging to the new Bar is displayed.\nDouble click on the left, will open window for editation of selected segment.Between these lists buttons are displayed:\n  * -> - add selected segment from left list to the right one\n  * <- - remove selected segment from the left list\n  * Add - opens window for creation of new segment(added to the left list)\n * Del - deletes selected segment from the left list\nOn bottom right, user can input starting hour and minute of created Bar.";
        public string SavingMechanismDescr { get; set; } = "App is saving created tasks and segments, also settings and last Bar is saved.";

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
/*
 TASK
Task have title, description, importance and deadline. Their list is going to be displayed in main window in order determined by user in settings.

BAR
Bar have start (hour and minute of the day) and list of segments. New one can be created by user in New Bar window (reachable from main window).

SEGMENT
Segment is used to record a block of aktivity (like breakfast, school, excercise or prayer time). It have Title, description, duration (in minutes) and background color.

SETTINGS
In settings window user can set integers (from 0 to 99) by which app will multiply importance and number of days let to deadline of a task in order to get its "score" (to determine order of tasks). Also user can set length of separating segment (segment automatically put between user specified segments to giving time to prepare for next segment) to 0 (for non) or to integer from 10 to 30.

MAIN WINDOW
Main window packs most of the functions and the rest servs onely as pleces, where user inputs data.\n
At the top, user can see details about segments in his Bar (if user double clicks on any of them he can edit it in window which will appear) and under it he can see segments of Bar with real length in comparison to one another.\n
On the left, list of tasks can be seen displaying title, importance, deadline and "completness" checkbook. In the middle, details about selected task are displayed. If user double clicks on some of tasks, window for editation will be opened.\n
Other controll buttons are on the right.\n

TASK WINDOW


NEW BAR WINDOW
On the left is displayed list of existing segments, on the right, list of segments belonging to the new Bar is displayed.\n
Double click on the left, will open window for editation of selected segment. Between these lists buttons are displayed:\n
  * -> - add selected segment from left list to the right one\n
  * <- - remove selected segment from the left list\n
  * Add - opens window for creation of new segment (added to the left list)\n
  * Del - deletes selected segment from the left list\n
On bottom right, user can input starting hour and minute of created Bar.

SEGMENT WINDOW


SAVING MECHANISM
App is saving created tasks and segments, also settings and last Bar is saved. 
 */