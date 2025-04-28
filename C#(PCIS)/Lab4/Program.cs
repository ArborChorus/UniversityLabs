using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable; // Додано using
using System.Xml.Linq;


    interface IDateAndCopy
{
    object DeepCopy();
    DateTime Date { get; set; }
}

enum Education
{
    Master,
    Bachelor,
    SecondEducation
}

public class HelloWorld
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Підтримка української мови в консолі

        // --- Частина з колекцією студентів (залишено без змін) ---
        Console.WriteLine("========== ІНІЦІАЛІЗАЦІЯ КОЛЕКЦІЇ СТУДЕНТІВ ==========\n");

        StudentCollection studentCollection = new StudentCollection();

        NewStudent andrii = TestCollections.Create(5);
        NewStudent vasyl = TestCollections.Create(10);
        studentCollection.AddStudents([new NewStudent(), andrii, vasyl]);

        Console.WriteLine("Початковий список студентів:");
        Console.WriteLine(studentCollection.ToString());

        Console.WriteLine("\n========== СОРТУВАННЯ ==========\n");

        studentCollection.SortByAverage();
        Console.WriteLine("Після сортування за середнім балом:");
        Console.WriteLine(studentCollection.ToString());

        studentCollection.SortByBirthdayDate();
        Console.WriteLine("Після сортування за датою народження:");
        Console.WriteLine(studentCollection.ToString());

        studentCollection.SortBySurname();
        Console.WriteLine("Після сортування за прізвищем:");
        Console.WriteLine(studentCollection.ToString());

        Console.WriteLine("\n========== АНАЛІЗ КОЛЕКЦІЇ ==========\n");

        Console.WriteLine($"Максимальний середній бал: {studentCollection.MaxMidCount}\n");

        Console.WriteLine("Студенти з освітнім рівнем 'Магістр':");
        foreach (var student in studentCollection.MastersEducationStudents)
        {
            Console.WriteLine(student);
        }

        Console.WriteLine("\nСтуденти із середнім балом більше 62.5:");
        foreach (var student in studentCollection.AverageMidCountGroup(62.5))
        {
            Console.WriteLine(student);
        }

        // --- Частина з тестуванням швидкості колекцій (модифіковано) ---
        Console.WriteLine("\n========== ТЕСТУВАННЯ ШВИДКОСТІ ПОШУКУ У КОЛЕКЦІЯХ ==========\n");

        int studentsInCollection = 10000; // Зменшено для швидшого тестування, можна збільшити
        Console.WriteLine($"Кількість елементів у тестових колекціях: {studentsInCollection}\n");


        NewStudent firstStudData = TestCollections.Create(0);
        NewStudent halfStudData = TestCollections.Create(studentsInCollection / 2);
        NewStudent lastStudData = TestCollections.Create(studentsInCollection - 1);
        NewStudent theForgottenStudData = new NewStudent(); // Неіснуючий

        Person firstPersonKey = firstStudData.Person;
        Person halfPersonKey = halfStudData.Person;
        Person lastPersonKey = lastStudData.Person;
        Person forgottenPersonKey = theForgottenStudData.Person; // Неіснуючий ключ Person

        string firstStringKey = "KeyString_0";
        string halfStringKey = $"KeyString_{studentsInCollection / 2}";
        string lastStringKey = $"KeyString_{studentsInCollection - 1}";
        string forgottenStringKey = "KeyString_NotFound"; // Неіснуючий ключ string

        string firstStringValue = "Value String 0";
        string halfStringValue = $"Value String {studentsInCollection / 2}";
        string lastStringValue = $"Value String {studentsInCollection - 1}";
        string forgottenStringValue = "Value String Nothing"; // Неіснуюче значення string


        TestCollections testCollection = new TestCollections();
        Console.WriteLine("Створення та заповнення тестових колекцій...");
        var watchInit = System.Diagnostics.Stopwatch.StartNew();
        testCollection.TestCollection(studentsInCollection);
        watchInit.Stop();
        Console.WriteLine($"Час заповнення колекцій: {watchInit.Elapsed}\n");


        var watch = new System.Diagnostics.Stopwatch();

        // ---------------------- List<Person> (Standard) ----------------------
        Console.WriteLine(">>> ПОШУК У List<Person> (Standard):");
        TestSearch(() => testCollection.FindListPerson(firstPersonKey), "перший елемент", watch);
        TestSearch(() => testCollection.FindListPerson(halfPersonKey), "центральний елемент", watch);
        TestSearch(() => testCollection.FindListPerson(lastPersonKey), "останній елемент", watch);
        TestSearch(() => testCollection.FindListPerson(forgottenPersonKey), "неіснуючий елемент", watch);
        Console.WriteLine();

        // ---------------------- List<string> (Standard) ----------------------
        Console.WriteLine(">>> ПОШУК У List<string> (Standard):");
        TestSearch(() => testCollection.FindListString(firstStringValue), "перший елемент", watch);
        TestSearch(() => testCollection.FindListString(halfStringValue), "центральний елемент", watch);
        TestSearch(() => testCollection.FindListString(lastStringValue), "останній елемент", watch);
        TestSearch(() => testCollection.FindListString(forgottenStringValue), "неіснуючий елемент", watch);
        Console.WriteLine();

        // ---------------------- Dictionary<Person, NewStudent> (Standard) [Key Search] ----------------------
        Console.WriteLine(">>> ПОШУК У Dictionary<Person, NewStudent> (Standard, за ключем):");
        TestSearch(() => testCollection.FindDictPersonKey(firstPersonKey), "перший ключ", watch);
        TestSearch(() => testCollection.FindDictPersonKey(halfPersonKey), "центральний ключ", watch);
        TestSearch(() => testCollection.FindDictPersonKey(lastPersonKey), "останній ключ", watch);
        TestSearch(() => testCollection.FindDictPersonKey(forgottenPersonKey), "неіснуючий ключ", watch);
        Console.WriteLine();

        // ---------------------- Dictionary<string, NewStudent> (Standard) [Key Search] ----------------------
        Console.WriteLine(">>> ПОШУК У Dictionary<string, NewStudent> (Standard, за ключем):");
        TestSearch(() => testCollection.FindDictStringKey(firstStringKey), "перший ключ", watch);
        TestSearch(() => testCollection.FindDictStringKey(halfStringKey), "центральний ключ", watch);
        TestSearch(() => testCollection.FindDictStringKey(lastStringKey), "останній ключ", watch);
        TestSearch(() => testCollection.FindDictStringKey(forgottenStringKey), "неіснуючий ключ", watch);
        Console.WriteLine();


        // ---------------------- ImmutableList<Person> ----------------------
        Console.WriteLine(">>> ПОШУК У ImmutableList<Person>:");
        TestSearch(() => testCollection.FindImmutableListPerson(firstPersonKey), "перший елемент", watch);
        TestSearch(() => testCollection.FindImmutableListPerson(halfPersonKey), "центральний елемент", watch);
        TestSearch(() => testCollection.FindImmutableListPerson(lastPersonKey), "останній елемент", watch);
        TestSearch(() => testCollection.FindImmutableListPerson(forgottenPersonKey), "неіснуючий елемент", watch);
        Console.WriteLine();

        // ---------------------- ImmutableList<string> ----------------------
        Console.WriteLine(">>> ПОШУК У ImmutableList<string>:");
        TestSearch(() => testCollection.FindImmutableListString(firstStringValue), "перший елемент", watch);
        TestSearch(() => testCollection.FindImmutableListString(halfStringValue), "центральний елемент", watch);
        TestSearch(() => testCollection.FindImmutableListString(lastStringValue), "останній елемент", watch);
        TestSearch(() => testCollection.FindImmutableListString(forgottenStringValue), "неіснуючий елемент", watch);
        Console.WriteLine();

        // ---------------------- ImmutableDictionary<Person, NewStudent> [Key Search] ----------------------
        Console.WriteLine(">>> ПОШУК У ImmutableDictionary<Person, NewStudent> (за ключем):");
        TestSearch(() => testCollection.FindImmutableDictPersonKey(firstPersonKey), "перший ключ", watch);
        TestSearch(() => testCollection.FindImmutableDictPersonKey(halfPersonKey), "центральний ключ", watch);
        TestSearch(() => testCollection.FindImmutableDictPersonKey(lastPersonKey), "останній ключ", watch);
        TestSearch(() => testCollection.FindImmutableDictPersonKey(forgottenPersonKey), "неіснуючий ключ", watch);
        Console.WriteLine();

        // ---------------------- ImmutableDictionary<string, NewStudent> [Key Search] ----------------------
        Console.WriteLine(">>> ПОШУК У ImmutableDictionary<string, NewStudent> (за ключем):");
        TestSearch(() => testCollection.FindImmutableDictStringKey(firstStringKey), "перший ключ", watch);
        TestSearch(() => testCollection.FindImmutableDictStringKey(halfStringKey), "центральний ключ", watch);
        TestSearch(() => testCollection.FindImmutableDictStringKey(lastStringKey), "останній ключ", watch);
        TestSearch(() => testCollection.FindImmutableDictStringKey(forgottenStringKey), "неіснуючий ключ", watch);
        Console.WriteLine();

        // ---------------------- SortedList<Person, NewStudent> [Key Search] ----------------------
        Console.WriteLine(">>> ПОШУК У SortedList<Person, NewStudent> (за ключем):");
        TestSearch(() => testCollection.FindSortedListPersonKey(firstPersonKey), "перший ключ", watch);
        TestSearch(() => testCollection.FindSortedListPersonKey(halfPersonKey), "центральний ключ", watch);
        TestSearch(() => testCollection.FindSortedListPersonKey(lastPersonKey), "останній ключ", watch);
        TestSearch(() => testCollection.FindSortedListPersonKey(forgottenPersonKey), "неіснуючий ключ", watch);
        Console.WriteLine();

        // ---------------------- SortedList<string, NewStudent> [Key Search] ----------------------
        Console.WriteLine(">>> ПОШУК У SortedList<string, NewStudent> (за ключем):");
        TestSearch(() => testCollection.FindSortedListStringKey(firstStringKey), "перший ключ", watch);
        TestSearch(() => testCollection.FindSortedListStringKey(halfStringKey), "центральний ключ", watch);
        TestSearch(() => testCollection.FindSortedListStringKey(lastStringKey), "останній ключ", watch);
        TestSearch(() => testCollection.FindSortedListStringKey(forgottenStringKey), "неіснуючий ключ", watch);
        Console.WriteLine();

        // ---------------------- SortedDictionary<Person, NewStudent> [Key Search] ----------------------
        Console.WriteLine(">>> ПОШУК У SortedDictionary<Person, NewStudent> (за ключем):");
        TestSearch(() => testCollection.FindSortedDictPersonKey(firstPersonKey), "перший ключ", watch);
        TestSearch(() => testCollection.FindSortedDictPersonKey(halfPersonKey), "центральний ключ", watch);
        TestSearch(() => testCollection.FindSortedDictPersonKey(lastPersonKey), "останній ключ", watch);
        TestSearch(() => testCollection.FindSortedDictPersonKey(forgottenPersonKey), "неіснуючий ключ", watch);
        Console.WriteLine();

        // ---------------------- SortedDictionary<string, NewStudent> [Key Search] ----------------------
        Console.WriteLine(">>> ПОШУК У SortedDictionary<string, NewStudent> (за ключем):");
        TestSearch(() => testCollection.FindSortedDictStringKey(firstStringKey), "перший ключ", watch);
        TestSearch(() => testCollection.FindSortedDictStringKey(halfStringKey), "центральний ключ", watch);
        TestSearch(() => testCollection.FindSortedDictStringKey(lastStringKey), "останній ключ", watch);
        TestSearch(() => testCollection.FindSortedDictStringKey(forgottenStringKey), "неіснуючий ключ", watch);
        Console.WriteLine();

    }

    /// <summary>
    /// Метод для вимірювання часу пошуку та виводу результату
    /// </summary>
    static void TestSearch(Action action, string description, System.Diagnostics.Stopwatch watch)
    {
        watch.Restart();
        action.Invoke();
        watch.Stop();
        // Вимикаємо вивід результату (1/-1) з методів Find... для чистоти вимірів часу
        Console.WriteLine($"{description,-30} - {watch.Elapsed}");
    }
}