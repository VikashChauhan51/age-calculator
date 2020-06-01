using AgeCal.ViewModels;
using AgeCal.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPopup : SemiFullPagePopup<AddViewModel>
    {
        public AddPopup()
        {
            InitializeComponent();
            Container = AddPopupContainer;
            AgePicker.MaximumDate = DateTime.Now;
            AgePicker.MinimumDate = new DateTime(1900, 1, 1);
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnClose += AddPopup_OnClose;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            OnClose -= AddPopup_OnClose;
        }
        private void AddPopup_OnClose(object sender, EventArgs e)
        {
            if (ViewModel!=null)
            {
                var vm = ViewModel as AddViewModel;
                if (vm!=null)
                {
                    vm.Clear();
                }

            }
        }
    }
}