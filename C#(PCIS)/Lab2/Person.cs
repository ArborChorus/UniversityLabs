using System;

    class Person : IDateAndCopy
    {
        protected string? _firstName;
        protected string? _secondName;
        protected DateTime _birthDate;


        protected string? FirstName
        {
            get { return _firstName; }
            init
            {
                _firstName = value;
            }
        }

        protected string? SecondName
        {
            get { return _secondName; }
            init
            {
                _secondName = value;
            }
        }

        public DateTime Date
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }


        public Person(string firstName, string secondName, DateTime birthDate) => (FirstName, SecondName, Date) = (firstName, secondName, birthDate);

        public Person() : this("dummy", "dumbest", DateTime.Parse("2000-01-01")) { }



        public int YearChange
        {
            get { return _birthDate.Year; }
            init { _birthDate = new DateTime(value, _birthDate.Month, _birthDate.Day); }
        }

        public override string ToString()
        {
            return $"{FirstName} {SecondName} {Date.ToShortDateString()}";
        }

        public virtual string ToShortString()
        {
            return $"{FirstName} {SecondName}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || ReferenceEquals(this, obj) || GetType() != obj.GetType()) return false;
            Person other = (Person)obj;
            return (FirstName == other.FirstName) && (SecondName == other.SecondName) && (Date == other.Date);
        }

        public static bool operator ==(Person? person1, Person? person2)
        {

            if (person1 is null || person2 is null)
            {
                return false;
            }

            return person1.Equals(person2);
        }

        public static bool operator !=(Person? person1, Person? person2) => !(person1==person2);


        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, SecondName, Date);
        }

        public virtual object? DeepCopy()
        {
            return new Person(FirstName, SecondName, Date);
        }
    }