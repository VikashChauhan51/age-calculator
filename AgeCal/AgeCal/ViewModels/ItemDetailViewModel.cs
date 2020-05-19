using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AgeCal.Interfaces;
using AgeCal.Models;

namespace AgeCal.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public User Item { get; set; }
        private object parm;
        private readonly IUserRepository _userRepository;
        public ItemDetailViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public override void OnNavigationParameter(object parm)
        {
            this.parm = parm;
            if (this.parm != null)
            {
                ExecuteLoadCommand();
            }
        }

        public override void OnPageAppearing()
        {
            base.OnPageAppearing();


        }

        void ExecuteLoadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                this.Item = _userRepository.Get((string)parm);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                if (this.Item == null)
                    this.Item = new User();
            }
        }

       
    }
}
