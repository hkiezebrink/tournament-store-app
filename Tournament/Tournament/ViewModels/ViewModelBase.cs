using System;
using Tournament.MVVM;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Tournament
{
    /// <summary>
    /// The base model for each Viewmodel. It uses the BindableBase for easy binding to the View and the use of PropertyChanged events.
	/// This ViewModelBase uses the NavigationService which activates navigation in every ViewModel
    /// </summary>
    public class ViewModelBase : BindableBase
    {
		// ResourceLoader for loading strings form te Resourse file
		protected ResourceLoader rl;
		public ViewModelBase()
		{
			rl = new ResourceLoader();
		}

		// This OnNavigatedTo Method needs to be implemented in the ViewModel
		public virtual void OnNavigatedTo(NavigationEventArgs navigationEvent) { }
		// NavigationService that can be used inside the ViewModel
		public INavigationService NavigationService { get; set; }

		/// <summary>
		/// Data Property to determine if the page should show the back button
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Show a MessageDialog with a choice (two buttons)
		/// When pressed OK run the callback function
		/// </summary>
		/// <param name="message">Message of the dialog</param>
		/// <param name="title">Title of the dialog</param>
		/// <param name="callbackFunction">The callback function of type Action</param>
		protected async void Message(string message, string title, Action callbackFunction)
		{
			bool result = false;

			MessageDialog dialog = new MessageDialog(message, title);
			dialog.Commands.Add(new UICommand("OK", new UICommandInvokedHandler((cmd) => result = true)));
			dialog.Commands.Add(new UICommand("Cancel"));
			await dialog.ShowAsync();

			if (result)
			{
				callbackFunction();
			}
		}
		/// <summary>
		/// Show a MessageDialog with one button
		/// </summary>
		/// <param name="message">Message of the dialog</param>
		/// <param name="title">Title of the dialog</param>
		protected async void Message(string message, string title)
		{
			MessageDialog dialog = new MessageDialog(message, title);
			dialog.Commands.Add(new UICommand("OK"));
			await dialog.ShowAsync();
		}
	}
}