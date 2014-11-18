using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorldCreator.Models
{
    [Table("Players")]
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Unique, MaxLength(30)]
        public string Name { get; set; }

        public int Place { get; set; }

        public int Points { get; set; }

        public int CombosCount { get; set; }

        public int HighestLevelElement { get; set; }

        public int HighestLevelCleared { get; set; }
    }
}
