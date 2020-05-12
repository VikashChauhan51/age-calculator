using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Core
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseStyle : ResourceDictionary
	{
		public BaseStyle ()
		{
			InitializeComponent ();
		}
	}
}