using AgeCal.Themes;
using AgeCal.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Utilities
{
  public static  class ThemeHelper
    {
        public static bool SetAppTheme(Theme selectedTheme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (selectedTheme)
                {
                    case Theme.Dark:
                        mergedDictionaries.Add(new DarkTheme());
                        break;

                    case Theme.Light:
                        mergedDictionaries.Add(new LightTheme());
                        break;

                    case Theme.Pink:
                        mergedDictionaries.Add(new PinkTheme());
                        break;

                    case Theme.Blue:
                        mergedDictionaries.Add(new BlueTheme());
                        break;

                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }

                return true;
            }

            return false;
        }
    }
}
