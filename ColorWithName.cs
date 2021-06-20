using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace TimeOrganiser
{
    public class ColorWithName
    {
        public static ObservableCollection<ColorWithName> MyColors { get; set; } = new ObservableCollection<ColorWithName>();
        public string Word { get; set; }
        public SolidColorBrush Color { get; set; }
        public ColorWithName(string aWord, SolidColorBrush aColor)
        {
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
}
