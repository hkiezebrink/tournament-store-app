using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

namespace Tournament
{
	public class ViewBase : Page
	{
		protected override void OnNavigatedTo(NavigationEventArgs navigationEvent)
		{
			var viewModel = DataContext as ViewModelBase;
			if (viewModel != null)
			{
				viewModel.OnNavigatedTo(navigationEvent);
			} 
		}
	}
}
