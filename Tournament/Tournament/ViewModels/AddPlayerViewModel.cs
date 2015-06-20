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

namespace Tournament.ViewModels
{
	class AddPlayerViewModel : BindableBase
	{
		private string _playerName;
		private ObservableCollection<Player> _players;

		/// <summary>
		/// ViewModel constructor
		/// </summary>
		public AddPlayerViewModel()
		{
			GetPlayersCommand = new DelegateCommand(GetPlayers);
			InsertPlayerCommand = new DelegateCommand(InsertPlayer);

			_players = new ObservableCollection<Player>();
			_playerName = String.Empty;
			//_players.Add(new Player {Name = "Harrie", TournamentId = 4});

			// Create database
			Dal.CreateDatabase();
		}

		public ObservableCollection<Player> Players
		{
			get { return _players; }
		}

		public string PlayerName { 
			get 
			{
				return _playerName;
			}
			set
			{
				_playerName = value;
				OnPropertyChanged();
			}
		}
		
		#region Action Commands

		public ICommand GetPlayersCommand { get; private set; }
		public ICommand InsertPlayerCommand { get; private set; }

		#endregion
		
		#region Action Methods
		
		private void GetPlayers()
		{
			_players = Dal.GetPlayers(1);
			OnPropertyChanged("Players");
		}

		public void InsertPlayer()
		{
			if (String.IsNullOrWhiteSpace(PlayerName) || PlayerName.Length > 36)
			{
				return;
			}
			Player player = new Player {Name = this.PlayerName.Trim(), TournamentId = 1};

			_players.Add(player);
			// Empty PlayerName
			Dal.InsertPlayer(player);
			PlayerName = String.Empty;

			OnPropertyChanged("Players");
		}
		
		#endregion

	}
}
