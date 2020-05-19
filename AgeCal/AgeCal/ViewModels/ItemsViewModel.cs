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
            Title = "Browse";
            Items = new ObservableCollection<User>();
            _userRepository = userRepository;
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
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
        public override void OnPageAppearing()
        {
            base.OnPageAppearing();
            Task.Run(() => ExecuteLoadItemsCommand());
        }

    }
}