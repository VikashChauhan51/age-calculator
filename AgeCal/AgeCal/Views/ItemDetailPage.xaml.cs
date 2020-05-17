using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AgeCal.Models;
using AgeCal.ViewModels;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : AgeContentPage<ItemDetailViewModel>
    {


        public ItemDetailPage() : base()
        {
            InitializeComponent();
            if (ViewModel != null)
            {
                PageTitle = ViewModel?.Item?.Text;
            }
            PageHasbackButton = true;

        }


    }
}