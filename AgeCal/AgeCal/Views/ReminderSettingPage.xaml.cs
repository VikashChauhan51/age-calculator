using AgeCal.ViewModels;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderSettingPage : AgeContentPage<ReminderSettingViewModel>
    {
        public ReminderSettingPage()
        {
            InitializeComponent();
            PageTitle = "Reminder Settings";
            ShowBottomNav = false;
            PageHasbackButton = true;
        }
    }
}