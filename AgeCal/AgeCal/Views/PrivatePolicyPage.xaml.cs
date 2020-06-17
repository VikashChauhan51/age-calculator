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
    public partial class PrivatePolicyPage : AgeContentPage<PrivatePolicyViewModel>
    {
        public PrivatePolicyPage()
        {
            InitializeComponent();
            PageTitle = "Private Policy";
            ShowBottomNav = false;
            PageHasbackButton = true;
            var html = new HtmlWebViewSource
            {
                Html = policy
            };
            PrivateWebView.Source = html;
        }

        private static string policy = @"<!DOCTYPE html>
    <html>
    <head>
      <meta charset='utf-8'>
      <meta name='viewport' content='width=device-width'>
      <title>Privacy Policy</title>
      <style> body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; padding:1em; } </style>
    </head>
    <body>
    <strong>Privacy Policy</strong> <p>
                  Tidings team built the Tidings app as
                  a Free app.This is a offline birthday reminder app. This SERVICE is provided by
                  Tidings team at no cost and is intended for use as
                  is.
                </p> <p>
                  This page is used to inform visitors regarding our
                  policies with the collection, use, and disclosure of Personal
                  Information if anyone decided to use our Service.
                </p> <p>
                  If you choose to use our Service, then you agree to
                  the collection and use of information in relation to this
                  policy. The Personal Information that we collect is
                  used for providing and improving the Service. we will not use or share your information with
                  anyone except as described in this Privacy Policy.
                </p> <p>
                  The terms used in this Privacy Policy have the same meanings
                  as in our Terms and Conditions, which is accessible at
                  Tidings unless otherwise defined in this Privacy Policy.
                </p> <p><strong>Information Collection and Use</strong></p> <p>
                  For a better experience, while using our Service, We
                  may require you to provide us with certain personally
                  identifiable information, including but not limited to Full name, Phone number and date of birth of user. The information that
                  We request will be retained on your device and is not collected by us in any way. We are not responsible if your data gets lost.
                </p> <div><p>
                    The app does use third party services that may collect
                    information used to identify you.
                  </p> <p>
                    Link to privacy policy of third party service providers used
                    by the app
                  </p> <ul><li><a href=‘https://www.google.com/policies/privacy/’ target=‘_blank’ rel=‘noopener noreferrer’>Google Play Services</a></li><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----></ul></div> <p><strong>Log Data</strong></p> <p>
                  We want to inform you that whenever you
                  use our Service, in a case of an error in the app
                  I am not collect any data and information from your device.
                </p> <p><strong>Cookies</strong></p> <p>
                  Cookies are files with a small amount of data that are
                  commonly used as anonymous unique identifiers. These are sent
                  to your browser from the websites that you visit and are
                  stored on your device's internal memory.
                </p> <p>
                  This Service does not use these ‘cookies’ explicitly. However,
                  the app may use third party code and libraries that use
                  ‘cookies’ to collect information and improve their services.
                  You have the option to either accept or refuse these cookies
                  and know when a cookie is being sent to your device. If you
                  choose to refuse our cookies, you may not be able to use some
                  portions of this Service.
                </p>  
                </p> <p><strong>Security</strong></p> <p>
                  We value your trust in providing us your
                  Personal Information, thus we are striving to use commercially
                  acceptable means of protecting it. But remember that no method
                  of transmission over the internet, or method of electronic
                  storage is 100% secure and reliable, and we cannot
                  guarantee its absolute security.
                </p> <p><strong>Links to Other Sites</strong></p> <p>
                  This Service not contains any links to other sites.</p>
                <p><strong>Changes to This Privacy Policy</strong></p> <p>
                  We may update our Privacy Policy from
                  time to time. Thus, you are advised to review this page
                  periodically for any changes. We will
                  notify you of any changes by posting the new Privacy Policy on
                  this page.
                </p> <p>This policy is effective as of 2020-06-10</p> <p><strong>Contact Us</strong></p> <p>
                  If you have any questions or suggestions about our
                  Privacy Policy, do not hesitate to contact us at tidingsw@gmail.com
                </p> </body>
    </html>
      
";
    }
}