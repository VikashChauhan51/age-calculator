using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AgeCal.ViewModels
{
    public class ValidationBase // : BindableBase, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public ValidationBase()
        {
            ErrorsChanged += ValidationBase_ErrorsChanged;
        }

        private void ValidationBase_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            // OnPropertyChanged("HasErrors");
            // OnPropertyChanged("ErrorsList");
        }

       

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (_errors.ContainsKey(propertyName) && (_errors[propertyName].Any()))
                {
                    return _errors[propertyName].ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
            else
            {
                return _errors.SelectMany(err => err.Value.ToList()).ToList();
            }
        }

        public bool HasErrors
        {
            get
            {
                return _errors.Any(propErrors => propErrors.Value.Any());
            }
        }

        //protected virtual void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        //{
        //    var validationContext = new ValidationContext(this, null)
        //    {
        //        MemberName = propertyName
        //    };

        //    var validationResults = new List<ValidationResult>();
        //    Validator.TryValidateProperty(value, validationContext, validationResults);
        //    RemoveErrorsByPropertyName(propertyName);

        //    HandleValidationResults(validationResults);
        //}
    }
}
