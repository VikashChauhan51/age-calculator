using AgeCal.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.ViewModels
{
    public class AddViewModel : BaseViewModel
    {

        public ExclusiveRelayCommand AddCommand { get; set; }
        private string id;
        public AddViewModel()
        {
            AddCommand = new ExclusiveRelayCommand(Add);

        }
        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        DateTime dob = DateTime.Now;
        public DateTime DOB
        {
            get { return dob; }
            set
            {
                dob = value;
                RaisePropertyChanged(nameof(DOB));
            }
        }
        TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        public void Add()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    Task.Run(async () => await DataStore.AddItemAsync(new Models.Item { Text = Title, Description = Description, Id = Guid.NewGuid().ToString() }));
                    NavigationService.GoBackModel(new Core.Toast { Message = "Saved" });
                }
            }
            catch (Exception ex)
            {


            }
            finally
            {
                IsBusy = false;
            }


        }

        public override void OnNavigationParameter(object parm)
        {
            if (parm != null && parm is string)
                id = (string)parm;

        }
    }
}
