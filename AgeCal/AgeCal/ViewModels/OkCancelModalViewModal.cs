using AgeCal.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AgeCal.ViewModels
{
    public class OkCancelModalParameter
    {
        public string Title { get; set; }
        public IEnumerable<string> Messages { get; set; }
        public bool EnabledCancel { get; set; }
    }
    public class OkCancelModalViewModal : BaseViewModel
    {

        public ExclusiveRelayCommand OkCommand { get; protected set; }
        public ExclusiveRelayCommand CancelCommand { get; protected set; }
        public OkCancelModalViewModal()
        {
            OkCommand = new ExclusiveRelayCommand(Ok);
            CancelCommand = new ExclusiveRelayCommand(Cancel);
        }
        string modelTitle = string.Empty;
        public string ModelTitle
        {
            get { return modelTitle; }
            set
            {
                modelTitle = value;

            }
        }
        string okLabel = string.Empty;
        public string OkLabel
        {
            get { return okLabel; }
            set
            {
                okLabel = value;

            }
        }
        string cancelLabel = string.Empty;
        public string CancelLabel
        {
            get { return cancelLabel; }
            set
            {
                cancelLabel = value;

            }
        }
        ObservableCollection<string> message;
        public ObservableCollection<string> Messages
        {
            get { return message; }
            set
            {
                message = value;

            }
        }

        protected void Ok()
        {
            Exit();
        }
        protected void Cancel()
        {
            Exit();

        }
        protected virtual void Exit()
        {
            NavigationService.GoBackModel();
        }

    }
}
