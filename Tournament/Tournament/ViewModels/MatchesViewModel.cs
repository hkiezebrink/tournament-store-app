using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tournament.DataAccessLayer;
using Tournament.Models;
using Tournament.MVVM;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Tournament
{
	public class MatchesViewModel : ViewModelBase
	{
		#region Fields
		private int _tournamentId;
		private TournamentViewModel _selectedTournament;
		// Grouped matches based on round property
		private IEnumerable<IGrouping<int, PlayersFixture>> _fixtures;
		#endregion

		/// <summary>
		/// Create a MatchesViewModel object corresponding the Matches View
		/// </summary>
		public MatchesViewModel()
		{
			// set the command
			// GoBack is the function to execute when CanGoBack returns true
			GoBackCommand = new DelegateCommand(GoBack, CanGoBack);
		}

		// Command and execute function
		#region Command
		public ICommand GoBackCommand { get; private set; }

		// Command execute function
		private void GoBack()
		{
			NavigationService.GoHome();
		}	
		#endregion	

		/// <summary>
		/// Tournament type of TournamentViewModel
		/// </summary>
		public TournamentViewModel Tournament
		{
			get { return this._selectedTournament; }
			set
			{
				this.SetProperty(ref this._selectedTournament, value);
			}
		}

		/// <summary>
		/// Bind Grouped PlayersFixture to the View
		/// </summary>
		public IEnumerable<IGrouping<int, PlayersFixture>> Fixtures
		{
			get { return this._fixtures; }
			set { this.SetProperty(ref this._fixtures, value); }
		}

		/// <summary>
		/// OnNavigatedTo runs when this ViewModel is placed in the Frame object
		/// </summary>
		/// <param name="navigationEvent">TournamentId to get the players</param>
		public override void OnNavigatedTo(NavigationEventArgs navigationEvent)
		{
			if (navigationEvent.Parameter != null)
			{
				// get tournament and save its name
				Tournament = navigationEvent.Parameter as TournamentViewModel;
				_tournamentId = Tournament.Model.TournamentId;

				Tournament.PlayersFixtures = Dal.GetPlayersFixture(_tournamentId);
				// Group PlayerFixtures for the view
				Fixtures = (from f in Tournament.PlayersFixtures group f by f.Round into grp orderby grp.Key select grp);
			}
			else
			{
				// Invalid tournament ID go home
				NavigationService.GoHome();
			}
		}
	}
}
