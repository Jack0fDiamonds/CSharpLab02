using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Lab03.Exceptions;
using Lab03.Tools.Managers;

namespace Lab03.Models
{
    internal class Person
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate;
        private string _sunSign;
        private int _age;

        private readonly string[] chineseZodiacSigns = new string[] { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };

        public string FirstName
        {
            get { return _firstName; }
            private set { _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            private set { _lastName = value; }
        }
        public string Email
        {
            get { return _email; }
            private set { _email = value; }
        }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            private set { _birthDate = value; }
        }
        public bool IsAdult
        {
            get
            {
                CalculateAge();
                return _age >= 18;
            }
        }
        public bool IsBirthday
        {
            get { return DateTime.Today.DayOfYear == _birthDate.DayOfYear; }
        }

        public string ChineseSign
        {
            get { return chineseZodiacSigns[_birthDate.Year % 12]; }
        }

        public string SunSign
        {
            get
            {
                WesternZodiac();
                return _sunSign;
            }
        }

        public int Age
        {
            get
            {
                CalculateAge();
                return _age;
            }
            set { _age = value; }
        }

        private void CalculateAge()
        {
            _age = DateTime.Today.Year - _birthDate.Year - (_birthDate.DayOfYear > DateTime.Today.DayOfYear ? 1 : 0);
        }

        private string DetermineWesternZodiac()
        {
            int day = _birthDate.DayOfYear;
            if (day > 355 || day < 20)
                return "Capricorn";
            else if (day < 50)
                return "Aquarius";
            else if (day < 80)
                return "Pisces";
            else if (day < 110)
                return "Aries";
            else if (day < 141)
                return "Taurus";
            else if (day < 172)
                return "Gemini";
            else if (day < 204)
                return "Cancer";
            else if (day < 235)
                return "Leo";
            else if (day < 266)
                return "Virgo";
            else if (day < 296)
                return "Libra";
            else if (day < 326)
                return "Scorpio";
            return "Sagittarius";
        }

        private async void WesternZodiac()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                _sunSign = DetermineWesternZodiac();
                Thread.Sleep(200);
            });
            LoaderManager.Instance.HideLoader();
        }

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = birthDate;
            ValidatePersonData(this);
        }

        public Person(string firstName, string lastName, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = DateTime.Now;
            ValidatePersonData(this);
        }

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthDate = birthDate;
            _email = "";
            ValidatePersonData(this);
        }

        private void ValidatePersonData(Person person)
        {
            if (!IsEmailValid(_email)) throw new InvalidEmailException();
            if (Age < 0) throw new UnbornPersonException();
            if (Age > 135) throw new DeadPersonException();
        }

        private bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
