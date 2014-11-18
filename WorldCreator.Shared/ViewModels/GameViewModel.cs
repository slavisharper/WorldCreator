namespace WorldCreator.ViewModels
{
    using System.Collections.Generic;
    using WorldCreator.Extensions;

    public class GameViewModel
    {
        private IList<ItemViewModel> itemsOnBoard;
        private IList<GroupViewModel> groups;

        public GameViewModel()
        { }

        public GameViewModel(IEnumerable<ItemViewModel> itemsOnBoard, IEnumerable<GroupViewModel> playerGroups)
        {
            this.ItemsOnBoard = itemsOnBoard;
            this.PlayerGroups = playerGroups;
        }

        public GroupViewModel SelectedGroup { get; set; }

        public IEnumerable<ItemViewModel> ItemsOnBoard
        {
            get { return this.itemsOnBoard; }
            set
            {
                if (this.itemsOnBoard == null)
                {
                    this.itemsOnBoard = new List<ItemViewModel>();
                }

                this.itemsOnBoard.Clear();
                this.itemsOnBoard.AddRange(value);
            }
        }

        public IEnumerable<GroupViewModel> PlayerGroups 
        {
            get { return this.groups; }
            set
            {
                if (this.groups == null)
                {
                    this.groups = new List<GroupViewModel>();
                }

                this.groups.Clear();
                this.groups.AddRange(value);
            }
        }
    }
}
