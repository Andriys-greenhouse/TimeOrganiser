using System;
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
        public Segment(string aName, string aDescription, int aLenght)
        {
            Name = aName;
            Description = aDescription;
            Lenght = aLenght;
        }
    }
}
