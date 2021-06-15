using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeOrganiser
{
    public class Segment
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public double ShowLength { get { return Duration/4.4; } }
        public SolidColorBrush BackgroundColor { get; set; }
        public Segment(string aName, string aDescription, int aLenght, Color aColor)
        {
            Title = aName;
            Description = aDescription;
            Duration = aLenght;
            BackgroundColor = new SolidColorBrush(aColor);
        }
    }
}
