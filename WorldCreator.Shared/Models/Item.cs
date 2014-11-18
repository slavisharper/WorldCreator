using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldCreator.Models
{
    [Table("Items")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        [Unique, MaxLength(30)]
        public string Name { get; set; }

        public string IconPath { get; set; }

        public int Level { get; set; }

        public string GroupName { get; set; }
    }
}
