using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class ComperStudentMidCount:IComparer<NewStudent>
    {
        public int Compare(NewStudent? first, NewStudent? second)
        {
        if (first is null && second is null) return 0;
        if (first is null) return -1;
        if (second is null) return 1;

        return first.MidCount.CompareTo(second.MidCount);
    }
}

