using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorldCreator.Data;
using WorldCreator.Models;
using WorldCreator.ViewModels;

namespace WorldCreator.Common
{
    public class ModelParser
    {
        public static PlayerViewModel ParseToPlayerViewModel(Player player)
        {
            ApplicationDataContext data = ApplicationDataContext.Instance;
            IEnumerable<Achievment> achievments = data.GetPlayerAchievments();
            IEnumerable<AchievmentViewModel> parsedAchievments = ParseAchievments(achievments);
            PlayerViewModel parsedPlayer = new PlayerViewModel(player.Name, parsedAchievments, player.Points, player.CombosCount);
            parsedPlayer.HighestLevelCleared = player.HighestLevelCleared;
            parsedPlayer.HighestLevelElement = player.HighestLevelElement;
            return parsedPlayer;
        }

        public static GameViewModel ParseGameData(Player player)
        {
            ApplicationDataContext data = ApplicationDataContext.Instance;
            GameViewModel gameVM = new GameViewModel();

            IEnumerable<Item> rawITems = data.GetPlayerItems();
            IEnumerable<ItemViewModel> playerItems = ParseItems(rawITems);
            IEnumerable<ItemViewModel> itemsOnBoard = ParseItems(data.GetPlayerItemsOnBoard());

            gameVM.ItemsOnBoard = itemsOnBoard;
            gameVM.PlayerGroups = ParseGroups(playerItems);
            return gameVM;
        }

        public static IEnumerable<Item> ParseToItems(IEnumerable<ItemViewModel> initialModels)
        {
            List<Item> parsedItems = new List<Item>();
            foreach (var item in initialModels)
            {
                parsedItems.Add(ParseToItem(item));
            }

            return parsedItems;
        }

        public static Item ParseToItem(ItemViewModel item)
        {
            var parsedItem = new Item();
            parsedItem.GroupName = item.GroupName;
            parsedItem.IconPath = item.IconPath;
            parsedItem.Level = item.Level;
            parsedItem.Name = item.Name;
            parsedItem.X = item.X;
            parsedItem.Y = item.Y;
            return parsedItem;
        }

        private static ObservableCollection<GroupViewModel> ParseGroups(IEnumerable<ItemViewModel> items)
        {
            var groups = new Dictionary<string, ObservableCollection<ItemViewModel>>();
            foreach (var item in items)
            {
                if (!groups.ContainsKey(item.Name))
                {
                    groups[item.Name] = new ObservableCollection<ItemViewModel>();
                }

                var group = groups[item.Name];
                group.Add(item);
            }

            var values = new ObservableCollection<GroupViewModel>();
            foreach (var item in groups)
            {
                ObservableCollection<ItemViewModel> current = item.Value;
                var group = new GroupViewModel(item.Key, current);
                values.Add(group);
            }

            return values;
        }

        private static IEnumerable<AchievmentViewModel> ParseAchievments(IEnumerable<Achievment> list)
        {
            List<AchievmentViewModel> achievments = new List<AchievmentViewModel>();
            foreach (var item in list)
            {
                achievments.Add(new AchievmentViewModel(
                    item.Title, item.Description, item.BonusPoints));
            }

            return achievments;
        }

        private static IEnumerable<ItemViewModel> ParseItems(IEnumerable<Item> items)
        {
            List<ItemViewModel> parsedItems = new List<ItemViewModel>();
            foreach (var item in items)
            {
                ItemViewModel parsedItem = new ItemViewModel(item.Name, item.IconPath, item.Level);
                parsedItem.X = item.X;
                parsedItem.Y = item.Y;
                parsedItems.Add(parsedItem);
            }

            return parsedItems;
        } 
    }
}
