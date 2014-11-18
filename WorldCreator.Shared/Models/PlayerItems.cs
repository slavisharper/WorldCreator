using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldCreator.Data
{
    [Table("PlayerItems")]
    public class PlayerItems
    {
        [Indexed]
        public int PlayerId { get; set; }

        [Indexed]
        public int ItemId { get; set; }

        public bool IsOnBoard { get; set; }
    }
}
