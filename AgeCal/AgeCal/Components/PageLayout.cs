using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Components
{
    public class PageLayout : AbsoluteLayout
    {
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
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,

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
    }
}
