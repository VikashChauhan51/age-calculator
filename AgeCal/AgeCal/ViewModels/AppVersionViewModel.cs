using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AgeCal.ViewModels
{
    public class AppVersionViewModel : BaseViewModel
    {
        public AppVersionViewModel()
        {
            Name = AppInfo.Name;
            Version = AppInfo.VersionString;
        }
        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        string version = string.Empty;
        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                RaisePropertyChanged(nameof(Version));
            }
        }

    }
}