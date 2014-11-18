using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldCreator.Models
{
    [Table("PlayerChievments")]
    public class PlayerAchievments
    {
        [Indexed]
        public int PlayerId { get; set; }

        [Indexed]
        public int AchievmentId { get; set; }
    }
}
