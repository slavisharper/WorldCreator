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

        public async static Task<GameViewModel> ParseGameData(Player player)
        {
            var playerVM = ParseToPlayerViewModel(player);
            ApplicationDataContext data = ApplicationDataContext.Instance;
            GameViewModel gameVM = new GameViewModel();

            IEnumerable<Item> rawITems = await data.GetPlayerItems();
            IEnumerable<ItemViewModel> playerItems = ParseItems(rawITems);
            IEnumerable<Item> dbItemsOnBoard = await data.GetPlayerItemsOnBoard();
            IEnumerable<ItemViewModel> itemsOnBoard = ParseItems(dbItemsOnBoard);

            gameVM.Player = playerVM;
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
            parsedItem.X = item.Left;
            parsedItem.Y = item.Top;
            return parsedItem;
        }

        private static ObservableCollection<GroupViewModel> ParseGroups(IEnumerable<ItemViewModel> items)
        {
            var groups = new Dictionary<string, ObservableCollection<ItemViewModel>>();
            foreach (var item in items)
            {
                if (!groups.ContainsKey(item.GroupName))
                {
                    groups[item.GroupName] = new ObservableCollection<ItemViewModel>();
                }

                var group = groups[item.GroupName];
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
                parsedItem.Left = item.X;
                parsedItem.Top = item.Y;
                parsedItem.GroupName = item.GroupName;
                parsedItems.Add(parsedItem);
            }

            return parsedItems;
        }

        internal static Player ParseToPlayer(PlayerViewModel p)
        {
            var player = new Player();
            player.CombosCount = p.CombosCount;
            player.HighestLevelCleared = p.HighestLevelCleared;
            player.HighestLevelElement = p.HighestLevelElement;
            player.Name = p.Name;
            player.Points = p.Points;
            return player;
        }
    }
}
