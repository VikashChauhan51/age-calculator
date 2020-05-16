using AgeCal.Components;
using AgeCal.Core;
using AgeCal.ViewModels;
using Rg.Plugins.Popup.Animations;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Views
{
    public class FullPagePopup<T> : AgePopup<T> where T : BaseViewModel
    {
        public FullPagePopup()
        {
            ControlTemplate = new FullPagePopupTemplate();
            HasSystemPadding = Device.RuntimePlatform == Device.Android;
            Animation = new MoveAnimation
            {
                PositionIn = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Bottom,
                PositionOut = Rg.Plugins.Popup.Enums.MoveAnimationOptions.Bottom
            };
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(ViewModel!=null)
            ViewModel.DisplayToast += Toast;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (ViewModel!=null)
            {
                ViewModel.DisplayToast -= Toast;
            }
        }

        void Toast(object sender, Toast message)
        {
            PageLayout?.Toast(message.Message, message.Duration);
        }
        public LayoutAlignment CloseAlignment { get; set; } = LayoutAlignment.End;
    }
}
