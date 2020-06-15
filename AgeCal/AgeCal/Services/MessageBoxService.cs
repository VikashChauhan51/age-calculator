using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgeCal.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            try
            {
                return await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
            }
            catch
            {


            }
            return null;
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            try
            {
                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
            }
            catch
            {

            }
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            try
            {
                return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
            }
            catch
            {

            }
            return false;
        }
    }
}
