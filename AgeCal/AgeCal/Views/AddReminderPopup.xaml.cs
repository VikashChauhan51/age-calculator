using AgeCal.Models;
using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReminderPopup : FullPagePopup<AddReminderViewModel>
    {
        public AddReminderPopup()
        {
            InitializeComponent();
            
        }

        private void UserPicker_Focused(object sender, FocusEventArgs e)
        {
            if (ViewModel == null || ViewModel.IsBusy || ViewModel.Items.Count == 0)
                return;

            if (ViewModel.HasMore)
                ViewModel.LoadMoreCommand.Execute(null);
        }
    }
}