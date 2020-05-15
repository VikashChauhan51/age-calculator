using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastView : ContentView
    {
        public ToastView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
           nameof(Message),
           typeof(string),
           typeof(ToastView),
           null,
           propertyChanged: (bindable, oldV, newV) => ((ToastView)bindable).UpdateMessage((string)oldV, (string)newV));
        public string Message { get { return (string)GetValue(MessageProperty); } set { SetValue(MessageProperty, value); } }

        protected virtual void UpdateMessage(string oldV, string newV)
        {
            if (oldV != newV)
                Message = newV;
        }
    }
}