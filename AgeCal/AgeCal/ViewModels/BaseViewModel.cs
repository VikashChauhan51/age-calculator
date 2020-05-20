using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using AgeCal.Models;
using AgeCal.Services;
using System.Threading.Tasks;
using AgeCal.Core;
using AgeCal.Ioc;
using GalaSoft.MvvmLight;
using AgeCal.Interfaces;

namespace AgeCal.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public EventHandler<Toast> DisplayToast;
        public ExclusiveRelayCommand<AgeNavigationType> TopNavigationCommand { get; set; }
        public ExclusiveRelayCommand<Type> BottomNavigationCommand { get; set; }
        public ExclusiveRelayCommand<Toast> ToastCommand { get; set; }
        public ExclusiveRelayCommand GoBackCommand { get; set; }

        protected BaseViewModel()
        {
            TopNavigationCommand = new ExclusiveRelayCommand<AgeNavigationType>(TopNavigation);
            ToastCommand = new ExclusiveRelayCommand<Toast>(ShowToast);
            BottomNavigationCommand = new ExclusiveRelayCommand<Type>(BottomNavigation);
            GoBackCommand = new ExclusiveRelayCommand(Back);
        }

        protected IAgeNavigationService NavigationService => IocRegistry.Locate<IAgeNavigationService>();
        protected ILocalizer Localizer => IocRegistry.Locate<ILocalizer>();
        protected IAppMessagingCenter MessageService => IocRegistry.Locate<IAppMessagingCenter>();
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }
        bool isReady = false;
        public bool IsReady
        {
            get { return isReady; }
            set
            {
                isReady = value;
                RaisePropertyChanged(nameof(IsReady));
            }
        }
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void BottomNavigation(Type viewModel)
        {
            if (!this.GetType().Equals(viewModel))
                NavigationService.NavigateTo(viewModel.ToString());
        }
        protected void Back()
        {
            NavigationService.GoBack();
        }
        protected void TopNavigation(AgeNavigationType type)
        {
            switch (type)
            {
                case AgeNavigationType.NONE:
                    break;
                case AgeNavigationType.BACK:
                    NavigationService.GoBack();
                    break;
                case AgeNavigationType.CLOSE:
                    break;
                default:
                    break;
            }
        }
        protected void ShowToast(Toast message)
        {
            DisplayToast?.Invoke(this, message);
        }
        public virtual void OnNavigationParameter(object parm)
        {
            IsReady = true;
        }
        public virtual void OnPageAppearing()
        {

        }
        public virtual void OnPageDisappearing()
        {

        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual Task<Tuple<string, object>> ShouldReRoute() => Task.FromResult(new Tuple<string, object>(string.Empty, null));

        #endregion
    }
}
