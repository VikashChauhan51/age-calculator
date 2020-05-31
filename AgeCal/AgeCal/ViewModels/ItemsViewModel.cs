using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using AgeCal.Models;
using System.Linq;
using System.Collections.Generic;
using AgeCal.Services;
using AgeCal.i18n;

namespace AgeCal.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<User> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadMoreItemsCommand { get; set; }
        private readonly IUserService _userService;
        public ItemsViewModel(IUserService userService)
        {
            Title = AppResource.Data;
            Items = new ObservableCollection<User>();
            _userService = userService;
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            LoadMoreItemsCommand = new Command(LoadMore);
            MessageService.Register<User>(this, AddedUser);
        }

        bool hasMore;
        public bool HasMore
        {
            get { return hasMore; }
            set
            {

                hasMore = value;
                RaisePropertyChanged(nameof(HasMore));
            }
        }

        private void LoadMore(object obj)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {


                var items = _userService.Gets(Items.Count, 10);
                RenderData(items);
            }
            catch (Exception ex)
            {


            }
            finally
            {
                IsBusy = false;
            }
        }

        private void RenderData(IEnumerable<User> items)
        {
            HasMore = items != null && items.Count() >= 10;
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.DOB = new DateTime(item.DOB.Year, item.DOB.Month, item.DOB.Day, item.Time.Hours, item.Time.Minutes, item.Time.Seconds);
                    Items.Add(item);
                }
            }

        }

        void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = _userService.Gets(0, 10);
                RenderData(items);
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
            ExecuteLoadItemsCommand();
        }

    }
}