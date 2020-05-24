﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AgeCal.Models;
using AgeCal.Views;
using AgeCal.ViewModels;
using AgeCal.Ioc;
using AgeCal.Core;
using System.ComponentModel;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : AgeContentPage<ItemsViewModel>
    {


        public ItemsPage() : base()
        {
            InitializeComponent();

            if (ViewModel != null)
            {
                ShowSpinner = !ViewModel.IsReady;

            }
            PageTitle = "Data";
            ShowBottomNav = true;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as User;
            if (item == null)
                return;

            if (ViewModel != null)
                IocRegistry.Locate<IAgeNavigationService>().NavigateTo<ItemDetailViewModel>(item);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    base.OnViewModelPropertyChanged(sender, e);
                    switch (e.PropertyName)
                    {
                        case (nameof(ViewModel.IsReady)):
                            ShowSpinner = !ViewModel.IsReady;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {


                }
            });

        }


    }
}