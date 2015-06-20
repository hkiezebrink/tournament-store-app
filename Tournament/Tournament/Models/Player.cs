using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Models
{
	public class Player
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int PlayerId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[MaxLength(64), Unique]
		public string Name { get; set; }

		/// <summary>
		/// Tournament identifier
		/// </summary>
		[Indexed]
		public int TournamentId { get; set; }

	}
}
