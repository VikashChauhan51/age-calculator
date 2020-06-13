﻿using AgeCal.ViewModels;
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
    public partial class TermsPage : AgeContentPage<TermsViewModel>
    {
        public TermsPage()
        {
            InitializeComponent();
            PageTitle = "Terms & Conditions";
            ShowBottomNav = false;
            PageHasbackButton = true;
            var html = new HtmlWebViewSource
            {
                Html = terms
            };
            TermsWebView.Source = html;
        }

        private static string terms = @"<!DOCTYPE html>
    <html>
    <head>
      <meta charset='utf-8'>
      <meta name='viewport' content='width=device-width'>
      <title>Terms &amp; Conditions</title>
      <style> body { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; padding:1em; } </style>
    </head>
    <body>
    <strong>Terms &amp; Conditions</strong> <p>
                  By downloading or using the app, these terms will
                  automatically apply to you – you should make sure therefore
                  that you read them carefully before using the app. You’re not
                  allowed to copy, or modify the app, any part of the app, or
                  our trademarks in any way. You’re not allowed to attempt to
                  extract the source code of the app, and you also shouldn’t try
                  to translate the app into other languages, or make derivative
                  versions. The app itself, and all the trade marks, copyright,
                  database rights and other intellectual property rights related
                  to it, still belong to Tidings team.
                </p> <p>
                  Tidings team is committed to ensuring that the app is
                  as useful and efficient as possible. For that reason, we
                  reserve the right to make changes to the app or to charge for
                  its services, at any time and for any reason. We will never
                  charge you for the app or its services without making it very
                  clear to you exactly what you’re paying for.
                </p> <p>
                  The Tiding app stores and processes personal data that
                  you have provided to us, in order to provide my
                  Service. It’s your responsibility to keep your phone and
                  access to the app secure. We therefore recommend that you do
                  not jailbreak or root your phone, which is the process of
                  removing software restrictions and limitations imposed by the
                  official operating system of your device. It could make your
                  phone vulnerable to malware/viruses/malicious programs,
                  compromise your phone’s security features and it could mean
                  that the Tiding app won’t work properly or at all.
                </p> <div><p>
                    The app does use third party services that declare their own
                    Terms and Conditions.
                  </p> <p>
                    Link to Terms and Conditions of third party service
                    providers used by the app
                  </p> <ul><li><a href='https://policies.google.com/terms' target='_blank' rel='noopener noreferrer'>Google Play Services</a></li><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----><!----></ul></div> <p>
                  You should be aware that there are certain things that
                  Tidings team will not take responsibility for. 
                </p> <p></p>  
				<p>
                  Along the same lines, Tidings team cannot always take
                  responsibility for the way you use the app i.e. You need to
                  make sure that your device stays charged – if it runs out of
                  battery and you can’t turn it on to avail the Service,
                  Tidings team cannot accept responsibility.
                </p> <p>
                  With respect to Vikash’s responsibility for your
                  use of the app, when you’re using the app, it’s important to
                  bear in mind that although we endeavour to ensure that it is
                  updated and correct at all times, we do rely on third parties
                  to provide information to us so that we can make it available
                  to you. Tidings team accepts no liability for any
                  loss, direct or indirect, you experience as a result of
                  relying wholly on this functionality of the app.
                </p> <p>
                  At some point, we may wish to update the app. The app is
                  currently available on Android – the requirements for
                  system(and for any additional systems we
                  decide to extend the availability of the app to) may change,
                  and you’ll need to download the updates if you want to keep
                  using the app. Tidings team does not promise that it
                  will always update the app so that it is relevant to you
                  and/or works with the Android version that you have
                  installed on your device. However, you promise to always
                  accept updates to the application when offered to you, We may
                  also wish to stop providing the app, and may terminate use of
                  it at any time without giving notice of termination to you.
                  Unless we tell you otherwise, upon any termination, (a) the
                  rights and licenses granted to you in these terms will end;
                  (b) you must stop using the app, and (if needed) delete it
                  from your device.
                </p> <p><strong>Changes to This Terms and Conditions</strong></p> <p>
                  I may update our Terms and Conditions
                  from time to time. Thus, you are advised to review this page
                  periodically for any changes. I will
                  notify you of any changes by posting the new Terms and
                  Conditions on this page.
                </p> <p>
                  These terms and conditions are effective as of 2020-06-10
                </p> <p><strong>Contact Us</strong></p> <p>
                  If you have any questions or suggestions about my
                  Terms and Conditions, do not hesitate to contact me
                  at tidingsw@gmail.com.
                </p>  
    </body>
    </html>
      ";
    }
}