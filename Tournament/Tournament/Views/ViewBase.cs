using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

namespace Tournament
{
	/// <summary>
	/// The ViewBase is the layer beneath each View
	/// Every View needs to extend this ViewBase to get use of the 
	/// OnNavigatedTo (in the corresponding ViewModel)
	/// And NavigationService
	/// </summary>
	public class ViewBase : Page
	{
		protected override void OnNavigatedTo(NavigationEventArgs navigationEvent)
		{
			// get the viewModel (set in the XAML page)
			var viewModel = DataContext as ViewModelBase;
			if (viewModel != null)
			{
				// pass the navigation event to the viewModel
				viewModel.OnNavigatedTo(navigationEvent);
			} 
		}

		// Extern code to make the NavigationService work in the ViewModel
		#region DataContextChanged handling
		// Workaround for no DataContextChanged event in WinRT
		// Set a binding for this dependency property to an empty binding and hook up a change callback handler
		// The change callback handler becomes the equivalent of a DataContextChanged event since the property will be set each time the DataContext changes
		public static readonly DependencyProperty DataContextChangedWatcherProperty =
			DependencyProperty.Register("DataContextChangedWatcher", typeof(object), typeof(ViewBase),
			new PropertyMetadata(null, OnDataContextChanged));

		public object DataContextChangedWatcher
		{
			get { return (object)GetValue(DataContextChangedWatcherProperty); }
			set { SetValue(DataContextChangedWatcherProperty, value); }
		}

		private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var vm = ((ViewBase)d).DataContext as ViewModelBase;
			if (vm != null) vm.NavigationService = NavigationService.Current;
		}

		public ViewBase()
		{
			BindingOperations.SetBinding(this, DataContextChangedWatcherProperty, new Binding());
		}

		#endregion
	}
}
