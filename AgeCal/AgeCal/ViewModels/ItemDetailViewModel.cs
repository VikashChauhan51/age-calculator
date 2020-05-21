using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;

namespace AgeCal.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ExclusiveRelayCommand DeleteCommand { get; set; }
        public ExclusiveRelayCommand ShareCommand { get; set; }
        private readonly IUserRepository _userRepository;
        public ItemDetailViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            DeleteCommand = new ExclusiveRelayCommand(Delete);
            ShareCommand = new ExclusiveRelayCommand(Share);
        }

        private void Share()
        {
             
        }

        private  void Delete()
        {
            NavigationService.GoBack();
        }

        string id = string.Empty;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged(nameof(Id));
            }
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

        public override void OnNavigationParameter(object parm)
        {
            if (parm is User item)
            {
                Id = item.Id;
                Name = item.Text;
                Description = item.Description;
                DOB = item.DOB;
                Time = item.Time;
            }

        }

        public override void OnPageAppearing()
        {
            base.OnPageAppearing();


        }

        void ExecuteLoadCommand(string id)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var item = _userRepository.Get(id);
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Text;
                    Description = item.Description;
                    DOB = item.DOB;
                    Time = item.Time;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;

            }
        }


    }
}
