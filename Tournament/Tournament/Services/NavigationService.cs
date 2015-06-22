using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Tournament
{
	/// <summary>
	/// NavigationService to use Navigation methods in the ViewModel 
	/// instead of the code-behind of the XAML
	/// Code from: online pluralsight video tutorial
	/// </summary>
	public class NavigationService : INavigationService
	{
		private static NavigationService _Instance;

		public static INavigationService Current
		{
			get
			{
				if (_Instance == null) _Instance = new NavigationService();
				return _Instance;
			}
		}

		public static Frame Frame { get; set; }

		public static string ViewNamespace { get; set; }

		public void GoHome()
		{
			if (Frame != null)
			{
				while (Frame.CanGoBack) Frame.GoBack();
			}
		}

		public void GoBack()
		{
			if (Frame != null && Frame.CanGoBack) Frame.GoBack();
		}

		public void GoForward()
		{
			if (Frame != null && Frame.CanGoForward) Frame.GoForward();
		}

		public void Navigate(string pageName)
		{
			Navigate(pageName, null);
		}

		public void Navigate(string pageName, object parameter)
		{
			string viewTypeName;
			if (string.IsNullOrEmpty(ViewNamespace)) viewTypeName = String.Format("{0}.{1}", typeof(NavigationService).Namespace, pageName);
			else viewTypeName = String.Format("{0}.{1}", ViewNamespace, pageName);
			var viewType = Type.GetType(viewTypeName);
			Frame.Navigate(viewType, parameter);
		}

		public bool CanGoBack
		{
			get { return Frame.CanGoBack; }
		}

		public bool CanGoForward
		{
			get { return Frame.CanGoForward; }
		}

	}
}
