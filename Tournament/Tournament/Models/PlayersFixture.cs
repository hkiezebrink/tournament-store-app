using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Models
{
	/// <summary>
	/// This is not a model for the SQLite database
	/// But to combine a Player Object to a Fixture
	/// </summary>
	public class PlayersFixture
	{
		public int MatchId { get; set; }
		// Combine Fixture with player objects
		public Player PlayerOne { get; set; }	
		public Player PlayerTwo { get; set; }
		public int ScoreOne { get; set; }
		public int ScoreTwo { get; set; }
		public int Round { get; set; }

	}
}
