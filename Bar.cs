using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeOrganiser
{
    public class Bar
    {
        public DateTime Begening { get; set; }
        public ObservableCollection<Segment> Content { get; set; }
        public int BarSelectedIndex { get; set; } = 0;

        public Bar(DateTime aBegening, ObservableCollection<Segment> aContent)
        {
            Begening = aBegening;
            Content = aContent;
        }
    }
}
