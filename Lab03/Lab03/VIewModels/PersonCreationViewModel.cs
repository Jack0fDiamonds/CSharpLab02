using Lab03.Tools.Managers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab03.Models;
using Lab03.Tools;
using Lab03.Tools.Navigation;
using Lab03.Exceptions;

namespace Lab03.VIewModels
{
    class PersonCreationViewModel:BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate;
        private bool _dateSet;

        private Person _person;

        private ICommand _proceedCommand;

        private readonly string[] chineseZodiacSigns = new string[] { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                _dateSet = true;
                OnPropertyChanged();
            }
        }

        public ICommand ProceedCommand
        {
            get
            {
                return _proceedCommand ??
                       (_proceedCommand = new RelayCommand<object>(ProceedImplementation, CanProceed));
            }
        }

        private bool CanProceed(object obj)
        {
            return !String.IsNullOrEmpty(_firstName) &&
                   !String.IsNullOrEmpty(_lastName) &&
                   !String.IsNullOrEmpty(_email) &&
                   _dateSet &&
                   _birthDate != null;
        }

        private async void ProceedImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Thread.Sleep(250);
                try
                {
                    _person = new Person(_firstName, _lastName, _email, _birthDate);
                    PersonManager.Person = _person;
                    //if (!IsEmailValid(_email))
                    //{
                    //    MessageBox.Show("Error\nInvalid email");
                    //    return false;
                    //}
                    //if (_person.Age < 0 || _person.Age > 135)
                    //{
                    //    MessageBox.Show("Error\nInvalid Date of birth");
                    //    return false;
                    //}

                    if (_person.IsBirthday)
                    {
                        MessageBox.Show("Happy Birthday!!!");
                    }

                    return true;
                }
                catch (InvalidEmailException ex)
                {
                    MessageBox.Show($"Error\nEmail {Email} is invalid");
                }
                catch (UnbornPersonException ex)
                {
                    MessageBox.Show("Error\nThis person has not been born yet");
                }
                catch (DeadPersonException ex)
                {
                    MessageBox.Show("Error\nThis person is already dead");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error\nCould not create this person");
                }

                return false;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                NavigationManager.Instance.Navigate(ViewType.PersonDisplay);
            }
        }
    }
}
