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
            RenderBackButton(HasBackButton);

        }
        protected override void UpdateTitle(string oldV, string newV)
        {
            base.UpdateTitle(oldV, newV);
            TitleLabel.Text = newV;
        }
        protected override void UpdateSubTitle(string oldV, string newV)
        {
            base.UpdateSubTitle(oldV, newV);
            SubTitleLabel.Text = newV;
        }
        protected override void UpdateTitleIcon(string oldV, string newV)
        {
            base.UpdateTitleIcon(oldV, newV);
        }
        void RenderBackButton(bool visible)
        {

        }
        protected override void UpdateHasBackButton(bool oldV, bool newV)
        {
            base.UpdateHasBackButton(oldV, newV);
        }
        public override void Dispose()
        {
            base.Dispose();
        }

        protected void OnBackClick(object sender, EventArgs args)
        {
            ButtonPressed?.Execute(Core.AgeNavigationType.BACK);

        }
    }
}