using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldCreator.Models
{
    [Table("Achievments")]
    public class Achievment
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

         [Unique, MaxLength(30)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int BonusPoints { get; set; }
    }
}
