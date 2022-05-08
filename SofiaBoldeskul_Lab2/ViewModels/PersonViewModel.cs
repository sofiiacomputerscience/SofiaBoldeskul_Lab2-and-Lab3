
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using SofiaBoldeskul_Lab2.Models;
    using SofiaBoldeskul_Lab2.Properties;
    using SofiaBoldeskul_Lab2.Tools;
    using SofiaBoldeskul_Lab2.Tools.Exceptions;
   using SofiaBoldeskul_Lab2.Tools.Managers;

    namespace SofiaBoldeskul_Lab2.ViewModels
{
    class PersonViewModel : INotifyPropertyChanged, ILoaderOwner
    {
        #region Fields
        private readonly Person _person = new Person();
        #endregion


        #region Commands
        private RelayCommand<object> _proceedCommand;
        #endregion


        #region Properties
        public string Name
        {
            get { return _person.Name; }
            set { _person.Name = value; }
        }

        public string Surname
        {
            get { return _person.Surname; }
            set { _person.Surname = value; }
        }

        public string Email
        {
            get { return _person.Email; }
            set { _person.Email = value; }
        }


        public DateTime BirthDate
        {
            get { return _person.BirthDate; }
            set { _person.BirthDate = value; }
        }

        public string BirthDateStr
        {
            get { return _person.BirthDate != DateTime.MinValue ? _person.BirthDate.ToShortDateString() : " "; }
        }

        public string SunSign
        {
            get { return _person.SunSign; }

        }

        public string ChineseSign
        {
            get { return _person.ChineseSign; }
        }

        public bool? IsAdult
        {
            get { return _person.IsAdult; }
        }

        public bool? IsBirthday
        {
            get { return _person.IsBirthday; }
        }
        #endregion


        #region Commands

        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(Proceed, o => CanExecuteCommand()));
            }
        }

        #endregion

        private bool CanExecuteCommand()
        {
            return !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Surname) && !String.IsNullOrWhiteSpace(Email) && (BirthDate != DateTime.MinValue);
        }



        private void Proceed()
        {

            if (_person.BirthDate.Month.CompareTo(DateTime.Today.Month) == 0 &&
                _person.BirthDate.Day.CompareTo(DateTime.Today.Day) == 0)
            {
                MessageBox.Show("Happy Birthday! Enjoy your special day!");
            }

            _person.CalculateSunSign();
            _person.CalculateChineseSign();
            _person.CalculateIsAdult();
            _person.CalculateIsBirthday();


            OnPropertyChanged("Name");
            OnPropertyChanged("Surname");
            OnPropertyChanged("Email");
            OnPropertyChanged("BirthDateStr");
            OnPropertyChanged("SunSign");
            OnPropertyChanged("ChineseSign");
            OnPropertyChanged("IsAdult");
            OnPropertyChanged("IsBirthday");

            Thread.Sleep(1000);



        }






        private async void Proceed(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            bool res = await Task.Run(() =>
            {
                try
                {
                    ValidName(Name);
                }
                catch (NameException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                try
                {
                    ValidSurname(Surname);
                }
                catch (SurnameException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                try
                {
                    ValidEmail();
                }
                catch (EmailException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                try
                {
                    ValidDateOfBirth();
                }
                catch (BirthException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                catch (DeadException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                Proceed();
                return true;
            });
            LoaderManager.Instance.HideLoader();
        }




        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        #endregion


        #region Properties
        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion



        #region Validation

        private void ValidEmail()
        {
            if (!Regex.IsMatch(Email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase))
            {
                throw new EmailException(Email);
            }
        }

        private static void ValidName(string Name)
        {
            if (!Regex.IsMatch(Name, @"^[a-zA-Z]+$"))
            {
                throw new NameException(Name);
            }

        }
        private static void ValidSurname(string Surname)
        {
            if (!Regex.IsMatch(Surname, @"^[a-zA-Z]+$"))
            {
                throw new SurnameException(Surname);
            }
        }


        private void ValidDateOfBirth()
        {
            if (BirthDate > DateTime.Today)
            {
                throw new BirthException(BirthDate);
            }

            if (BirthDate.Year < (DateTime.Today.Year - 135))
            {
                throw new DeadException(BirthDate);
            }
        }
        #endregion


        internal PersonViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }
    }

    internal class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
    }
}
