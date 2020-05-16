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

            Button addButton = new Button
            {
                Style = (Style)Application.Current.Resources["BtnRound"],
                Text = "+"

            };
            addButton.Clicked += AddData;

            this.IconLayout.Children.Add(AddIcon("home.png",-10,0));
            this.IconLayout.Children.Add(AddIcon("data.png", 0, 10));
            this.IconLayout.Children.Add(addButton);
            this.IconLayout.Children.Add(AddIcon("fact.png", 10, 10));
            this.IconLayout.Children.Add(AddIcon("team.png", 10, 0));
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
        private void AddData(object sender, EventArgs e)
        {

            ButtonPressed?.Execute(typeof(ViewModels.ItemsViewModel));
        }
        ImageButton AddIcon(string icon, double leftPadding, double rightPadding)
        {
            return new ImageButton
            {
                Source = icon,
                HeightRequest = 48,
                WidthRequest = 48,
                Padding = new Thickness(leftPadding, 0, rightPadding, 0),
                Margin = 0,
                BackgroundColor = Color.Transparent

            };
        }
    }
}