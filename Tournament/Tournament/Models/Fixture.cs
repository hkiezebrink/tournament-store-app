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
		[NotNull]
		public int MatchId { get; set; }

		/// <summary>
		/// Gets or sets the PlayerOneName.
		/// </summary>
		public int PlayerOne { get; set; }

		/// <summary>
		/// Gets or sets the PlayerTwoName.
		/// </summary>
		public int PlayerTwo { get; set; }

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