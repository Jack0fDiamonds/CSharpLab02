using Lab03.Models;
using Lab03.Tools;
using Lab03.Tools.Managers;

namespace Lab03.VIewModels
{
    class PersonDisplayViewModel:BaseViewModel
    {
        private Person _person;

        public PersonDisplayViewModel()
        {
            _person = PersonManager.Person;
        }

        public Person Person
        {
            get { return _person; }
            set { _person = value; }
        }
        public string FirstName
        {
            get { return _person.FirstName; }
        }
        public string LastName
        {
            get { return _person.LastName; }
        }
        public string Email
        {
            get { return _person.Email; }
        }
        public string BirthDate
        {
            get { return _person.BirthDate.ToShortDateString(); }
        }
        public string IsAdult
        {
            get { return _person.IsAdult ? "Adult" : "Under 18"; }
        }
        public string IsBirthday
        {
            get { return _person.IsBirthday ? "Today is Birthday" : "Today is not birthday"; }
        }
        public string ChineseSign
        {
            get { return _person.ChineseSign; }
        }
        public string SunSign
        {
            get { return _person.SunSign; }
        }
    }
}
