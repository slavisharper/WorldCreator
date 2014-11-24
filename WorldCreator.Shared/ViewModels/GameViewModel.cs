namespace WorldCreator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using Windows.Foundation;
    using WorldCreator.Audio;
    using WorldCreator.Common;
    using WorldCreator.Data;
    using WorldCreator.Extensions;
    using WorldCreator.GameLogic;
    using WorldCreator.Models;

    public class GameViewModel : BaseViewModel, IGameViewModel
    {
        private ObservableCollection<ItemViewModel> itemsOnBoard;
        private ObservableCollection<GroupViewModel> groups;
        private GroupViewModel selectedGroup;
        private CombinatorEngine comboEngine;
        private ApplicationDataContext data;
        private IPlayerViewModel currentPlayer;
        private IScoreManager scoreManager;
        private ItemViewModel currentMovedItem;
        private ItemViewModel currentAddedItem;
        private ISoundPlayer soundPlayer;
        private double startedMoveLeft;
        private double startedMoveTop;
        private Animator animator;
        private bool gameIsLoading;

        public GameViewModel()
            : this(new List<ItemViewModel>(), new ObservableCollection<GroupViewModel>(),
                   new CombinatorEngine(), new ParseConnection())
        { }

        public GameViewModel(IEnumerable<ItemViewModel> itemsOnBoard, IEnumerable<GroupViewModel> playerGroups,
                             CombinatorEngine engine, IScoreManager scoreManager)
        {
            this.ItemsOnBoard = itemsOnBoard;
            this.PlayerGroups = playerGroups;
            this.comboEngine = engine;
            this.scoreManager = scoreManager;
            this.InitializePlayer();
            this.animator = new Animator();
            this.data = ApplicationDataContext.Instance;
        }

        public GroupViewModel SelectedGroup
        {
            get
            {
                return this.selectedGroup;
            }
            set
            {
                this.selectedGroup = value;
                this.OnPropertyChanged("SelectedGroup");
            }
        }

        public IPlayerViewModel Player
        {
            get { return this.currentPlayer; }
            set
            {
                this.currentPlayer = value;
                this.OnPropertyChanged("Player");
            }
        }

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

        public void ChangeSelectedGroup(int index)
        {
            this.SelectedGroup = (this.PlayerGroups as ObservableCollection<GroupViewModel>)[index];
        }

        public void AddItemToBoard(string itemName, double x, double y)
        {
            if (this.currentAddedItem.Name != null && this.currentAddedItem.Name == itemName)
            {
                this.AddItemToBoard(this.currentAddedItem, x, y);
            }
        }
        
        // TO DO: add added sound
        public async void AddItemToBoard(ItemViewModel item, double x, double y)
        {
            if (item != null)
            {
                var element = this.GetItem(item.Name, this.ItemsOnBoard);
                if (element == null)
                {
                    item.Top = y;
                    item.Left = x;
                    (this.ItemsOnBoard as ObservableCollection<ItemViewModel>).Add(item);
                    var dbItem = ModelParser.ParseToItem(item);
                    await this.data.AddItemToBoard(dbItem);
                }
                else
                {
                    this.animator.MoveItem(element, x, y);
                }

                item.IsSelected = false;
            }
        }

        public bool IsLoading
        {
            get { return this.gameIsLoading; }
            set
            {
                this.gameIsLoading = value;
                this.OnPropertyChanged("IsLoading");
            }
        }

        public void RemoveItem(string name)
        {
            var itemToRemove = GetItem(name, this.itemsOnBoard);
            this.RemoveItem(itemToRemove);
        }

        // TO DO: add removed sound
        public void RemoveItem(ItemViewModel item)
        {
            (this.ItemsOnBoard as ObservableCollection<ItemViewModel>).Remove(item);
            var dbItem = ModelParser.ParseToItem(item);
            this.data.RemoveItemFromBoard(dbItem);
        }

        public void MoveItemOnBoard(string name, double deltaX, double deltaY, double width, double height)
        {
            if (this.currentMovedItem == null)
            {
                return;
            }

            if (name == this.currentMovedItem.Name)
            {
                this.MoveItem(this.currentMovedItem, deltaX, deltaY, width, height);
            }
        }

        // TO DO: add grabbed sound
        public void StartAddingItemMove(string name)
        {
            var movingItem = this.GetItem(name, this.SelectedGroup.Items);
            if (movingItem != null)
            {
                movingItem.IsSelected = true;    
            }

            this.currentAddedItem = movingItem;
        }

        public void StartItemMove(string name)
        {
            var movingItem = this.GetItem(name, this.itemsOnBoard);
            this.startedMoveLeft = movingItem.Left;
            this.startedMoveTop = movingItem.Top;
            this.currentMovedItem = movingItem;
        }

        public void CheckForCombination(string name)
        {
            bool isCombinationPerformed = false;
            if (this.currentMovedItem != null && name == this.currentMovedItem.Name)
            {
                foreach (var item in this.itemsOnBoard)
                {
                    if (Math.Abs(item.Left - this.currentMovedItem.Left) < 30 &&
                        Math.Abs(item.Top - this.currentMovedItem.Top) < 30 &&
                        item != this.currentMovedItem)
                    {
                        this.CombineItems(item, this.currentMovedItem);
                        isCombinationPerformed = true;
                        break;
                    }
                }
            }
            if (!isCombinationPerformed)
            {
                this.data.UpdateItemPositionAsync(ModelParser.ParseToItem(this.currentMovedItem));
            }
        }

        // TO DO: add combined sound
        private void CombineItems(ItemViewModel item, ItemViewModel movedItem)
        {
            var combinedItem = this.comboEngine.PerformCombination(new Combination(item.Name, movedItem.Name));
            if (combinedItem != null)
            {
                var group = this.GetGroup(combinedItem.GroupName);
                var isExisting = group != null && this.GetItem(combinedItem.Name, group.Items) != null;
                combinedItem.Left = movedItem.Left;
                combinedItem.Top = movedItem.Top;
                this.RemoveItem(movedItem);
                this.RemoveItem(item);
                this.AddItemToGroup(combinedItem);
                this.AddItemToBoard(combinedItem, movedItem.Left, movedItem.Top);
                if (!isExisting)
                {
                    this.Player.CombosCount += 1;
                    this.Player.Points += 10 * combinedItem.Level;
                    this.Player.HighestLevelElement =
                        Math.Max(this.Player.HighestLevelElement, combinedItem.Level);
                    this.data.UpdatePlayerState(ModelParser.ParseToPlayer(this.Player));
                    this.Player.UpdateScore();
                }
            }
            else
            {
                this.animator.MoveItem(movedItem, this.startedMoveLeft, this.startedMoveTop);
            }
        }

        private void AddItemToGroup(ItemViewModel combinedItem)
        {
            GroupViewModel group = this.GetGroup(combinedItem.GroupName);
            if (group != null)
            {
                if (GetItem(combinedItem.Name, group.Items) == null)
                {
                    group.Items.Add(combinedItem);
                }

                if (this.SelectedGroup != null && group.Name == this.SelectedGroup.Name)
                {
                    this.OnPropertyChanged("SelectedGroup");
                    this.SelectedGroup = this.selectedGroup;
                }
            }
            else
            {
                group = new GroupViewModel(combinedItem.GroupName, new List<ItemViewModel>() { combinedItem });
                (this.PlayerGroups as ObservableCollection<GroupViewModel>).Add(group);
            }

        }

        private void MoveItem(ItemViewModel item, double deltaX, double deltaY, double width, double height)
        {
            item.Left += deltaX;
            item.Top += deltaY;

            if (item.Left < 0)
            {
                item.Left = 0;
            }
            else if (item.Left > (width - 100))
            {
                item.Left = width - 100;
            }

            if (item.Top < 0)
            {
                item.Top = 0;
            }
            else if (item.Top > (height - 100))
            {
                item.Top = height - 100;
            }
        }

        private void InitializePlayer()
        {
            this.soundPlayer = new SoundPlayer();
        }

        private GroupViewModel GetGroup(string name)
        {
            foreach (var group in this.PlayerGroups)
            {
                if (group.Name.ToLower() == name.ToLower())
                {
                    return group;
                }
            }

            return null;
        }

        private ItemViewModel GetItem(string itemName, IEnumerable<ItemViewModel> items)
        {
            foreach (var item in items)
            {
                if (item.Name.ToLower() == itemName.ToLower())
                {
                    return item;
                }
            }

            return null;
        }
    }
}
