using AgeCal.i18n;
using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomNavigationView : ContentView
    {
        public BottomNavigationView()
        {
            InitializeComponent();

            HasVisibility = true;
            BottomNavBar.IsVisible = HasVisibility;
            TopDivider.IsVisible = HasVisibility;
            Button addButton = new Button
            {
                Style = (Style)Application.Current.Resources["BtnRound"],
                Text = "+",
                AutomationId = "addButton"

            };
            addButton.Clicked += AddData;

            AddIcon("dashboard.png", AppResource.Home, typeof(HomeViewModel));
            AddIcon("users.png", AppResource.Data, typeof(ItemsViewModel));
            this.IconLayout.Children.Add(addButton);
            AddIcon("reminder.png", AppResource.Reminders, typeof(ReminderListViewModel));
            AddIcon("setting.png", AppResource.Settings, typeof(SettingViewModel));
        }
        public static readonly BindableProperty ButtonPressedProperty = BindableProperty.Create(
            nameof(ButtonPressed),
            typeof(ICommand),
            typeof(BottomNavigationView),
            null,
            propertyChanged: (bindable, oldV, newV) => ((BottomNavigationView)bindable).UpdateButtonPressed((ICommand)oldV, (ICommand)newV));
        public ICommand ButtonPressed { get { return (ICommand)GetValue(ButtonPressedProperty); } set { SetValue(ButtonPressedProperty, value); } }

        protected virtual void UpdateButtonPressed(ICommand oldV, ICommand newV)
        {
            ButtonPressed = newV;
        }

        public static readonly BindableProperty HasVisibilityProperty = BindableProperty.Create(
           nameof(HasVisibility),
           typeof(bool),
           typeof(BottomNavigationView),
           null,
           propertyChanged: (bindable, oldV, newV) => ((BottomNavigationView)bindable).UpdateHasVisibility((bool)oldV, (bool)newV));
        public bool HasVisibility { get { return (bool)GetValue(HasVisibilityProperty); } set { SetValue(HasVisibilityProperty, value); } }

        protected virtual void UpdateHasVisibility(bool oldV, bool newV)
        {
            HasVisibility = newV;
            BottomNavBar.IsVisible = HasVisibility;
            TopDivider.IsVisible = HasVisibility;
        }
        private void AddData(object sender, EventArgs e)
        {

            ButtonPressed?.Execute(typeof(AddViewModel));
        }
        void AddIcon(string icon, string label, Type viewModelType)
        {
            var MainContainer = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(15, 0, 15, 0),
                Margin = 0,

            };
            var Icon = new ImageButton
            {
                Source = icon,
                HeightRequest = 42,
                Padding = 0,
                Margin = 0,
                BackgroundColor = Color.Transparent,
                WidthRequest = 42,
                AutomationId = label + "Icon"
            };
            Icon.Clicked += Icon_Clicked;
            Icon.CommandParameter = viewModelType;
            var Text = new Label
            {
                InputTransparent = true,
                Text = label,
                Margin = 0,
                FontSize = 10,
                AutomationId = label + "Label"
            };
            MainContainer.Children.Add(Icon);
            MainContainer.Children.Add(Text);
            this.IconLayout.Children.Add(MainContainer);
        }

        private void Icon_Clicked(object sender, EventArgs e)
        {
            var btn = sender as ImageButton;
            if (btn != null)
            {
                var type = btn.CommandParameter as Type;
                ButtonPressed?.Execute(type);
            }
        }
    }


}