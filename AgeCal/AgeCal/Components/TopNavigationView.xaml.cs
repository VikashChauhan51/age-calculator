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
    public partial class TopNavigationView : BaseTopNavigationView
    {
        public TopNavigationView()
        {
            InitializeComponent();
            RenderBackIcon(HasBackButton);

        }
        protected override void UpdateTitle(string oldV, string newV)
        {
            base.UpdateTitle(oldV, newV);
            TitleLabel.Text = newV;
        }
        protected override void UpdateSubTitle(string oldV, string newV)
        {
            base.UpdateSubTitle(oldV, newV);
           // SubTitleLabel.Text = newV;
        }
        protected override void UpdateTitleIcon(string oldV, string newV)
        {
            base.UpdateTitleIcon(oldV, newV);
        }
     
        protected override void UpdateHasBackButton(bool oldV, bool newV)
        {
            base.UpdateHasBackButton(oldV, newV);
            RenderBackIcon(newV);
        }
        public override void Dispose()
        {
            base.Dispose();
            BackIcon.Clicked -= OnBackClick;
        }

        protected void OnBackClick(object sender, EventArgs args)
        {
            ButtonPressed?.Execute(Core.AgeNavigationType.BACK);

        }
        void RenderBackIcon(bool visibility)
        {
            BackIcon.IsVisible = visibility;
        }
    }
}