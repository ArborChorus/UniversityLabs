using System;

    class Test
    {
        public string SubjectName { get; set; }
        public bool IsPassed { get; set; }

        public Test(string subjectName, bool isPassed)
        {
            SubjectName = subjectName;
            IsPassed = isPassed;
        }

        public Test() : this("Opening", false) { }

        public override string ToString()
        {
            return SubjectName + ' ' + IsPassed.ToString();
        }

        public virtual object DeepCopy()
        {
            Test copied = new Test(SubjectName, IsPassed);
            return copied;
        }
    }
