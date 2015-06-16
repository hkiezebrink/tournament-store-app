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

    public class Dal
    {
        private static string dbPath = string.Empty;

        // Variable for the sqlite database
        private static string DbPath
        {
            get
            {
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
                var c = db.CreateTable<Tournament>();
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
                db.Execute("DELETE FROM Tournament WHERE Id = ?", tournament.Id);
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
                                where p.Id == Id
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

                if (tournament.Id == 0)
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
    }
}
