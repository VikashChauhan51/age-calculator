using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AgeCal.Models;
using AgeCal.Views;
using AgeCal.Interfaces;

namespace AgeCal.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<User> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private readonly IUserRepository _userRepository;
        public ItemsViewModel(IUserRepository userRepository)
        {
            Title = "Data";
            Items = new ObservableCollection<User>();
            _userRepository = userRepository;
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            MessageService.Register<User>(this, AddedUser);
        }

        void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = _userRepository.GetAll(0, 50);
                foreach (var item in items)
                {
                    item.DOB = new DateTime(item.DOB.Year, item.DOB.Month, item.DOB.Day, item.Time.Hours, item.Time.Minutes, item.Time.Seconds);
                    Items.Add(item);
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
        void AddedUser(User newUsre)
        {
            if (newUsre != null)
            {
                newUsre.DOB = new DateTime(newUsre.DOB.Year, newUsre.DOB.Month, newUsre.DOB.Day, newUsre.Time.Hours, newUsre.Time.Minutes, newUsre.Time.Seconds);
                Items.Add(newUsre);
            }
        }
        public override void OnPageAppearing()
        {
            base.OnPageAppearing();
            Task.Run(() => ExecuteLoadItemsCommand());
        }

    }
}