using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raindrops.Models
{
    [Table("Player")]
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int Score { get; set; }
    }
}
