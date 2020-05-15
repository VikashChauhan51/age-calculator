using AgeCal.Components;
using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Views
{
    public class SemiFullPagePopup<T> : FullPagePopup<T> where T : BaseViewModel
    {
        public SemiFullPagePopup() : base()
        {
            ControlTemplate = new SemiFullPagePopupTemplate();
            HasSystemPadding = Device.RuntimePlatform == Device.Android;
        }
    }
}
