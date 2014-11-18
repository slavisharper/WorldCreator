namespace WorldCreator.ViewModels
{
    using Windows.Storage;
    using WorldCreator.Common;
    using WorldCreator.Data;
    using WorldCreator.Models;

    public class MainViewModel
    {
        private const string PlayerNameKey = "name";
        private ApplicationDataContext dataContext;

        public MainViewModel()
        {
            ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            this.dataContext = new ApplicationDataContext();
            string playerName = localData.Values[PlayerNameKey] as string;
            if (playerName == null)
            {
                // TO DO: Get player name and initialize with initial data
                this.ChangePlayer("Unnamed");
            }
            else
            {
                // TO DO: Loading screen
                this.LoadPlayer(playerName);
            }
        }

        public PlayerViewModel Player { get; set; }

        public bool IsPlayerLogged { get; set; }

        public GameViewModel Game { get; set; }

        private async void LoadPlayer(string playerName)
        {
            Player player = await this.dataContext.LoadPlayer(playerName);
            this.Player =  await ModelParser.ParseFullPlayerData(player, dataContext);
            this.Game = await ModelParser.ParseGameData(dataContext);
            this.IsPlayerLogged = true;
            ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            localData.Values[PlayerNameKey] = playerName;
        }

        private async void ChangePlayer(string playerName)
        {
            Player player = await this.dataContext.LoadPlayer(playerName);
            if (player == null)
            {
                player = await this.dataContext.LoadInitialPlayer(playerName);
            }

            this.Player =  await ModelParser.ParseFullPlayerData(player, dataContext);
            //this.Game = await ModelParser.ParseGameData(dataContext);
            //this.IsPlayerLogged = true;
            //ApplicationDataContainer localData = ApplicationData.Current.LocalSettings;
            //localData.Values[PlayerNameKey] = playerName;
        }
    }
}
