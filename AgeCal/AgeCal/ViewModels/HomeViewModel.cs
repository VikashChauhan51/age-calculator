using AgeCal.i18n;
using AgeCal.Interfaces;
using AgeCal.Models;
using AgeCal.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgeCal.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        public HomeViewModel(IUserService userService)
        {
            _userService = userService;
            Title = AppResource.Home;
            Items = new ObservableCollection<User>();
            LoadItemsCommand = new Command(LoadData);
            Message = string.Empty;
            MessageService.Register<User>(this, AddedUser);
        }


        public Command LoadItemsCommand { get; set; }
        ObservableCollection<User> items;
        public ObservableCollection<User> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged(nameof(Items));
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
            LoadData();
        }
        void AddedUser(User newUsre)
        {
            if (newUsre != null)
            {
                LoadData();

            }
        }
        public void LoadData()
        {
            try
            {
                if (!IsBusy)
                {

                    IsBusy = true;
                    Items.Clear();
                    var todays = _userService.GetTodayBirthdays();
                    if (todays.Any())
                    {
                        foreach (var today in todays)
                            Items.Add(today);
                        Message = "Today Birthdays";

                        return;
                    }
                    else
                    {
                        var upcomings = _userService.GetUpcomingBirthdays();
                        if (upcomings.Any())
                        {
                            foreach (var upcoming in upcomings)
                                Items.Add(upcoming);

                            Message = "Upcoming Birthdays";

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
