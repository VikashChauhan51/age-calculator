using AgeCal.Interfaces;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
        public HomeViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            Title = "Dashboard";


        }

        User birthday = new User();
        public User Birthday
        {
            get { return birthday; }
            set
            {
                if (value == null)
                    birthday = new User();
                else
                    birthday = value;

                RaisePropertyChanged(nameof(Birthday));
            }
        }

        bool hasData;
        public bool HasData
        {
            get { return hasData; }
            set
            {
                hasData = value;
                RaisePropertyChanged(nameof(HasData));
            }
        }

        string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }



        public override void OnPageAppearing()
        {
            base.OnPageAppearing();

        }

        public void SetBirthday()
        {
            try
            {
                if (!IsBusy)
                {

                    IsBusy = true;
                    Message = string.Empty;
                    var today = _userRepository.GetTodayBirthday();
                    if (today != null)
                    {
                        HasData = true;
                        Birthday = today;
                        if (_userRepository.TodayHasMoreBirthday())
                            Message = "Today has more than one Birthdays";
                    }
                    else
                    {
                        var month = _userRepository.GetMonthBirthday();
                        if (month != null)
                        {
                            HasData = true;
                            Birthday = today;
                            if (_userRepository.MonthHasMoreBirthday())
                                Message = "This month has more than one Birthdays";
                        }
                        else
                        {
                            var year = _userRepository.GetYearBirthday();
                            if (year != null)
                            {
                                HasData = true;
                                Birthday = today;
                                if (_userRepository.YearsHasMoreBirthday())
                                    Message = "This year has more than one Birthdays";
                            }
                            else
                            {
                                HasData = false;
                                Message = string.Empty;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
