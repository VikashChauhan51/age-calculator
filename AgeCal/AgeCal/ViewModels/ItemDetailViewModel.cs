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
        public override void OnNavigationParameter(object parm)
        {
            this.parm = parm;
        }

        public override void OnPageAppearing()
        {
            base.OnPageAppearing();
            Task.Run(async () => await ExecuteLoadCommand());

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
