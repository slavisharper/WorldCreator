namespace WorldCreator.ViewModels
{
    using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using WorldCreator.Common;
using WorldCreator.Data;
using WorldCreator.GameLogic;
using WorldCreator.Models;

    public class MainViewModel
    {
        private const string PlayerNameKey = "name";
        private ApplicationDataContext dataContext;
        private CombinatorEngine comboEngine;
        public MainViewModel()
        {
            this.dataContext = ApplicationDataContext.Instance;
            this.comboEngine = new CombinatorEngine();
            GameInitialize();
        }

        public PlayerViewModel Player { get; set; }

        public bool IsPlayerLogged { get; set; }

        public GameViewModel Game { get; set; }

        private async void GameInitialize()
        {
            ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            string playerName = localData.Values[PlayerNameKey] as string;
            if (playerName == null)
            {
                // TO DO: Get player name and initialize with initial data
                await this.ChangePlayer("Unnamed");
            }
            else
            {
                // TO DO: Loading screen
                await this.LoadPlayer(playerName);
            }
        }

        private async Task LoadPlayer(string playerName)
        {
            Player player = await this.dataContext.LoadPlayer(playerName);
            this.LoadPlayerData(player);
        }

        private async Task ChangePlayer(string playerName)
        {
            Player player = await this.dataContext.LoadPlayer(playerName);
            if (player == null)
            {
                player = await this.dataContext.LoadInitialPlayer(playerName);
                IEnumerable<ItemViewModel> initialModels = CombinationsGetter.BasicItems;
            }

            this.LoadPlayerData(player);
        }

        private void LoadPlayerData(Player player)
        {
            this.Player = ModelParser.ParseFullPlayerData(player, dataContext);
            this.Game = ModelParser.ParseGameData(dataContext);
            this.IsPlayerLogged = true;
            ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            localData.Values[PlayerNameKey] = player.Name;
        }
    }
}
