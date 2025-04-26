using System;


    class Exam : IDateAndCopy
    {
        private string _name;
        private int _mark;
        private DateTime _startDate;

        public string Name
        {
            get { return _name; }
            init { _name = value; }
        }

        public int Mark
        {
            get { return _mark; }
            init { _mark = value; }
        }

        public DateTime Date
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public Exam(string subname, int mark, DateTime examDate) => (Name, Mark, Date) = (subname, mark, examDate);

        public Exam() : this("laziness", 55, DateTime.Parse("2000-01-01")) { }

        public int GetMark()
        {
            return Mark;
        }

        public override string ToString()
        {
            return $"{Name} {Mark} {Date.ToShortDateString()}";
        }

        public virtual object? DeepCopy()
        {
            return new Exam(this.Name, this.Mark, this.Date);
        }
    }