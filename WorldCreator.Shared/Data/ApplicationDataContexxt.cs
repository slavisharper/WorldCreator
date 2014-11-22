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
                    CreateTables(instance);
                }

                return instance;
            }
        }

        private static void CreateTables(ApplicationDataContext instance)
        {
            var result = instance.connection.CreateTablesAsync<Achievment, Item, Player, PlayerAchievments>().Result;
        }

        public async Task<IEnumerable<string>> PlayerNames()
        {
            int count = await this.connection.Table<Player>().CountAsync();
            if (count < 1)
            {
                return new List<string>();
            }
            else
            {
                var players = await this.connection.Table<Player>().ToListAsync();
                var names = players.Select(p => p.Name);
                return names;
            }
        }

        public async Task<Player> LoadPlayer(string playerName)
        {
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
            var player = new Player();
            player.Name = playerName;
            player.Points = 0;
            player.HighestLevelCleared = 0;
            player.HighestLevelElement = 0;
            await connection.InsertAsync(player);
            this.currentPlayer = await connection.Table<Player>()
                .Where(p => p.Name == playerName).FirstOrDefaultAsync();
            return player;
        }

        public async Task AddItemAsync(Item item)
        {
            var query = this.connection.Table<Item>()
                .Where(i => i.Name == item.Name && i.PlayerId == currentPlayer.ID);
            var dbItem = await query.FirstOrDefaultAsync();
            if (dbItem == null)
	        {
                item.PlayerId = currentPlayer.ID;
                await this.connection.InsertAsync(item);
	        }
        }

        public async Task AddMultipleItemsAsync(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                await this.AddItemAsync(item);
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

        public async Task AddItemToBoard(Item item)
        {
            var query = this.connection.Table<Item>()
                .Where(i => i.Name == item.Name && i.PlayerId == currentPlayer.ID);
            Item dbItem = await query.FirstOrDefaultAsync();
            if (dbItem == null)
            {
                await this.AddItemAsync(item);
                dbItem = await query.FirstOrDefaultAsync();
            }

            dbItem.IsOnBoard = true;
            dbItem.X = item.X;
            dbItem.Y = item.Y;
            await this.connection.UpdateAsync(dbItem);
        }

        public async Task AddMultipleItemsToBoard(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                await this.AddItemToBoard(item);
            }
        }

        public async void RemoveItemFromBoard(Item item)
        {
            var query = this.connection.Table<Item>()
                .Where(i => i.Name == item.Name && i.PlayerId == currentPlayer.ID);
            Item dbItem = await query.FirstAsync();
            if (dbItem != null)
            {
                dbItem.IsOnBoard = false;
                await this.connection.UpdateAsync(dbItem);
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

        public async Task<IEnumerable<Item>> GetPlayerItems()
        {
            var items = await this.connection.Table<Item>()
                .Where(i => i.PlayerId == currentPlayer.ID)
                .ToListAsync();
            return items;
        }

        public async Task<List<Item>> GetPlayerItemsOnBoard()
        {
            var items = await this.connection.Table<Item>()
                .Where(i => i.PlayerId == currentPlayer.ID && i.IsOnBoard == true)
                .ToListAsync();
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

        private async void CreateTables()
        {
            await this.connection.CreateTablesAsync<Achievment, Item, Player, PlayerAchievments>();
        }
    }
}
