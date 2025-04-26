using System;
using System.Collections;
using System.Collections.Generic;
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
        static void Main()
        {
            // 1. Створити 2 об'єкти типу Person з однаковими даними та перевірити чи посилання на об'єкти рівні, вивести значення хеш-кодів для об'єктів.
            Person person1 = new Person("John", "Doe", new DateTime(1990, 5, 15));
            Person person2 = new Person("John", "Doe", new DateTime(1990, 5, 15));

            Console.WriteLine("Перевірка посилань на об'єкти Person:");
            Console.WriteLine($"Посилання person1 == person2: {ReferenceEquals(person1, person2)}");
            Console.WriteLine($"Хеш-код person1: {person1.GetHashCode()}");
            Console.WriteLine($"Хеш-код person2: {person2.GetHashCode()}");
            Console.WriteLine();

            // 2. Створити об'єкт типу NewStudent, додати елементи в список іспитів та заліків, вивести дані об'єкту NewStudent.
            NewStudent newStudent = new NewStudent(
                "Alice",
                "Smith",
                new DateTime(2002, 10, 20),
                Education.Bachelor,
                201,
                new ArrayList() { "Test1", "Test2" },
                new ArrayList() { new Exam("Математика", 95, new DateTime(2024, 12, 10)), new Exam("Фізика", 88, new DateTime(2024, 12, 15)) }
            );

            Console.WriteLine("Дані об'єкту NewStudent:");
            Console.WriteLine(newStudent.ToString());
            Console.WriteLine();
            Console.WriteLine($"Коротка інформація про NewStudent: {newStudent.ToShortString()}");
            Console.WriteLine();

            // 3. Вивести значення властивості типу Person для об'єкту типу NewStudent.
            Console.WriteLine("Властивість типу Person для об'єкту NewStudent:");
            Console.WriteLine(newStudent.Person);
            Console.WriteLine();

            // 4. За допомогою методу DeepCopy створити повну копію об'єкту NewStudent. Змінити дані в оригіналі NewStudent та вивести копію та оригінал, повна копія повинна залишитися без змін.
            NewStudent newStudentCopy = (NewStudent)newStudent.DeepCopy();

            // Змінюємо дані в оригіналі
            newStudent.GroupNum = 202;
            newStudent.ExamList.Add(new Exam("Програмування", 92, new DateTime(2024, 12, 20)));
            newStudent.TestList.Add("Test3");
            newStudent.Person.Date = new DateTime(2003, 1, 1);

            Console.WriteLine("Оригінал NewStudent:");
            Console.WriteLine(newStudent.ToString());
            Console.WriteLine();

            Console.WriteLine("Копія NewStudent:");
            Console.WriteLine(newStudentCopy.ToString());
            Console.WriteLine();

            // 5. В блоці try/catch присвоїти властивості номер групи неприпустиме значення, а в обробнику виключної ситуації вивести повідомлення про помилку.
            NewStudent newStudentForException = new NewStudent();
            try
            {
                newStudentForException.GroupNum = 50; // Неприпустиме значення (поза межами 100-699)
                Console.WriteLine($"Номер групи після присвоєння: {newStudentForException.GroupNum}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Виникла помилка при спробі присвоїти неприпустиме значення номеру групи: {ex.Message}");
            }
            Console.WriteLine();

            // 6. За допомогою оператору foreach для ітератора з параметром, визначеного в класі NewStudent, вивести список всіх іспитів з оцінкою вищою за 3.
            Console.WriteLine("Список іспитів з оцінкою вищою за 3 для NewStudent:");
            foreach (Exam exam in newStudent.ExamList)
            {
                if (exam is Exam && ((Exam)exam).Mark > 3)
                {
                    Console.WriteLine(exam);
                }
            }
        }
    }
