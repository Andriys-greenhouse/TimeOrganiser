using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TimeOrganiser
{
    public class LoadingResult
    {
        public Bar InsideBar { get; set; } = new Bar(new DateTime(2021, 5, 9, 8, 0, 0), new ObservableCollection<Segment>());
        public ObservableCollection<Task> InsideTasks { get; set; } = new ObservableCollection<Task>();
        public ObservableCollection<Segment> InsideSegments { get; set; } = new ObservableCollection<Segment>();
        public int InsideTimeFactor { get; set; } = 1;
        public int InsideImportanceFactor { get; set; } = 3;
        public int InsideLengthOfSepSegment { get; set; } = 15;

        public LoadingResult(ObservableCollection<Task> aTasks, ObservableCollection<Segment> aSegments,
            Bar aBar, int aImportanceFactor, int aTimeFactor, int aLenOfSepSeg)
        {
            InsideTasks = aTasks;
            InsideSegments = aSegments;
            InsideBar = aBar;
            InsideImportanceFactor = aImportanceFactor;
            InsideTimeFactor = aTimeFactor;
            InsideLengthOfSepSegment = aLenOfSepSeg;
        }

        public LoadingResult() { }
    }
}
