using Raindrops.Database;
using System;
using System.IO;

namespace Raindrops.ViweModels
{
    public class PlayerViewModel
    {
        private static PlayerDatabase PlayerDb;

        public PlayerDatabase PlayerVm
        {
            get
            {
                if (PlayerDb == null)
                {
                    PlayerDb = new PlayerDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Players.db3"));
                }
                return PlayerDb;
            }
            private set { }
        }

    }
}
