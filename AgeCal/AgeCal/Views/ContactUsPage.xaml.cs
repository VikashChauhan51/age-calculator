using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUsPage : AgeContentPage<ContactUsViewModel>
    {
        public ContactUsPage()
        {
            InitializeComponent();
            PageTitle = "Contact Us";
            ShowBottomNav = false;
            PageHasbackButton = true;
            var html = new HtmlWebViewSource
            {
                Html = contact
            };
            ContactWebView.Source = html;
            ContactWebView.Navigating += ContactWebView_Navigating;
        }

        private void ContactWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Device.OpenUri(new Uri(e.Url));
            e.Cancel = true;
        }

        private static string contact= @"<p>For questions comments or concerns please email us at <a target=‘_blank’ href='mailto:vikashchauhan51@gmail.com'>vikashchauhan51@gmail.com</a></p> ";
    }
}