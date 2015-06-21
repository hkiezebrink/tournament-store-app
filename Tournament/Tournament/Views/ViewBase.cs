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
