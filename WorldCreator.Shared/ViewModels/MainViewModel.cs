namespace WorldCreator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Windows.Storage;
    using WorldCreator.Common;
    using WorldCreator.Extensions;
    using WorldCreator.Data;
    using WorldCreator.GameLogic;
    using WorldCreator.Models;
    using System.Windows.Input;
    using WorldCreator.Commands;
    using Windows.UI.Popups;
    using Windows.UI.Xaml.Controls;
    using System.Net.Http;

    public class MainViewModel : BaseViewModel
    {
        private const string PlayerNameKey = "name";
        private ApplicationDataContext dataContext;
        private IGameViewModel game;
        private ApplicationDataContainer localSettings;
        private ObservableCollection<string> playerNames;
        private ICommand commandLogUser;
        private ICommand commandCreateUser;
        private bool isLoading;
        private bool isLogged;

        public MainViewModel()
        {
            this.dataContext = ApplicationDataContext.Instance;
            this.localSettings = ApplicationData.Current.LocalSettings;
            this.IsLoading = false;
            this.IsPlayerLogged = false;
            ProfileInitialize();
        }

        public bool IsPlayerLogged 
        {
            get { return this.isLogged; }
            set
            {
                this.isLogged = value;
                this.OnPropertyChanged("IsPlayerLogged");
            }
        }

        public bool IsLoading 
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                this.OnPropertyChanged("IsLoading");
            }
        }

        public IEnumerable<string> PlayerNames
        {
            get
            {
                if (this.playerNames == null)
                {
                    this.PlayerNames = new ObservableCollection<string>();
                }

                return this.playerNames; 
            }
            set
            {
                if (this.playerNames == null)
                {
                    this.playerNames = new ObservableCollection<string>();
                }

                this.playerNames.Clear();
                this.playerNames.AddRange(value);
            }
        }

        public IGameViewModel Game 
        {
            get 
            { 
                return this.game; 
            }
            set
            {
                this.game = value;
                this.OnPropertyChanged("Game");
            }
        }

        public ICommand CreateNewPlayer 
        {
            get
            {
                if (this.commandCreateUser == null)
                {
                    this.commandCreateUser = new RelayCommandWithParameter(this.PerformCreatePlayer);
                }

                return commandCreateUser;
            }
        }

        public ICommand LogExisitnigPlayer
        {
            get
            {
                if (this.commandLogUser == null)
                {
                    this.commandLogUser = new RelayCommandWithParameter(this.PerformLogPlayer);
                }

                return commandLogUser;
            }
        }

        private async void PerformLogPlayer(object obj)
        {
            this.IsLoading = true;
            string name = obj as string;
            if (name == null || name.Length < 3)
            {
                // TO DO Show alert
            }
            else
            {
                await this.LoadPlayer(name);
                this.IsLoading = false;
                this.IsPlayerLogged = true;
            }
        }

        private async void PerformCreatePlayer(object obj)
        {
            this.IsLoading = true;
            string name = obj as string;
            bool isParseUnreacable = false;
            try
            {
                await ValidatePlayer(name);
            }
            catch (HttpRequestException he)
            {
                isParseUnreacable = true;
            }
            catch (Parse.ParseException pe)
            {
                isParseUnreacable = true;
            }

            if (isParseUnreacable)
	        {
		        await this.ShowMessage("Networ error!", "You cannot create new user in offline mode.");
	        }
        }

        private async Task ValidatePlayer(string name)
        {
            if (name == null || name.Length < 3 || name.Length > 20)
            {
                await this.ShowMessage("Name must beetween 3 and 20 symbols.", "Error! Invalid name.");
                this.IsLoading = false;
            }
            else if (await ParseConnection.CheckIfNameIsTaken(name))
            {
                await this.ShowMessage("This name is already taken.", "Error! Invalid name.");
                this.IsLoading = false;
            }
            else
            {
                await this.ChangePlayer(name);
                this.playerNames.Add(name);
                this.IsLoading = false;
                this.IsPlayerLogged = true;
            }
        }

        private async void ProfileInitialize()
        {
            string playerName = this.localSettings.Values[PlayerNameKey] as string;
            this.PlayerNames = await this.dataContext.PlayerNames();
            IsPlayerLogged = false;
        }

        private async Task LoadPlayer(string playerName)
        {
            Player player = await this.dataContext.LoadPlayer(playerName);
            await this.LoadPlayerData(player);
            await this.ShowMessage("Now you can continue your world creation..", "Player logged successfully!.");
        }

        private async Task ShowMessage(string title, string message)
        {
            MessageDialog dialog = new MessageDialog(title, message);
            await dialog.ShowAsync();
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
                await this.dataContext.AddMultipleItemsToBoard(dbItems);
            }

            await this.LoadPlayerData(player);
        }

        private async Task LoadPlayerData(Player player)
        {
            this.Game = await ModelParser.ParseGameData(player);
            this.IsPlayerLogged = true;
            this.localSettings.Values[PlayerNameKey] = player.Name;
        }
    }
}
