﻿using AgeCal.i18n;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.ViewModels
{
    public class AddViewModel : BaseViewModel
    {

        public ExclusiveRelayCommand AddCommand { get; set; }
        private readonly IUserRepository _userRepository;
        public AddViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            AddCommand = new ExclusiveRelayCommand(Add);

        }
        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        DateTime dob = DateTime.Now;
        public DateTime DOB
        {
            get { return dob; }
            set
            {
                dob = value;
                RaisePropertyChanged(nameof(DOB));
            }
        }
        TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        public void Add()
        {
            try
            {
                if (!IsBusy && isValid())
                {
                    IsBusy = true;
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Text = Name,
                        Description = Description,
                        DOB = DOB,
                        Time = Time,
                        CreatedOn = DateTime.UtcNow
                    };
                    _userRepository.Add(user);
                    MessageService.Send<User>(user);
                    Clear();
                    NavigationService.GoBackModel(new Core.Toast { Message = AppResource.DataSaveMessage });

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

        public override void OnNavigationParameter(object parm)
        {


        }
        bool isValid()
        {
            var isValid = true;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                isValid = false;
            if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                isValid = false;
            if (DOB > DateTime.Now || DOB < new DateTime(1900, 1, 1))
                isValid = false;

            return isValid;

        }
        void Clear()
        {
            var date = DateTime.Now;
            Name = string.Empty;
            Description = string.Empty;
            Time = new TimeSpan(date.Hour, date.Minute, date.Second);
            DOB = date;
        }
    }
}
