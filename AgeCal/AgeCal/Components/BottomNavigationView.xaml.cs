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
        private List<NavbarIcon> IconContainer;
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
                FontSize = 16,
                AutomationId = "addButton"

            };
            addButton.Clicked += AddData;

            AddIcon(ActiveIcons[typeof(HomeViewModel)], AppResource.Home, typeof(HomeViewModel));
            AddIcon(ActiveIcons[typeof(ItemsViewModel)], AppResource.Data, typeof(ItemsViewModel));
            this.IconLayout.Children.Add(addButton);
            AddIcon(ActiveIcons[typeof(ReminderListViewModel)], AppResource.Reminders, typeof(ReminderListViewModel));
            AddIcon(ActiveIcons[typeof(SettingViewModel)], AppResource.Settings, typeof(SettingViewModel));
            SetContainer();
           
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
                Padding = new Thickness(10, 0, 10, 0),
                Margin = 0,

            };
            var subContainer = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 0,
                Margin = 0
            };
            var Icon = new ImageButton
            {
                Source = icon,
                HeightRequest = 32,
                Padding = 0,
                Margin = 0,
                BackgroundColor = Color.Transparent,
                WidthRequest = 32,
                AutomationId = label + "Icon",
                BorderColor = Color.Transparent
            };
            Icon.Clicked += Icon_Clicked;
            Icon.CommandParameter = viewModelType;
            var Text = new Label
            {
                InputTransparent = true,
                Text = label,
                Margin = 0,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 10,
                FontAttributes = FontAttributes.None,
                AutomationId = label + "Label"
            };
            subContainer.Children.Add(Icon);
            subContainer.Children.Add(Text);
            MainContainer.Children.Add(subContainer);
            this.IconLayout.Children.Add(MainContainer);
            FlexLayout.SetGrow(Icon, 1f);
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

        private Dictionary<Type, string> ActiveIcons = new Dictionary<Type, string>
        {
            {typeof(HomeViewModel),"home_active.png" },
            {typeof(ItemsViewModel),"users_active.png" },
            {typeof(ReminderListViewModel),"reminder_active.png" },
            {typeof(SettingViewModel),"setting_active.png" },
        };

        public void SetActiveIcon(Type vm)
        {
            if (IconContainer != null)
            {

                foreach (var item in IconContainer)
                {
                    if ((Type)item.Icon.CommandParameter == vm)
                    {
                        item.Text.FontAttributes = FontAttributes.Bold;
                    }
                    else
                    {
                        item.Text.FontAttributes = FontAttributes.None;

                    }
                }
            }

        }
        private void SetContainer()
        {
            IconContainer = null;
            IconContainer = new List<NavbarIcon>();
            var mainContainer = this.IconLayout.Children.Where(x => x.GetType() == typeof(StackLayout)).ToList();
            if (mainContainer != null)
            {
                foreach (StackLayout item in mainContainer)
                {
                    var stack = (StackLayout)item.Children.First(x => x.GetType() == typeof(StackLayout));
                    if (stack != null)
                    {
                        IconContainer.Add(new NavbarIcon
                        {
                            Icon = (ImageButton)stack.Children.First(x => x.GetType() == typeof(ImageButton)),
                            Text = (Label)stack.Children.First(x => x.GetType() == typeof(Label))
                        });
                    };
                }
            }
        }

    }

    public class NavbarIcon
    {
        public ImageButton Icon { get; set; }
        public Label Text { get; set; }
    }
}