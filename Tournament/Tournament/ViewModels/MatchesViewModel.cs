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
		private int _tournamentId;
		private string _tournamentName;
		private ObservableCollection<Fixture> _fixtures;
		private Models.Tournament _tournament;

		public MatchesViewModel()
		{
			_fixtures = new ObservableCollection<Fixture>();
			GoBackCommand = new DelegateCommand(GoBack, CanGoBack);
		}

		private void GoBack()
		{
			NavigationService.GoBack();
		}
		
		public ICommand GoBackCommand { get; private set; }

		/// <summary>
		/// Tournament name
		/// </summary>
		public string TournamentName
		{
			get
			{
				return _tournamentName;
			}
			set
			{
				if (_tournamentName != value)
				{
					_tournamentName = value;
					OnPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Bind Matches to View
		/// </summary>
		public ObservableCollection<Fixture> Fixtures
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
				_tournamentId = (int)navigationEvent.Parameter;
				_tournament = Dal.GetTournamentById(_tournamentId);
				TournamentName = _tournament.Name;
				Fixtures = Dal.GetFixtures(_tournamentId);
			}
			else
			{
				// Invalid tournament ID go home
				// NavigationService.GoHome();
			}
		}
	}
}
