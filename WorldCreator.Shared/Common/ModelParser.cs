using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldCreator.Data;
using WorldCreator.Models;
using WorldCreator.ViewModels;

namespace WorldCreator.Common
{
    public class ModelParser
    {
        public static async Task<PlayerViewModel> ParseFullPlayerData(Player player, ApplicationDataContext data)
        {
            IEnumerable<Achievment> achievments = await data.GetPlayerAchievments();
            IEnumerable<AchievmentViewModel> parsedAchievments = ParseAchievments(achievments);
            PlayerViewModel parsedPlayer = new PlayerViewModel(player.Name, parsedAchievments, player.Points, player.CombosCount);
            parsedPlayer.HighestLevelCleared = player.HighestLevelCleared;
            parsedPlayer.HighestLevelElement = player.HighestLevelElement;
            return parsedPlayer;
        }

        public static async Task<GameViewModel> ParseGameData(ApplicationDataContext data)
        {
            // TO DO
            GameViewModel gameVM = new GameViewModel();

            IEnumerable<Item> playerItems = await data.GetPlayerItems();

            return gameVM;
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

        private IEnumerable<ItemViewModel> ParseItems(IEnumerable<Item> items)
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
