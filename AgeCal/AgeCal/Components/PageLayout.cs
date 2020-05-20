using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Components
{
    public class PageLayout : AbsoluteLayout
    {
        private ToastView toast;
        private object toastLocak = new object();
        private ActivityIndicator spinner;
        public PageLayout()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        public void ShowSpinner()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (spinner == null)
                {
                    spinner = new ActivityIndicator
                    {
                        IsRunning = true,
                        IsEnabled = true,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Color = Color.Gray,
                        HeightRequest = 32,
                        WidthRequest = 32,

                    };
                    this.Children.Add(spinner);
                    AbsoluteLayout.SetLayoutFlags(spinner, AbsoluteLayoutFlags.PositionProportional);
                    AbsoluteLayout.SetLayoutBounds(spinner, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

                }
                spinner.IsVisible = true;
            });
        }
        public void HideSpinner()
        {
            if (spinner != null)
                Device.BeginInvokeOnMainThread(() => { spinner.IsVisible = false; });
        }
        public void Toast(string message, int duration = 3000)
        {
            if (duration <= 0)
                duration = 3000;

            var bottom = this.FindElementByName<BottomNavigationView>("BottomNavigationView");
            Device.BeginInvokeOnMainThread(() =>
            {
                if (toast == null)
                {
                    toast = new ToastView();
                    this.Children.Add(toast);
                    AbsoluteLayout.SetLayoutBounds(toast, new Rectangle(0, this.Height, 1, AbsoluteLayout.AutoSize));
                    AbsoluteLayout.SetLayoutFlags(toast, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional);
                }
                this.HeightRequest = 40;
                toast.Message = message;
                toast.IsVisible = true;
                double height = this.HeightRequest > 124 ? 100 : this.HeightRequest;
                double bottomNavHeight = 50;
                var transY = height + bottomNavHeight + 10;
                toast.TranslateTo(0, -transY, 500, Easing.CubicInOut);
                toast.FadeTo(100, 500, Easing.CubicInOut);
                Device.StartTimer(TimeSpan.FromMilliseconds(duration), Closed);
            });

        }

        private bool Closed()
        {
            if (toast == null) return false;
            lock (toastLocak)
            {
                toast?.TranslateTo(0, 0, 500, Easing.CubicIn);
                toast?.FadeTo(0, 500, Easing.CubicIn).ContinueWith((task) =>
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                            if (toast != null && this.Children.Contains(toast))
                            {
                                this.Children.Remove(toast);

                            }
                            toast = null;
                        }
                        catch
                        {


                        }
                    });
                });
            }
            return false;

        }
    }
}
