using Tournament.MVVM;
using Windows.UI.Xaml.Navigation;

namespace Tournament.ViewModels
{
    /// <summary>
    /// The base model for each model. It uses the BindableBase for easy binding to the View and the use of PropertyChanged events.
	/// This ViewModelBase uses the NavigationService which activates navigation in every ViewModel
    /// </summary>
    public class ViewModelBase : BindableBase
    {
		public virtual void OnNavigatedTo(NavigationEventArgs navigationEvent) { }
	}
}