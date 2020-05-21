using AgeCal.Core;
using AgeCal.Ioc;
using AgeCal.Models;
using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : AgeContentPage<MainViewModel>
    {

        public MainPage() : base()
        {
            InitializeComponent();
            PageTitle = "Age Calculator";
            
        }

      
    }
}