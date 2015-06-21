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
		private int _tournamentId;
		private ObservableCollection<Player> _players;

		/// <summary>
		/// ViewModel constructor
		/// </summary>
		public AddPlayerViewModel()
		{
			GenerateScheduleCommand = new DelegateCommand(GenerateSchedule, CanGenerateSchedule);
			InsertPlayerCommand = new DelegateCommand(InsertPlayer);
			GoBackCommand = new DelegateCommand(GoBack, CanGoBack);
			_players = new ObservableCollection<Player>();
			_playerName = String.Empty;

			//Dal.CreateDatabase();
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

		private void GoBack()
		{
			NavigationService.GoBack();
		}

		private async void GenerateSchedule()
		{
			bool result = false;

			MessageDialog message = new MessageDialog("Do you wish to leave this page?");
			message.Commands.Add(new UICommand("OK", new UICommandInvokedHandler((cmd) => result = true)));
			message.Commands.Add(new UICommand("Cancel"));
			await message.ShowAsync();

			if (result)
			{
				NavigationService.GoHome();
			}
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
			Player player = new Player {Name = this.PlayerName.Trim(), TournamentId = _tournamentId};

			_players.Add(player);
			// Empty PlayerName
			// Dal.InsertPlayer(player);
			PlayerName = String.Empty;
			OnPropertyChanged("Players");
		}
		
		#endregion

		/// <summary>
		/// Get players from one Tournament 
		/// and update the binding Players to show in the AddPlayers view
		/// </summary>
		/// <param name="tournamentId"></param>
		private void GetPlayersData(int tournamentId)
		{
			// _players = Dal.GetPlayers(tournamentId);
			// OnPropertyChanged("Players");
		}

		/// <summary>
		/// OnNavigatedTo runs when this ViewModel is placed in the Frame object
		/// </summary>
		/// <param name="navigationEvent">TournamentId to get the players</param>
		public override void OnNavigatedTo(NavigationEventArgs navigationEvent)
		{
			_tournamentId = (int)navigationEvent.Parameter;
			//GetPlayersData(_tournamentId);
		}
	}
}
