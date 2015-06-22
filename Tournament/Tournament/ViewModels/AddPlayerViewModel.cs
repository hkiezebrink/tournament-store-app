using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Tournament.DataAccessLayer;
using Tournament.Models;
using Tournament.MVVM;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Tournament
{
	class AddPlayerViewModel : ViewModelBase
	{
		private string _playerName;
		private TournamentViewModel _selectedTournament;
		private ObservableCollection<Player> _players;
		private int _tournamentId;

		/// <summary>
		/// AddPlayerViewModel constructor
		/// </summary>
		public AddPlayerViewModel()
		{
			GenerateScheduleCommand = new DelegateCommand(GenerateSchedule, CanGenerateSchedule);
			InsertPlayerCommand = new DelegateCommand(InsertPlayer);
			GoBackCommand = new DelegateCommand(AskGoBack, CanGoBack);
			_players = new ObservableCollection<Player>();
			_playerName = String.Empty;
		}

		#region Data Properties

		/// <summary>
		/// Players property with the players of one tournament
		/// </summary>
		public ObservableCollection<Player> Players
		{
			get { return _players; }
		}

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
		/// PlayerName property for new players
		/// set: only set when modified and run change event
		/// </summary>
		public string PlayerName { 
			get 
			{
				return _playerName;
			}
			set
			{
				if (_playerName != value)
				{
					_playerName = value;
					OnPropertyChanged();
				}
			}
		}
		
		#endregion

		#region Action Commands

		public ICommand InsertPlayerCommand { get; private set; }
		public ICommand GenerateScheduleCommand { get; private set; }
		public ICommand GoBackCommand { get; private set; }

		#endregion

		#region Action States

		private bool CanGenerateSchedule()
		{
			return true; 
		}

		#endregion

		#region Action Methods

		private void AskGoBack()
		{
			Message(rl.GetString("LeaveAddPlayers"), rl.GetString("LeaveAddPlayersTitle"), GoBack);
		}

		private void GoBack()
		{
			NavigationService.GoBack();
		}

		public void ExecuteMe()
		{
			NavigationService.GoHome();
		}

		private void GenerateSchedule()
		{
			// total players must be greater than one to generate a schedule
			if (Tournament.Players.Count < 2)
			{
				Message(rl.GetString("GenerateScheduleError"), rl.GetString("GenerateScheduleErrorTitle"));
				return;
			}

			// Save player objects in the database
			Dal.InsertPlayers(Tournament.Players, _tournamentId);
			CreateFixtures();
			NavigationService.Navigate("Matches", _tournamentId);
			// TODO Generate schedule
			/* Maak een tijdelijk lijst met Player objecten
			 * en zodra het aantal van deze lijst groter is dan 1
			 * voeg dan alle objecten toe in de database en genereer alle wedstrijden
			 * voeg de wedstrijden toe aan de Match model
			 * 
			 * Navigeer nu naar de matches pagina en stuur de tournamentId mee
			 */
		}

		public void InsertPlayer()
		{
			if (String.IsNullOrWhiteSpace(PlayerName) || PlayerName.Length > 36)
			{
				return;
			}
			Player player = new Player { Name = this.PlayerName.Trim() };

			Tournament.Players.Add(player);

			PlayerName = String.Empty;
			OnPropertyChanged("Players");
		}
		
		#endregion

		/// <summary>
		/// Create new matches for each round 
		/// </summary>
		/// <param name="tournament"></param>
		private void CreateFixtures()
		{
			// get the players of the tournament
			List<Player> players = Tournament.Players.ToList<Player>();
			// use the full name because of System.Text.RegularExpressions.Match class
			List<Fixture> fixtures = new List<Fixture>();

			if ((players.Count % 2) != 0)
			{
				// uneven player count
				players.Add(new Player { Name = null });
			}

			// Pak het eerste team
			// Deze zal niet wijzigen per ronde
			Player firstTeam = players[0];
			// Vervolgens pakken we het tweede team
			// deze zal wijzigen per ronde
			Player secondTeam = players[1];

			// Als er maar twee teams zijn voeg de wedstrijd toe en verlaat de functie
			if (players.Count == 2)
			{
				fixtures.Add(new Fixture
				{
					MatchId = 1,
					PlayerOne = firstTeam.PlayerId,
					PlayerTwo = secondTeam.PlayerId,
					Round = 1
				});
			}
			else
			{
				List<Player> upperTeams = new List<Player>();
				List<Player> bottomTeams = new List<Player>();
				// Bereken de lengte van de tijdelijke lists
				// dit betekent de rest van de list (1e en 2e overslaan), delen door twee
				// aangezien we de overige teams moeten verdelen over twee lists
				int listLength = (players.Count - 2) / 2;

				// Op basis van de list lengte kunnen we de teams toevoegen
				// teams		=	[P1, P2, P3, P4, P5, P6 P7, P8, P9, P10]
				// upperTeams	=	[P3, P4, P5, P6]
				upperTeams.AddRange(players.Skip(2).Take(listLength));
				// bottomTeams	=	[P10, P9, P8, P7]
				bottomTeams.AddRange(players.Skip(2 + listLength).Take(listLength).Reverse());

				int matchId = 1;
				// Loop voor elke ronde
				// Aantal teams - 1 is het aantal ronden (elk team speelt niet tegen zichzelf)
				for (int round = 1; round < players.Count; round++)
				{
					// Wedstrijden aanmaken
					fixtures.Add(new Fixture
					{
						MatchId = matchId++,
						PlayerOne = firstTeam.PlayerId,
						PlayerTwo = secondTeam.PlayerId,
						Round = round
					});
					// bepaal de overige wedstrijden per ronde
					// voor elk team in de tijdelijke arrays
					for (int i = 0; i < listLength; i++)
					{
						// upperTeam	=	[P3, P4, enz.
						// bottomTeam	=	[P10, P9, enz.
						// De wedstrijd wordt dus (P3 vs p10) (P4 vs P9) enz.
						fixtures.Add(new Fixture
						{
							MatchId = matchId++,
							PlayerOne = upperTeams[i].PlayerId,
							PlayerTwo = bottomTeams[i].PlayerId,
							Round = round
						});
					}

					// Nadat de wedstrijden voor een ronde zijn aangemaakt
					// moeten we de round-robin toepassen op de tijdelijke lijsten en het tweede team

					// secondTeam gaat naar upperTeam [0]
					upperTeams.Insert(0, secondTeam);
					// secondTeam wordt bottomTeam [0]
					secondTeam = bottomTeams[0];
					// verwijder het eerste item van bottomTeam (wat nu secondTeam is)
					bottomTeams.RemoveAt(0);
					// voeg het laatste item van upperTeam toe achteraan bottomTeam
					bottomTeams.Add(upperTeams[listLength]);
					// verwijder de laatste item van upperTeam (welke nu achteraan bottomTeam staat)
					upperTeams.RemoveAt(listLength);
				}
			}

			

			//TODO Add Matches to Dal
			Dal.InsertFixtures(fixtures, _tournamentId);
			// TODO create PlayersFixture object and assign to SelectedTournament 
		}

		/// <summary>
		/// OnNavigatedTo runs when this ViewModel is placed in the Frame object
		/// Needs a tournamentId as Parameter from navigationEventArgs
		/// </summary>
		/// <param name="navigationEvent">TournamentId to get the players</param>
		public override void OnNavigatedTo(NavigationEventArgs navigationEvent)
		{
			if (navigationEvent.Parameter != null)
			{
				// get tournament and save its name
				_selectedTournament = navigationEvent.Parameter as TournamentViewModel;
				_tournamentId = _selectedTournament.Model.TournamentId;
			}
			else
			{
				// Invalid tournament ID go home
				NavigationService.GoHome();
			}
		}

	}
}
