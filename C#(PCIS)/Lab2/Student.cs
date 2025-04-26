using System;

    class Student : Person, IDateAndCopy
    {
        private Person _person;
        private Education _education;
        private int _groupNum;
        private Exam[] _examList;

        public Person Person
        {
            get { return _person; }
            init { _person = value; }
        }

        public Education Education
        {
            get { return _education; }
            init { _education = value; }
        }

        public int GroupNum
        {
            get { return _groupNum; }
            set { _groupNum = value; }
        }

        public Exam[] ExamList
        {
            get { return _examList; }
            set { _examList = value; }
        }

        public Student(Person person, Education education, int groupNum, Exam[] examList)
        {
            Person = person;
            Education = education;
            GroupNum = groupNum;
            ExamList = examList;
        }

        public Student() : this(new Person(), Education.Bachelor, 0, new Exam[0]) { }

        public double? MidCount
        {
            get
            {
                if (ExamList == null || ExamList.Length == 0) return 0;
                double sum = 0;
                foreach (var exam in ExamList)
                {
                    sum += exam.GetMark();
                }
                return sum / ExamList.Length;
            }
        }

        public bool this[Education index]
        {
            get { return Education == index; }
        }

        public void AddExams(params Exam[] tempExams)
        {
            if (tempExams == null) return;
            var newExams = new Exam[ExamList.Length + tempExams.Length];
            ExamList.CopyTo(newExams, 0);
            tempExams.CopyTo(newExams, ExamList.Length);
            ExamList = newExams;
        }

        public override string ToString()
        {
            string exams = string.Join(", ", Array.ConvertAll(ExamList, exam => exam.ToString()));
            return $"{Person}, Education: {Education}, Group: {GroupNum}, Exams: {ExamList}";
        }

        public virtual string ToShortString()
        {
            return $"{Person.ToShortString()}, Education: {Education}, Group: {GroupNum}, Average Mark: {MidCount}";
        }

        public virtual object? DeepCopy()

        {
            Person copiedPerson = (Person)this.Person.DeepCopy();
            Exam[] copiedExamList = new Exam[this.ExamList.Length];
            for (int i = 0; i < this.ExamList.Length; i++)
            {
                copiedExamList[i] = (Exam)this.ExamList[i].DeepCopy();
            }
            return new Student(copiedPerson, this.Education, this.GroupNum, copiedExamList);
        }
    }
