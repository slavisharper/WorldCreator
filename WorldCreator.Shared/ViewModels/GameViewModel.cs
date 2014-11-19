namespace WorldCreator.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using WorldCreator.Extensions;
    using WorldCreator.GameLogic;

    public class GameViewModel
    {
        private ObservableCollection<ItemViewModel> itemsOnBoard;
        private ObservableCollection<GroupViewModel> groups;
        private GroupViewModel selectedGroup;
        private CombinatorEngine comboEngine;

        public GameViewModel()
        { }

        public GameViewModel(IEnumerable<ItemViewModel> itemsOnBoard, ObservableCollection<GroupViewModel> playerGroups)
        {
            this.ItemsOnBoard = itemsOnBoard;
            this.PlayerGroups = playerGroups;
            this.comboEngine = new CombinatorEngine();
        }

        public GroupViewModel SelectedGroup { get; set; }

        public IEnumerable<ItemViewModel> ItemsOnBoard
        {
            get 
            {
                if (this.itemsOnBoard == null)
                {
                    this.ItemsOnBoard = new ObservableCollection<ItemViewModel>();
                }

                return this.itemsOnBoard;
            }
            set
            {
                if (this.itemsOnBoard == null)
                {
                    this.itemsOnBoard = new ObservableCollection<ItemViewModel>();
                }

                this.itemsOnBoard.Clear();
                this.itemsOnBoard.AddRange(value);
            }
        }

        public IEnumerable<GroupViewModel> PlayerGroups 
        {
            get
            {
                if (this.groups == null)
                {
                    this.PlayerGroups = new ObservableCollection<GroupViewModel>();
                }

                return this.groups;
            }
            set
            {
                if (this.groups == null)
                {
                    this.groups = new ObservableCollection<GroupViewModel>();
                }

                this.groups.Clear();
                this.groups.AddRange(value);
            }
        }
    }
}
