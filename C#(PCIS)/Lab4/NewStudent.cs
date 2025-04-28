using System;
using System.Text;

    class NewStudent : Person, IDateAndCopy
    {
        private Education _education;
        private int _groupNum;
        private System.Collections.Generic.List<Test> _testList;
        private System.Collections.Generic.List<Exam> _examList;

        public NewStudent(string firstName, string secondName, DateTime birthDate, Education education, int groupNum, System.Collections.Generic.List<Test> testList, System.Collections.Generic.List<Exam> examList) : base(firstName, secondName, birthDate)
        {
            Person = new Person(firstName, secondName, birthDate);
            _education = education;
            _groupNum = groupNum;
            _testList = testList;
            _examList = examList;
        }

        public NewStudent() : this(new Person(),Education.Bachelor, 0, [], [])
        {
        }

        public NewStudent(Person studentPerson,Education formOfEducation,
                int groupNumber, System.Collections.Generic.List<Test> testList, System.Collections.Generic.List<Exam> examList) : base()
        {
            Person = studentPerson;
            Education = formOfEducation;
            GroupNum = groupNumber;
            TestList = testList;
            ExamList = examList;
        }

        public Person Person { get; init; }

        public Education Education
        {
            get { return _education; }
            init { _education = value; }
        }
        public int GroupNum
        {
            get { return _groupNum; }
            set
            {
                _groupNum = value;
            }
        }
        public System.Collections.Generic.List<Test> TestList
        {
            get { return _testList; }
            init { _testList = value; }
        }
        public System.Collections.Generic.List<Exam> ExamList
        {
            get { return _examList; }
            set { _examList = value; }
        }

        public double MidCount
        {
            get
            {
                double res = 0.0;
                if(ExamList==null||ExamList.Count == 0)return 0;
                foreach (Exam examGrade in ExamList)
                {
                    res += examGrade.Mark;
                }
                return res / ExamList.Count;
            }
        }

        public void AddExams(System.Collections.Generic.List<Exam> newExamList)
        {
            if (ExamList is null)
            {
                ExamList = newExamList;
                return;
            }
            ExamList.AddRange(newExamList);

        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder(Education.ToString() + ' ' + GroupNum.ToString() + ' ');
            for (int i = 0; i < ExamList.Count; ++i)
            {
                res.Append(ExamList[i].ToString());
            }

            res.Append(' ');

            for (int i = 0; i < TestList.Count; ++i)
            {
                res.Append(TestList[i].ToString());
            }
            return res.ToString();
        }

        public string ToShortString()
        {
            return Education.ToString() + ' ' + GroupNum.ToString() + ' ' + MidCount.ToString() + ' ';
        }

        public virtual object DeepCopy()
        {
            System.Collections.Generic.List<Exam> copiedExamList = [];
            System.Collections.Generic.List<Test> copiedTestList = [];
            Person? copiedStudentPerson = this.Person?.DeepCopy() as Person;

            for (int i = 0; i < ExamList.Count; ++i)
            {
                if (ExamList[i] is Exam exam)
                {
                    copiedExamList.Add((Exam)exam.DeepCopy());
                }
            }

            for (int i = 0; i < TestList.Count; ++i)
            {
                if (TestList[i] is Test test)
                {
                    copiedTestList.Add((Test)test.DeepCopy());
                }
            }

            NewStudent copied = new NewStudent(copiedStudentPerson,
                                                Education,
                                                GroupNum,
                                                testList: copiedTestList,
                                                examList: copiedExamList);
            return copied;
        }
    }
