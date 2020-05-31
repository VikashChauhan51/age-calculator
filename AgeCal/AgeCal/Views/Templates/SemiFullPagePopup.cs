using AgeCal.Components;
using AgeCal.Ioc;
using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Views
{
    public class SemiFullPagePopup<T> : FullPagePopup<T> where T : BaseViewModel
    {
        private ImageButton closeButton;
        public SemiFullPagePopup() : base()
        {
            ControlTemplate = new SemiFullPagePopupTemplate();
            HasSystemPadding = Device.RuntimePlatform == Device.Android;

        }

        private BoxView _overlayBox;
        protected BoxView OverlayBox
        {
            get
            {
                if (_overlayBox == null)
                    _overlayBox = this.FindElementByName<BoxView>("OverlayBox");
                return _overlayBox;
            }
        }
        private StackLayout _semiPopupLayout;
        protected StackLayout SemiPopupLayout
        {
            get
            {
                if (_semiPopupLayout == null)
                    _semiPopupLayout = this.FindElementByName<StackLayout>("SemiPopupLayout");
                return _semiPopupLayout;
            }
        }

        protected Layout Container
        {
            set
            {
                if (value != null)
                {
                    if (value is StackLayout)
                    {
                        StackLayout closeContainer = new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            BackgroundColor = Color.Transparent,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Fill
                        };
                        BoxView space = new BoxView()
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        };
                        closeContainer.Children.Add(space);
                        closeButton = new ImageButton
                        {
                            Source = "cross.png",
                            HorizontalOptions = LayoutOptions.End,
                            HeightRequest = 32,
                            WidthRequest = 32,
                            Padding = 0,
                            Margin = 0,
                            BackgroundColor = Color.Transparent,
                            AutomationId = "BtnClose"
                        };
                        closeButton.Clicked += CloseButton_Clicked;
                        closeContainer.Children.Add(closeButton);
                        AbsoluteLayout.SetLayoutFlags(closeButton, AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.SizeProportional);
                        BoxView box = new BoxView()
                        {
                            BackgroundColor = Color.Black,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            HeightRequest = 4,
                            WidthRequest = 30
                        };
                        AbsoluteLayout.SetLayoutBounds(closeButton, new Rectangle(1, 0, 1, 1));
                        ((StackLayout)value).Children.Insert(0, closeContainer);
                        ((StackLayout)value).Children.Insert(0, box);

                    }
                }
            }
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        public void ClosePopup()
        {
            IocRegistry.Locate<Core.IAgeNavigationService>().GoBackModel();

        }
    }
}
