using AgeCal.i18n;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
using AgeCal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.ViewModels
{
    public class AddViewModel : BaseViewModel
    {

        public ExclusiveRelayCommand AddCommand { get; set; }
        private readonly IUserService _userService;
        private bool loaded = false;
        public AddViewModel(IUserService userService)
        {
            _userService = userService;
            AddCommand = new ExclusiveRelayCommand(Add);
            HasError = false;
            loaded = false;
        }
        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;

                if(loaded)
                HasError = string.IsNullOrEmpty(name);
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
        string phone = string.Empty;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                RaisePropertyChanged(nameof(Phone));
            }
        }
        bool hasError;
        public bool HasError
        {
            get { return hasError; }
            set
            {

                hasError = value;
                RaisePropertyChanged(nameof(HasError));
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
                if (!IsValid())
                    return;

                if (!IsBusy)
                {
                    IsBusy = true;
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Text = Name,
                        Description = Description,
                        DOB = DOB,
                        Time = Time,
                        Phone=Phone,
                        Email=string.Empty,
                        CreatedOn = DateTime.UtcNow
                    };
                    _userService.Add(user);
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
            loaded = false;

        }
        bool IsValid()
        {
            loaded = true;
            var isValid = true;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                isValid = false;

            if (DOB > DateTime.Now || DOB < new DateTime(1900, 1, 1))
                isValid = false;

            HasError = !isValid;
            return isValid;

        }
       public void Clear()
        {
            var date = DateTime.Now;
            Name = string.Empty;
            Description = string.Empty;
            Time = new TimeSpan(12, 0, 0);
            DOB = date;
            Phone = string.Empty;
            HasError = false;
            loaded = false;
        }
    }
}
