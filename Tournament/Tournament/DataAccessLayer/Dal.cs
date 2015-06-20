namespace Tournament.DataAccessLayer
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
        private static string dbPath = string.Empty;

		private static ObservableCollection<Player> _players;

		static Dal()
		{
			_players = new ObservableCollection<Player>();
		}

        // Variable for the sqlite database
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
            }
        }

		#region Tournament Methods
		// Method to delete a tournament
        public static void DeleteTournament(Tournament tournament)
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                // Activate Tracing
                db.Trace = true;

                // SQL Syntax:
                db.Execute("DELETE FROM Tournament WHERE Id = ?", tournament.TournamentId);
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
			//using (var db = new SQLiteConnection(DbPath))
			//{
			//	List<Player> players = (from p in db.Table<Player>() where p.TournamentId == tournamentId select p).ToList();
			//	return players;
			//}
			return _players;
		}

		#endregion
	}
}
