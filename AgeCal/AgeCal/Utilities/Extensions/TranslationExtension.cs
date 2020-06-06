using AgeCal.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal
{
    [ContentProperty("Text")]
    public class TranslationExtension : IMarkupExtension
    {
        public TranslationExtension()
        {

        }
        public string Text { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;
            return BundleResourceManager.Translate(Text);
        }
    }
}
