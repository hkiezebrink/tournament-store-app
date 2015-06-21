using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Tournament.Models
{
    /// <summary>
    /// This is the model of the MVVM. 
    /// </summary>
    public class Tournament
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int TournamentId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <remarks>Is an enum in the viewmodel.</remarks>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        /// <remarks>Is a blob in the database.</remarks>
        public byte[] Picture { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }
    }
}
