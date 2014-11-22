namespace WorldCreator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Windows.Foundation;
    using WorldCreator.Common;
    using WorldCreator.Data;
    using WorldCreator.Extensions;
    using WorldCreator.GameLogic;
    using WorldCreator.Models;

    public class GameViewModel : BaseViewModel
    {
        private ObservableCollection<ItemViewModel> itemsOnBoard;
        private ObservableCollection<GroupViewModel> groups;
        private GroupViewModel selectedGroup;
        private CombinatorEngine comboEngine;
        private ApplicationDataContext data;
        private PlayerViewModel currentPlayer;

        public GameViewModel()
            : this(new List<ItemViewModel>(), new ObservableCollection<GroupViewModel>())
        { }

        public GameViewModel(IEnumerable<ItemViewModel> itemsOnBoard, ObservableCollection<GroupViewModel> playerGroups)
        {
            this.ItemsOnBoard = itemsOnBoard;
            this.PlayerGroups = playerGroups;
            this.comboEngine = new CombinatorEngine();
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

        public PlayerViewModel Player 
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
            ItemViewModel itemToAdd = this.GetItem(itemName, this.selectedGroup.Items);
            this.AddItemToBoard(itemToAdd, x, y);
        }

        public async void AddItemToBoard(ItemViewModel item, double x, double y)
        {
            if (item != null && this.GetItem(item.Name, this.ItemsOnBoard) == null)
            {
                item.Top = y;
                item.Left = x;
                (this.ItemsOnBoard as ObservableCollection<ItemViewModel>).Add(item);
                var dbItem = ModelParser.ParseToItem(item);
                await this.data.AddItemToBoard(dbItem);
            }
        }

        public void RemoveItem(string name)
        {
            var itemToRemove = GetItem(name, this.itemsOnBoard);
            this.RemoveItem(itemToRemove);
        }

        public void RemoveItem(ItemViewModel item)
        {
            (this.ItemsOnBoard as ObservableCollection<ItemViewModel>).Remove(item);
            var dbItem = ModelParser.ParseToItem(item);
            this.data.RemoveItemFromBoard(dbItem);
        }

        internal void MoveItemOnBoard(string name, double deltaX, double deltaY, double width, double height)
        {
            var item = this.GetItem(name, this.itemsOnBoard);
            if (item == null)
            {
                return;
            }

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

        internal void CheckForCombination(string name)
        {
            var movedItem = this.GetItem(name, this.itemsOnBoard);
            foreach (var item in this.itemsOnBoard)
            {
                if (Math.Abs(item.Left - movedItem.Left) < 30 && 
                    Math.Abs(item.Top - movedItem.Top) < 30 &&
                    item != movedItem)
                {
                    this.CombineItems(item, movedItem);
                    break;
                }
            }
        }

        private void CombineItems(ItemViewModel item, ItemViewModel movedItem)
        {
            var combinedItem = this.comboEngine.PerformCombination(new Combination(item.Name, movedItem.Name));
                    if (combinedItem != null)
                    {
                        combinedItem.Left = movedItem.Left;
                        combinedItem.Top = movedItem.Top;
                        this.RemoveItem(movedItem);
                        this.RemoveItem(item);
                        this.AddItemToGroup(combinedItem);
                        this.AddItemToBoard(combinedItem, movedItem.Left, movedItem.Top);
                        this.Player.CombosCount += 1;
                        this.Player.Points += 10 * combinedItem.Level;
                        this.Player.HighestLevelElement = 
                            Math.Max(this.Player.HighestLevelElement, combinedItem.Level);
                        this.data.UpdatePlayerState(ModelParser.ParseToPlayer(this.Player));
                    }
                    else
                    {
                        // If it is not possible move back the element
                    }
        }

        private void AddItemToGroup(ItemViewModel combinedItem)
        {
            GroupViewModel group = this.GetGroup(combinedItem.GroupName);
            if (group != null)
            {
                group.Items.Add(combinedItem);
                if (group.Name == this.SelectedGroup.Name)
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
