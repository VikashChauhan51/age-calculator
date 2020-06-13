using AgeCal.ViewModels;
using Newtonsoft.Json.Linq;
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
             
            ContactWebView.RegisterAction(data => 
            {
                
                try
                {
                  var result=  JObject.Parse(data);
                    if (result!=null)
                        Device.OpenUri(new Uri(result["value"].ToString()));
                }
                catch  
                {

                    
                }
                
            });
            
        }

 
    }
}