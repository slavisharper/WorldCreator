namespace WorldCreator.Data
{
    using SQLite;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorldCreator.Models;
    using System.Linq;

    public class ApplicationDataContext
    {
        private const string GameDatabaseName = "wordlcreator.db";
        private SQLiteAsyncConnection connection;
        private Player currentPlayer;
        private static ApplicationDataContext instance;

        private ApplicationDataContext()
        {
            this.connection = new SQLiteAsyncConnection(GameDatabaseName);
        }

        public static ApplicationDataContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationDataContext();
                }

                return instance;
            }
        }

        public async Task<Player> LoadPlayer(string playerName)
        {
            await this.CreateTables();
            var query = this.connection.Table<Player>().Where(p => p.Name.Equals(playerName));
            var players = await query.ToListAsync();
            if (players.Count < 1)
            {
                return null;
            }
            else
            {
                this.currentPlayer = players[0];
                return currentPlayer;
            }
        }

        public async Task<Player> LoadInitialPlayer(string playerName)
        {
            await this.CreateTables();
            var player = new Player();
            player.Name = playerName;
            player.Points = 0;
            player.HighestLevelCleared = 0;
            player.HighestLevelElement = 0;
            player.Place = -1;
            await connection.InsertAsync(player).ContinueWith((p) => {
                player.ID = p.Id;
                this.currentPlayer = player;
            });

            return player;
        }

        public async void AddItem(Item item)
        {
            var query = this.connection.Table<Item>().Where(i => i.Name == item.Name);
            var dbItems = await query.ToListAsync();
            Item dbItem;
            if (dbItems.Count < 1)
	        {
                await this.connection.InsertAsync(item);
                dbItem = await query.FirstAsync();
	        }
            else
            {
                dbItem = dbItems[0];
            }
            
            this.InsertPlayerItem(dbItem.ID, currentPlayer.ID, true);
        }

        public void AddMultipleItems(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                this.AddItem(item);
            }
        }

        public async void AddAchievment(Achievment achievment)
        {
            var query = this.connection.Table<Achievment>().Where(a => a.Title == achievment.Title);
            Achievment dbAchievment;
            var dbAchievments = await query.ToListAsync();
            if (dbAchievments.Count < 1 )
            {
                await this.connection.InsertAsync(achievment);
                dbAchievment = await query.FirstAsync();
            }
            else
            {
                dbAchievment = dbAchievments[0];
            }

            this.InsertPlayerAchievment(dbAchievment.ID, currentPlayer.ID);
        }

        public async void AddItemToBoard(Item item)
        {
            var query = this.connection.Table<Item>().Where(i => i.Name == item.Name);
            Item dbItem = await query.FirstAsync();
            var itemQuery = this.connection.Table<PlayerItems>()
                .Where(pi => pi.ItemId == dbItem.ID && currentPlayer.ID == pi.PlayerId);
            PlayerItems playerItem = await itemQuery.FirstAsync();
            if (playerItem == null)
            {
                playerItem = new PlayerItems();
                playerItem.IsOnBoard = true;
                playerItem.ItemId = dbItem.ID;
                playerItem.PlayerId = currentPlayer.ID;
                await this.connection.InsertAsync(playerItem);
            }
            else
            {
                playerItem.IsOnBoard = true;
                await this.connection.UpdateAsync(playerItem);
            }

        }

        public async void RemoveItemFromBoard(Item item)
        {
            var query = this.connection.Table<Item>().Where(i => i.Name == item.Name);
            Item dbItem = await query.FirstAsync();
            var itemQuery = this.connection.Table<PlayerItems>()
                .Where(pi => pi.ItemId == dbItem.ID && currentPlayer.ID == pi.PlayerId);
            PlayerItems playerItem = await itemQuery.FirstAsync();
            if (playerItem != null)
            {
                playerItem.IsOnBoard = false;
                await this.connection.UpdateAsync(playerItem);
            }
        }

        public IEnumerable<Achievment> GetPlayerAchievments()
        {
            var query = this.connection.QueryAsync<Achievment>("select A.* from PlayerAchievments PA " + 
                                                                   "join Achievments A " +
                                                                   "on AchievmentId = A.ID " + 
                                                                   "where PA.PlayerId = " + currentPlayer.ID);
            var achievments = query.Result;
            return achievments;
        }

        public IEnumerable<Item> GetPlayerItems()
        {
            var query = this.connection.QueryAsync<Item>("select I.* from PlayerItems PI " +
                                                                   "join Items I " +
                                                                   "on ItemId = I.ID " +
                                                                   "where PI.PlayerId = " + currentPlayer.ID);
            var items = query.Result;
            return items;
        }

        public IEnumerable<Item> GetPlayerItemsOnBoard()
        {
            var query = this.connection.QueryAsync<Item>("select I.* from PlayerItems PI " +
                                                                   "join Items I " +
                                                                   "on ItemId = I.ID " +
                                                                   "where PI.PlayerId = " + currentPlayer.ID +
                                                                   " and PI.IsOnBoard = 1");
            var items = query.Result;
            return items;
        }

        public async void UpdatePlayerState(Player player)
        {
            this.currentPlayer.HighestLevelCleared = player.HighestLevelCleared;
            this.currentPlayer.HighestLevelElement = player.HighestLevelElement;
            this.currentPlayer.Points = player.Points;
            this.currentPlayer.CombosCount = player.CombosCount;
            await this.connection.UpdateAsync(currentPlayer);
        }

        private void InsertPlayerAchievment(int achievmentId, int playerId)
        {
            PlayerAchievments achievment = new PlayerAchievments();
            achievment.PlayerId = playerId;
            achievment.AchievmentId = achievmentId;
            this.connection.InsertAsync(achievment);
        }

        private void InsertPlayerItem(int itemId, int playerId, bool isOnBoard)
        {
            PlayerItems playerItems = new PlayerItems();
            playerItems.IsOnBoard = isOnBoard;
            playerItems.ItemId = itemId;
            playerItems.PlayerId = playerId;
            this.connection.InsertAsync(playerItems);
        }

        private async Task CreateTables()
        {
            await this.connection.CreateTablesAsync<Achievment, Item, Player, PlayerItems, PlayerAchievments>();
        }
    }
}
