using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class TestCollections
    {
        public System.Collections.Generic.List<Person> ListOfPerson;
        public System.Collections.Generic.List<string> ListOfString;
        public System.Collections.Generic.Dictionary<Person, NewStudent> DictOfPersAndNStud;
        public System.Collections.Generic.Dictionary<string, NewStudent> DictKeyOfPersAndNStud;

        public static NewStudent Create(int index)
        {
            var person = new Person(
                firstName: $"FName {index}",
                secondName: $"LName {index}",
                birthDate: DateTime.MinValue.AddYears(20).AddDays(index).AddHours(11)
            );
            int groupNum = 101 + (index % 599);

            Education eduForm = (Education)(index % Enum.GetValues(typeof(Education)).Length);

            var tests = new List<Test>();
            var exams = new List<Exam>();


            exams.Add(new Exam { Name = "Math", Mark = 60 + (index % 41), Date = DateTime.MinValue.AddDays(10 + index % 5).AddHours(11) });
            exams.Add(new Exam { Name = "Physics", Mark = 55 + (index % 46), Date = DateTime.MinValue.AddDays(5 + index % 3).AddHours(11) });

            tests.Add(new Test { SubjectName = "History", IsPassed = (index % 2 == 0) });

            return new NewStudent(
                studentPerson: person,
                formOfEducation: eduForm,
                groupNumber: groupNum,
                testList: tests,
                examList: exams
            );
        }
        public void TestCollection(int numOfElements)
        {
            if (numOfElements < 0) throw new ArgumentOutOfRangeException(nameof(numOfElements));

            ListOfPerson = new List<Person>(numOfElements);
            ListOfString = new List<string>(numOfElements);
            DictOfPersAndNStud = new Dictionary<Person, NewStudent>(numOfElements);
            DictKeyOfPersAndNStud = new Dictionary<string, NewStudent>(numOfElements);

            for (int i = 0; i < numOfElements; ++i)
            {
                NewStudent student = Create(i);

                ListOfPerson.Add(student.Person);
                ListOfString.Add($"Value String {i}");
                DictOfPersAndNStud.Add(student.Person, student);
                DictKeyOfPersAndNStud.Add($"KeyString_{i}", student);
            }
        }
        public void FindListPerson(Person findPerson)
        {
            Console.WriteLine(ListOfPerson.Contains(findPerson) ? 1 : -1);
            return;
        }
        public void FindListString(string findString)
        {
            Console.WriteLine(ListOfString.Contains(findString) ? 1 : -1);
            return;
        }
        public void FindDictPerson(Person findKeyPerson)
        {
            Console.WriteLine(DictOfPersAndNStud.ContainsKey(findKeyPerson) ? 1 : -1);
            return;
        }
        public void FindDictString(NewStudent findValStudent)
        {
            Console.WriteLine(DictKeyOfPersAndNStud.ContainsValue(findValStudent) ? 1 : -1);
            return;
        }
    }

