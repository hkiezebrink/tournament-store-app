﻿namespace Tournament.DataAccessLayer
{
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Windows.ApplicationModel;
    using Windows.Storage;
	using System.Collections.ObjectModel;

    /// <summary>
    /// The Dal class is used for communicating with the SQLite database. 
    /// </summary>
    public class Dal
    {
		static Dal()
		{
			_players = new ObservableCollection<Player>();
		}

        #region Fields
        // Fields for the sqlite database
        private static string DbPath
        {
            get
            {
                // If the database path is empty, create a new path to the localfolder.
                if (string.IsNullOrEmpty(dbPath))
                {
                    dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite");
                }

                return dbPath;
            }
        }
        private static string dbPath = string.Empty;
        private static ObservableCollection<Player> _players;
        #endregion

        #region Tournament Methods
        // Method to create a Tournament table
        public static void CreateDatabase()
        {
            // Create a new connection
            using (var db = new SQLiteConnection(DbPath))
            {
                // Activate Tracing
                db.Trace = true;

                // Create the table if it does not exist
                //var c = db.CreateTable<Tournament>();

                db.CreateTable<Tournament>();
                db.CreateTable<Player>();
                db.CreateTable<Fixture>();
            }
        }

		// Method to delete a tournament
        public static void DeleteTournament(Tournament tournament)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                // Activate Tracing
                db.Trace = true;

                // SQL Syntax:
				db.Execute("DELETE FROM Tournament WHERE TournamentId = ?", tournament.TournamentId);
				db.Execute("DELETE FROM Player WHERE TournamentId = ?", tournament.TournamentId);
				db.Execute("DELETE FROM Fixture WHERE TournamentId = ?", tournament.TournamentId);
            }
        }

        // Method to select all tournaments
        public static List<Tournament> GetAllTournaments()
        {
            List<Tournament> models;

            using (var db = new SQLiteConnection(DbPath))
            {
                // Activate Tracing
                db.Trace = true;

                models = (from p in db.Table<Tournament>()
                          select p).ToList();
            }

            return models;
        }

        // Method to select a specific tournament
        public static Tournament GetTournamentById(int Id)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                Tournament m = (from p in db.Table<Tournament>()
                                where p.TournamentId == Id
                                select p).FirstOrDefault();
                return m;
            }
        }

        // Method to save a tournament
        public static void SaveTournament(Tournament tournament)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                // Activate Tracing
                db.Trace = true;

                if (tournament.TournamentId == 0)
                {
                    // New
                    db.Insert(tournament);
                }
                else
                {
                    // Update
                    db.Update(tournament);
                }
            }
        }
		#endregion	

		#region Player Methods
		/// <summary>
		/// Insert player object in Player table 
		/// </summary>
		/// <param name="TournamentId"></param>
		/// <param name="player"></param>
		/// <returns></returns>
		public static int InsertPlayer(Player player)
		{
			if (Dal.GetTournamentById(player.TournamentId) != null)
			{
				using (var db = new SQLiteConnection(DbPath))
				{
					 db.Insert(player);
				}
			}
			return player.PlayerId;
		}

		/// <summary>
		/// Get all the Players from one Tournament
		/// </summary>
		/// <param name="tournamentId"></param>
		/// <returns></returns>
		public static ObservableCollection<Player> GetPlayers(int tournamentId)
		{
			using (var db = new SQLiteConnection(DbPath))
			{
				List<Player> queryResult = (from p in db.Table<Player>() where p.TournamentId == tournamentId select p).ToList();
				_players = new ObservableCollection<Player>(queryResult);
			}

			return _players;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="players"></param>
		/// <param name="tournamentId"></param>
		/// <returns></returns>
		public static ObservableCollection<Player> InsertPlayers(ObservableCollection<Player> players, int tournamentId)
		{ 
			if (Dal.GetTournamentById(tournamentId) != null)
			{
				using (var db = new SQLiteConnection(DbPath))
				{
					foreach (Player player in players)
					{
						player.TournamentId = tournamentId;
						db.Insert(player);	
					}

					List<Player> queryResult = (from p in db.Table<Player>() where p.TournamentId == tournamentId select p).ToList();
					players = new ObservableCollection<Player>(queryResult);
				}
			}
			return players;
		}

		#endregion

		#region Matches

		/// <summary>
		/// Insert a collection of Fixture object in the database
		/// </summary>
		/// <param name="fixtures"></param>
		/// <param name="tournamentId"></param>
		/// <returns></returns>
		public static bool InsertFixtures(List<Fixture> fixtures, int tournamentId)
		{
			bool success = false;
			if (Dal.GetTournamentById(tournamentId) != null)
			{
				using (var db = new SQLiteConnection(DbPath))
				{
					foreach (Fixture match in fixtures)
					{
						match.TournamentId = tournamentId;
						db.Insert(match);
					}
					success = true;
				}
			}
			return success;
		}

		/// <summary>
		/// Get all the Fixtures from one Tournament
		/// </summary>
		/// <param name="tournamentId"></param>
		/// <returns></returns>
		public static ObservableCollection<Fixture> GetFixtures(int tournamentId)
		{
			ObservableCollection<Fixture> _fixtures;
			using (var db = new SQLiteConnection(DbPath))
			{
				List<Fixture> queryResult = (from p in db.Table<Fixture>() where p.TournamentId == tournamentId select p).ToList();
				_fixtures = new ObservableCollection<Fixture>(queryResult);
			}

			return _fixtures;
		}

		/// <summary>
		/// Get all the Fixtures from one Tournament and create custom PlayersFixture object
		/// </summary>
		/// <param name="tournamentId"></param>
		/// <returns></returns>
		public static ObservableCollection<PlayersFixture> GetPlayersFixture(int tournamentId)
		{
			ObservableCollection<PlayersFixture> _playersFixture = new ObservableCollection<PlayersFixture>();

			using (var db = new SQLiteConnection(DbPath))
			{
				// Get Fixtures from one Tournament
				List<Fixture> queryResult = (from p in db.Table<Fixture>() where p.TournamentId == tournamentId select p).ToList();
				
				// Create PlayersFixture list
				foreach (Fixture item in queryResult)
				{
					// Get both Player objects
					Player p1 = (from p in db.Table<Player>() where p.PlayerId == item.PlayerOne select p).FirstOrDefault();
					Player p2 = (from p in db.Table<Player>() where p.PlayerId == item.PlayerTwo select p).FirstOrDefault();

					if (p1 != null & p2 != null)
					{
						// assign them to PlayersFixture
						_playersFixture.Add(new PlayersFixture
						{
							MatchId = item.MatchId,
							PlayerOne = p1,
							PlayerTwo = p2,
							Round = item.Round
						});		
					}
					
				}
			}
			return _playersFixture;
		}

		#endregion
	}
}
