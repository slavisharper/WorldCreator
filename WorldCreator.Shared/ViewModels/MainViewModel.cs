namespace WorldCreator.ViewModels
{
    using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using WorldCreator.Common;
using WorldCreator.Data;
using WorldCreator.GameLogic;
using WorldCreator.Models;

    public class MainViewModel : BaseViewModel
    {
        private const string PlayerNameKey = "name";
        private ApplicationDataContext dataContext;
        private CombinatorEngine comboEngine;
        private PlayerViewModel currentPlayer;

        public MainViewModel()
        {
            this.dataContext = ApplicationDataContext.Instance;
            this.comboEngine = new CombinatorEngine();
            GameInitialize();
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
                IEnumerable<Item> dbItems = ModelParser.ParseToItems(initialModels);
                await this.dataContext.AddMultipleItemsAsync(dbItems);
            }

            this.LoadPlayerData(player);
        }

        private void LoadPlayerData(Player player)
        {
            this.Player = ModelParser.ParseToPlayerViewModel(player);
            this.Game = ModelParser.ParseGameData(player);
            this.IsPlayerLogged = true;
            ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            localData.Values[PlayerNameKey] = player.Name;
        }
    }
}
