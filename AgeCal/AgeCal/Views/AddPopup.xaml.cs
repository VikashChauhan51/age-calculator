﻿using AgeCal.ViewModels;
using AgeCal.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddPopup : SemiFullPagePopup<ItemsViewModel>
    {
		public AddPopup ()
		{
			InitializeComponent();
            
		}
	}
}