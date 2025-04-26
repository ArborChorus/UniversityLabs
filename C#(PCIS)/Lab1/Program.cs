using System;
using System.Collections.Generic;

class Person
{
    private string? _firstName;
    private string? _secondName;
    private DateTime _birthDate;

    private string? FirstName
    {
        get { return _firstName; }
        init
        {
            _firstName = value;
        }
    }

    private string? SecondName
    {
        get { return _secondName; }
        init
        {
            _secondName = value;
        }
    }

    private DateTime BirthDate
    {
        get { return _birthDate; }
        init
        {
            _birthDate = value;
        }
    }


    public Person(string firstName, string secondName, DateTime birthDate) => (FirstName, SecondName, BirthDate) = (firstName, secondName, birthDate);

    public Person() : this("dummy", "dumbest", DateTime.Parse("2000-01-01")) { }



    public int YearChange
    {
        get { return _birthDate.Year; }
        init { _birthDate = new DateTime(value, _birthDate.Month, _birthDate.Day); }
    }

    public override string ToString()
    {
        return $"{FirstName} {SecondName} {BirthDate.ToShortDateString()}";
    }

    public virtual string ToShortString()
    {
        return $"{FirstName} {SecondName}";
    }
}

enum Education
{
    Master,
    Bachelor,
    SecondEducation
}

class Exam
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

    public DateTime StartDate
    {
        get { return _startDate; }
        init { _startDate = value; }
    }

    public Exam(string subname, int mark, DateTime examDate) => (Name, Mark, StartDate) = (subname, mark, examDate);

    public Exam() : this("laziness", 55, DateTime.Parse("2000-01-01")) { }

    public int GetMark()
    {
        return Mark;
    }

    public override string ToString()
    {
        return $"{Name} {Mark} {StartDate.ToShortDateString()}";
    }
}

class Student
{
    private Person _student;
    private Education _education;
    private int _groupNum;
    private Exam[] _examList;

    public Person Person
    {
        get { return _student; }
        init { _student = value; }
    }

    public Education Education
    {
        get { return _education; }
        init { _education = value; }
    }

    public int GroupNum
    {
        get { return _groupNum; }
        init { _groupNum = value; }
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
}

class HelloWorld
{
    static void Main()
    {
        // Student example
        Person person = new Person("Jon", "Snow", new DateTime(1990, 1, 1));
        Student student = new Student(person, Education.Bachelor, 101, new Exam[0]);
        Exam[] exams = { new Exam("Math", 90, new DateTime(2023, 10, 1)), new Exam("Physics", 85, new DateTime(2023, 10, 2)) };
        student.AddExams(exams);
        Console.WriteLine(student.ToShortString());
        Console.WriteLine($"Is Master: {student[Education.Master]}");
        Console.WriteLine($"Is Bachelor: {student[Education.Bachelor]}");
        Console.WriteLine($"Is SecondEducation: {student[Education.SecondEducation]}");
        Console.WriteLine(student.ToString());
        Console.WriteLine();

        Console.WriteLine("Enter nRwos:");
        int nRows = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter lRwos:");
        int lRows = Convert.ToInt32(Console.ReadLine());

        int totalElements = nRows * lRows;

        // Single-dimensional array
        Student[] singleDimArray = new Student[totalElements];
        for (int i = 0; i < singleDimArray.Length; i++)
        {
            singleDimArray[i] = new Student();
        }

        // Rectangular array
        Student[,] rectArray = new Student[nRows, lRows];
        for (int i = 0; i < nRows; i++)
        {
            for (int j = 0; j < lRows; j++)
            {
                rectArray[i, j] = new Student();
            }
        }

        // Jagged array
        int elementsLeft = totalElements;
        int nRowsForJagged = 0;
        while (elementsLeft > 0)
        {
            nRowsForJagged++;
            elementsLeft -= nRowsForJagged;
        }

        int jaggedElementsLeft = totalElements;
        Student[][] jaggedArray = new Student[nRowsForJagged][];
        int currentColumnSize = 1;

        for (int i = 0; i < nRowsForJagged; ++i)
        {
            currentColumnSize = i + 1;

            if (jaggedElementsLeft - i < 0) currentColumnSize = jaggedElementsLeft;
            jaggedElementsLeft -= currentColumnSize;

            jaggedArray[i] = new Student[currentColumnSize];

            for (int j = 0; j < currentColumnSize; ++j)
            {
                jaggedArray[i][j] = new Student();
            }
        }

        //fill student arrays with exams

        for (int i = 0; i < singleDimArray.Length; i++)
        {
            singleDimArray[i].AddExams(new Exam());
        }

        for (int i = 0; i < nRows; i++)
        {
            for (int j = 0; j < lRows; j++)
            {
                rectArray[i, j].AddExams(new Exam());
            }
        }

        for (int i = 0; i < nRowsForJagged; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                jaggedArray[i][j].AddExams(new Exam());
            }
        }


        int startTime, endTime;

        // check ticks for Single-dimensional
        startTime = Environment.TickCount;
        for (int i = 0; i < singleDimArray.Length; i++)
        {
            double? temp = singleDimArray[i].MidCount;
        }
        endTime = Environment.TickCount;
        Console.WriteLine($"Single-dimensional array ({totalElements} elements): {endTime - startTime} ms");

        // check ticks for Rectangular
        startTime = Environment.TickCount;
        for (int i = 0; i < nRows; i++)
        {
            for (int j = 0; j < lRows; j++)
            {
                double? temp = rectArray[i, j].MidCount;
            }
        }
        endTime = Environment.TickCount;
        Console.WriteLine($"Rectangular array ({totalElements} elements): {endTime - startTime} ms");

        // check ticks for jagged
        startTime = Environment.TickCount;
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                double? temp = jaggedArray[i][j].MidCount;
            }
        }
        endTime = Environment.TickCount;
        Console.WriteLine($"Jagged array ({totalElements} elements): {endTime - startTime} ms");


    }
}


