using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class StudentCollection
    {
        private System.Collections.Generic.List<NewStudent> _students;
        public System.Collections.Generic.List<NewStudent> Students
        {
            get { return _students; }
            set { _students = value; }
        }

        public void AddDefaults()
        {
            Students.Add(new NewStudent());
            Students.Add(new NewStudent());
        }

        public void AddStudents(params NewStudent[] newStudents)
        {
            if (newStudents is null) return;
            if (Students is null)
            {
                Students = [.. newStudents];
                return;
            }
            Students.AddRange(newStudents);
        }

        public override string ToString()
        {
            string res = "";
            foreach (NewStudent item in Students)
            {
                res+= item.ToString() + " ";
            }
            return res;
        }

        public string ToShortString()
        {
            string res = "";
            foreach (NewStudent item in Students)
            {
                res+=item.ToShortString() + " ";
            }
            return res;
        }

        public void SortBySecondName()
        {
            Students.Sort((s1, s2) => (s1.Person).CompareTo(s2.Person));
        }
        public void SortByBirthdayDate()
        {
            IComparer<NewStudent> ComparerPerson = new NewStudent();
            Students.Sort(comparer: ComparerPerson);
        }
        public void SortByMidCount()
        {
            ComperStudentMidCount ComparerMidCount = new ComperStudentMidCount();
            Students.Sort(ComparerMidCount);
        }
        public double MaxMidCount
        {
            get
            {
                if (Students is null)
                    throw new ArgumentNullException("ListOfStudents is null");

                if (Students.Count == 0)
                    return double.NaN;

                return Students.MaxBy(one => one.MidCount)!.MidCount;
            }
        }

        public List<NewStudent> MastersEducationStudents
        {
            get
            {
                if (Students is null)
                    return new List<NewStudent>();

                return Students.Where(student => student.Education == Education.Master).ToList();
            }
        }

        public List<NewStudent> AverageMidCountGroup(double mark)
        {
            return Students.Where(student => student.MidCount == mark).ToList();
        }

        public void SortBySurname()
        {
            Students.Sort((s1, s2) => (s1.Person).CompareTo(s2.Person));
        }
        public void SortByAverage()
        {
            ComperStudentMidCount ComparerAverage = new ComperStudentMidCount();
            Students.Sort(ComparerAverage);
        }
    }

