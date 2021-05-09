using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeOrganiser
{
    class Segment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Lenght { get; set; }
        public double ShowLength { get { return Lenght/4.4; } }
        public SolidColorBrush BackgroundColor { get; set; }
        public Segment(string aName, string aDescription, int aLenght, Color aColor)
        {
            Name = aName;
            Description = aDescription;
            Lenght = aLenght;
            BackgroundColor = new SolidColorBrush(aColor);
        }
    }
}
