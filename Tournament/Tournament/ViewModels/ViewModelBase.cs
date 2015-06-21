using Tournament.MVVM;
using Windows.UI.Xaml.Navigation;

namespace Tournament
{
    /// <summary>
    /// The base model for each model. It uses the BindableBase for easy binding to the View and the use of PropertyChanged events.
	/// This ViewModelBase uses the NavigationService which activates navigation in every ViewModel
    /// </summary>
    public class ViewModelBase : BindableBase
    {
		public virtual void OnNavigatedTo(NavigationEventArgs navigationEvent) { }

		public INavigationService NavigationService { get; set; }
		public bool CanGoBack()
		{
			if (NavigationService != null)
			{
				return NavigationService.CanGoBack;
			}
			else
			{ 
				return false;
			} 
		}
		//public virtual bool CanGoForward
		//{
		//	get
		//	{
		//		if (NavigationService != null) return NavigationService.CanGoForward;
		//		else return false;
		//	}
		//}
	}
}