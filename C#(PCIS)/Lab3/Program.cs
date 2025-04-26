using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Collections.Generic;

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

            Console.WriteLine("\n========== ТЕСТУВАННЯ ШВИДКОСТІ ПОШУКУ У КОЛЕКЦІЯХ ==========\n");

            int studentsInCollection = 500000;

            NewStudent firstStud = TestCollections.Create(0);
            NewStudent halfStud = TestCollections.Create(studentsInCollection / 2);
            NewStudent lastStud = TestCollections.Create(studentsInCollection - 1);
            NewStudent theForgottenStud = new NewStudent();

            TestCollections testCollection = new TestCollections();
            testCollection.TestCollection(studentsInCollection);

            var watch = new System.Diagnostics.Stopwatch();

            // ---------------------- List<Person> ----------------------
            Console.WriteLine(">>> ПОШУК У List<Person>:");
            TestSearch(() => testCollection.FindListPerson(firstStud), "перший елемент", watch);
            TestSearch(() => testCollection.FindListPerson(halfStud), "центральний елемент", watch);
            TestSearch(() => testCollection.FindListPerson(lastStud), "останній елемент", watch);
            TestSearch(() => testCollection.FindListPerson(theForgottenStud), "неіснуючий елемент", watch);

            // ---------------------- Dictionary<Person, NewStudent> ----------------------
            Console.WriteLine(">>> ПОШУК У Dictionary<Person, NewStudent>:");
            TestSearch(() => testCollection.FindDictPerson(firstStud.Person), "перший елемент", watch);
            TestSearch(() => testCollection.FindDictPerson(halfStud.Person), "центральний елемент", watch);
            TestSearch(() => testCollection.FindDictPerson(lastStud.Person), "останній елемент", watch);
            TestSearch(() => testCollection.FindDictPerson(theForgottenStud.Person), "неіснуючий елемент", watch);

            // ---------------------- List<string> ----------------------
            Console.WriteLine(">>> ПОШУК У List<string>:");
            TestSearch(() => testCollection.FindListString($"Value String 0"), "перший елемент", watch);
            TestSearch(() => testCollection.FindListString($"Value String {studentsInCollection / 2}"), "центральний елемент", watch);
            TestSearch(() => testCollection.FindListString($"Value String {studentsInCollection - 1}"), "останній елемент", watch);
            TestSearch(() => testCollection.FindListString("Value String Nothing"), "неіснуючий елемент", watch);

            // ---------------------- Dictionary<string, NewStudent> ----------------------
            Console.WriteLine(">>> ПОШУК У Dictionary<string, NewStudent>:");
            TestSearch(() => testCollection.FindDictString(TestCollections.Create(0)), "перший елемент", watch);
            TestSearch(() => testCollection.FindDictString(TestCollections.Create(studentsInCollection / 2)), "центральний елемент", watch);
            TestSearch(() => testCollection.FindDictString(TestCollections.Create(studentsInCollection - 1)), "останній елемент", watch);
            TestSearch(() => testCollection.FindDictString(new NewStudent()), "неіснуючий елемент", watch);
        }

        /// <summary>
        /// Метод для вимірювання часу пошуку та виводу результату
        /// </summary>
        static void TestSearch(Action action, string description, System.Diagnostics.Stopwatch watch)
        {
            watch.Restart();
            action.Invoke();
            watch.Stop();
            Console.WriteLine($"{description,-30} - {watch.Elapsed}");
        }

    }

