using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Raindrops.Models;
using SQLite;

namespace Raindrops.Database
{
    public class PlayerDatabase
    {
        private readonly SQLiteAsyncConnection PlayerDB;
        public PlayerDatabase(string dbPath)
        {
            PlayerDB = new SQLiteAsyncConnection(dbPath);
            PlayerDB.CreateTableAsync<Player>().Wait();
        }

        public async Task<List<Player>> GetAllInformationAsync()
        {
            return await PlayerDB.Table<Player>().ToListAsync();
        }
        public async Task<int> InsertScore(Player player)
        {
            return await PlayerDB.InsertAsync(player);
        }


    }
}
