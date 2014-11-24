using System;
using System.Collections.Generic;
namespace WorldCreator.ViewModels
{
    public interface IGameViewModel
    {
        void AddItemToBoard(string itemName, double x, double y);
        void AddItemToBoard(ItemViewModel item, double x, double y);
        void ChangeSelectedGroup(int index);
        void CheckForCombination(string name);
        IEnumerable<ItemViewModel> ItemsOnBoard { get; set; }
        void StartItemMove(string name);
        void StartAddingItemMove(string name);
        void MoveItemOnBoard(string name, double deltaX, double deltaY, double width, double height);
        IPlayerViewModel Player { get; set; }
        bool IsLoading { get; set; }
        IEnumerable<GroupViewModel> PlayerGroups { get; set; }
        void RemoveItem(string name);
        void RemoveItem(ItemViewModel item);
        GroupViewModel SelectedGroup { get; set; }
    }
}
