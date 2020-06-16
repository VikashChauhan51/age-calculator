using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AgeCal.Components
{
    public class BottomNavIcon : TouchableView
    {
        protected StackLayout MainLayout;
        protected ImageButton ImageElement;
        protected Label LabelElement;
        public BottomNavIcon()
        {
            MainLayout = new StackLayout
            {
                Padding = 0,
                Margin = 0,
                HorizontalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            ImageElement = new ImageButton
            {
                InputTransparent = true,
                HeightRequest = 32,
                Padding = 0,
                Margin = 0,
                BackgroundColor = Color.Transparent,
                WidthRequest = 32,
            };
            LabelElement = new Label
            {
                InputTransparent = true,
                Text = Label,
                Margin = 0,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 10,

            };
            MainLayout.Children.Add(ImageElement);
            MainLayout.Children.Add(LabelElement);
            Content = TouchableLayout = MainLayout;
        }

        public new static readonly BindableProperty PaddingProperty = BindableProperty.Create(
           nameof(Padding),
           typeof(Thickness),
           typeof(BottomNavIcon),
           null,
           propertyChanged: (bindable, oldV, newV) => ((BottomNavIcon)bindable).UpdatePadding((Thickness)oldV, (Thickness)newV));
        public new Thickness Padding { get { return (Thickness)GetValue(ClickedProperty); } set { SetValue(ClickedProperty, value); } }

        protected virtual void UpdatePadding(Thickness oldV, Thickness newV)
        {
            Padding = newV;
        }
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
           nameof(BackgroundColor),
           typeof(Color),
           typeof(BottomNavIcon),
           Color.Transparent,
           propertyChanged: (bindable, oldV, newV) => ((BottomNavIcon)bindable).UpdateBackgroundColor((Color)oldV, (Color)newV));
        public new Color BackgroundColor { get { return (Color)GetValue(BackgroundColorProperty); } set { SetValue(BackgroundColorProperty, value); } }

        protected virtual void UpdateBackgroundColor(Color oldV, Color newV)
        {
            BackgroundColor = newV;
            MainLayout.BackgroundColor = newV;
        }

        public new static readonly BindableProperty ClickedProperty = BindableProperty.Create(
           nameof(ClickCommand),
           typeof(ICommand),
           typeof(BottomNavIcon),
           null,
           propertyChanged: (bindable, oldV, newV) => ((BottomNavIcon)bindable).UpdateButtonPressed((ICommand)oldV, (ICommand)newV));
        public new ICommand ClickCommand { get { return (ICommand)GetValue(ClickedProperty); } set { SetValue(ClickedProperty, value); } }

        protected override void UpdateButtonPressed(ICommand oldV, ICommand newV)
        {
            base.UpdateButtonPressed(oldV, newV);
        }
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(
           nameof(Label),
           typeof(string),
           typeof(BottomNavIcon),
           null,
           propertyChanged: (bindable, oldV, newV) => ((BottomNavIcon)bindable).UpdateLabel((string)oldV, (string)newV));
        public string Label { get { return (string)GetValue(LabelProperty); } set { SetValue(LabelProperty, value); } }

        protected virtual void UpdateLabel(string oldV, string newV)
        {
            Label = newV;
            LabelElement.Text = newV;
        }
    }
}
