using System;
using System.Collections.Generic;
using System.Text;

namespace WorldCreator.ViewModels
{
    public class GameViewModel
    {
        public IEnumerable<ItemViewModel> ItemsOnBoard { get; set; }

        public IEnumerable<GroupViewModel> PlayerGroups { get; set; }
    }
}
