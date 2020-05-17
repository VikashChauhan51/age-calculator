using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AgeCal.Models;

namespace AgeCal.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        private object parm;
        public ItemDetailViewModel()
        {
        }
        public override async void OnNavigationParameter(object parm)
        {
            this.parm = parm;
            if (this.parm != null)
            {
                await ExecuteLoadCommand();
            }
        }

        public override void OnPageAppearing()
        {
            base.OnPageAppearing();


        }

        async Task ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                this.Item = await DataStore.GetItemAsync((string)parm);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                if (this.Item == null)
                    this.Item = new Item();
            }
        }
    }
}
