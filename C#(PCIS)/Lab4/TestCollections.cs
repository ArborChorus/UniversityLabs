using System;
using System.Collections.Generic;
using System.Collections.Immutable; // Додано using
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestCollections
{
    // Standard Collections
    public List<Person> ListOfPerson;
    public List<string> ListOfString;
    public Dictionary<Person, NewStudent> DictOfPersAndNStud;
    public Dictionary<string, NewStudent> DictKeyStringAndNStud; // Перейменовано для ясності (пошук за ключем)

    // Immutable Collections
    public ImmutableList<Person> ImmutableListOfPerson;
    public ImmutableList<string> ImmutableListOfString;
    public ImmutableDictionary<Person, NewStudent> ImmutableDictOfPersAndNStud;
    public ImmutableDictionary<string, NewStudent> ImmutableDictKeyStringAndNStud;

    // Sorted Collections
    public SortedList<Person, NewStudent> SortedListOfPersAndNStud;
    public SortedList<string, NewStudent> SortedListKeyStringAndNStud;
    public SortedDictionary<Person, NewStudent> SortedDictOfPersAndNStud;
    public SortedDictionary<string, NewStudent> SortedDictKeyStringAndNStud;


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

        // Standard Collections Initialization
        ListOfPerson = new List<Person>(numOfElements);
        ListOfString = new List<string>(numOfElements);
        DictOfPersAndNStud = new Dictionary<Person, NewStudent>(numOfElements);
        DictKeyStringAndNStud = new Dictionary<string, NewStudent>(numOfElements);

        // Immutable Collections Builders
        var immutableListPersonBuilder = ImmutableList.CreateBuilder<Person>();
        var immutableListStringBuilder = ImmutableList.CreateBuilder<string>();
        var immutableDictPersonBuilder = ImmutableDictionary.CreateBuilder<Person, NewStudent>();
        var immutableDictStringBuilder = ImmutableDictionary.CreateBuilder<string, NewStudent>();

        // Sorted Collections Initialization
        SortedListOfPersAndNStud = new SortedList<Person, NewStudent>(); // SortedList використовує Person.CompareTo (за SecondName)
        SortedListKeyStringAndNStud = new SortedList<string, NewStudent>();
        SortedDictOfPersAndNStud = new SortedDictionary<Person, NewStudent>(); // SortedDictionary використовує Person.CompareTo (за SecondName)
        SortedDictKeyStringAndNStud = new SortedDictionary<string, NewStudent>();


        for (int i = 0; i < numOfElements; ++i)
        {
            NewStudent student = Create(i);
            string keyString = $"KeyString_{i}";
            string valueString = $"Value String {i}";
            Person personKey = student.Person;

            // Populate Standard Collections
            ListOfPerson.Add(personKey);
            ListOfString.Add(valueString);
            DictOfPersAndNStud.Add(personKey, student);
            DictKeyStringAndNStud.Add(keyString, student);

            // Populate Immutable Collections (via Builders)
            immutableListPersonBuilder.Add(personKey);
            immutableListStringBuilder.Add(valueString);
            immutableDictPersonBuilder.Add(personKey, student);
            immutableDictStringBuilder.Add(keyString, student);

            // Populate Sorted Collections
            // Перевірка на унікальність ключів для SortedList/SortedDictionary (рідкісний випадок з однаковими SecondName)
            if (!SortedListOfPersAndNStud.ContainsKey(personKey))
                SortedListOfPersAndNStud.Add(personKey, student);
            if (!SortedListKeyStringAndNStud.ContainsKey(keyString))
                SortedListKeyStringAndNStud.Add(keyString, student);
            if (!SortedDictOfPersAndNStud.ContainsKey(personKey))
                SortedDictOfPersAndNStud.Add(personKey, student);
            if (!SortedDictKeyStringAndNStud.ContainsKey(keyString))
                SortedDictKeyStringAndNStud.Add(keyString, student);
        }

        // Build Immutable Collections
        ImmutableListOfPerson = immutableListPersonBuilder.ToImmutable();
        ImmutableListOfString = immutableListStringBuilder.ToImmutable();
        ImmutableDictOfPersAndNStud = immutableDictPersonBuilder.ToImmutable();
        ImmutableDictKeyStringAndNStud = immutableDictStringBuilder.ToImmutable();
    }

    // --- Find Methods for Standard Collections ---
    public void FindListPerson(Person findPerson)
    {
        //Console.WriteLine(ListOfPerson.Contains(findPerson) ? 1 : -1); // Просто виконуємо пошук для виміру часу
        ListOfPerson.Contains(findPerson);
    }
    public void FindListString(string findString)
    {
        //Console.WriteLine(ListOfString.Contains(findString) ? 1 : -1);
        ListOfString.Contains(findString);
    }
    public void FindDictPersonKey(Person findKeyPerson) // Пошук за ключем
    {
        //Console.WriteLine(DictOfPersAndNStud.ContainsKey(findKeyPerson) ? 1 : -1);
        DictOfPersAndNStud.ContainsKey(findKeyPerson);
    }
    public void FindDictStringKey(string findKeyString) // Пошук за ключем (змінено з ContainsValue)
    {
        //Console.WriteLine(DictKeyStringAndNStud.ContainsKey(findKeyString) ? 1 : -1);
        DictKeyStringAndNStud.ContainsKey(findKeyString);
    }

    // --- Find Methods for Immutable Collections ---
    public void FindImmutableListPerson(Person findPerson)
    {
        //Console.WriteLine(ImmutableListOfPerson.Contains(findPerson) ? 1 : -1);
        ImmutableListOfPerson.Contains(findPerson);
    }
    public void FindImmutableListString(string findString)
    {
        //Console.WriteLine(ImmutableListOfString.Contains(findString) ? 1 : -1);
        ImmutableListOfString.Contains(findString);
    }
    public void FindImmutableDictPersonKey(Person findKeyPerson)
    {
        //Console.WriteLine(ImmutableDictOfPersAndNStud.ContainsKey(findKeyPerson) ? 1 : -1);
        ImmutableDictOfPersAndNStud.ContainsKey(findKeyPerson);
    }
    public void FindImmutableDictStringKey(string findKeyString)
    {
        //Console.WriteLine(ImmutableDictKeyStringAndNStud.ContainsKey(findKeyString) ? 1 : -1);
        ImmutableDictKeyStringAndNStud.ContainsKey(findKeyString);
    }

    // --- Find Methods for Sorted Collections ---
    public void FindSortedListPersonKey(Person findKeyPerson)
    {
        //Console.WriteLine(SortedListOfPersAndNStud.ContainsKey(findKeyPerson) ? 1 : -1);
        SortedListOfPersAndNStud.ContainsKey(findKeyPerson);
    }
    public void FindSortedListStringKey(string findKeyString)
    {
        //Console.WriteLine(SortedListKeyStringAndNStud.ContainsKey(findKeyString) ? 1 : -1);
        SortedListKeyStringAndNStud.ContainsKey(findKeyString);
    }
    public void FindSortedDictPersonKey(Person findKeyPerson)
    {
        //Console.WriteLine(SortedDictOfPersAndNStud.ContainsKey(findKeyPerson) ? 1 : -1);
        SortedDictOfPersAndNStud.ContainsKey(findKeyPerson);
    }
    public void FindSortedDictStringKey(string findKeyString)
    {
        //Console.WriteLine(SortedDictKeyStringAndNStud.ContainsKey(findKeyString) ? 1 : -1);
        SortedDictKeyStringAndNStud.ContainsKey(findKeyString);
    }
}