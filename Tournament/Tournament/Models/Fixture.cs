using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Models
{
	public class Fixture
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int MatchId { get; set; }

		/// <summary>
		/// Gets or sets the PlayerOneName.
		/// </summary>
		public string PlayerOneName { get; set; }

		/// <summary>
		/// Gets or sets the PlayerTwoName.
		/// </summary>
		public string PlayerTwoName { get; set; }

		public int ScoreOne { get; set; }
		public int ScoreTwo { get; set; }
		[NotNull]
		public int Round { get; set; }

		/// <summary>
		/// Tournament identifier
		/// </summary>
		[NotNull]
		public int TournamentId { get; set; }

	}
}