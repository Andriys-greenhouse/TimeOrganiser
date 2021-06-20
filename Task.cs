using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeOrganiser
{
    public class Task
    {
        public string Title { get; set; } //2-20 char
        public string Description { get; set; } //0-200 char
        public int Importance { get; set; } //1-10
        public DateTime Deadline { get; set; } //from now on
        public string DeadlineFormated 
        { 
            get 
            {
                if (Deadline.Hour != 0) { return Deadline.ToString("yyyy/MM/dd : HH"); }
                else { return Deadline.ToString("yyyy/MM/dd"); }
            } 
        }
        public bool Solved { get; set; } = false;

        public Task(string aTitle, string aDescription, int aImportance, DateTime aDeadline)
        {
            Title = aTitle;
            Description = aDescription;
            Importance = aImportance;
            Deadline = aDeadline;
        }

        public int ValueToCompare(int aImportanceFactor, int aTimeFactor)
        {
            return Importance * aImportanceFactor - (Deadline - DateTime.Now).Days * aTimeFactor; 
        }
    }
}
