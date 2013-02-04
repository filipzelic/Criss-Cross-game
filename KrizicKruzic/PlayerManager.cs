using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace KrizicKruzic
{
    class PlayerManager
    {
        #region Privatne varijable
        private List<Player> playerList = new List<Player>();
        private string playersFileName = "players.txt";
        #endregion

        #region Pulic properties
        public List<Player> Players
        {
            get { return playerList; }
        }
        #endregion

        #region Konstruktor i inicijalizacija
        public PlayerManager()
        {
            Load();
        }
        #endregion

        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(playersFileName))
            {
                foreach (Player player in playerList)
                {
                    string repr = player.ToString();
                    sw.WriteLine(repr);
                }
            }
        }

        public void Load()
        {
            playerList.Clear();

            if (!File.Exists(playersFileName))
            {
                return;
            }

            using (StreamReader sr = new StreamReader(playersFileName))
            {
                try
                {
                    string playerStr = String.Empty;
                    while ((playerStr = sr.ReadLine()) != null)
                    {
                        Player player = Player.FromString(playerStr);
                        playerList.Add(player);
                    }

                }
                catch (Exception excp)
                {
                    Console.WriteLine(
                        "Pogreška kod učitavanja igrača: {0}",
                        excp.Message
                        );
                }
            } //automatski poziv za close

            //StreamReader sr;
            //try
            //{
            //    sr = new StreamReader("players.txt");
            //    //koristi sr
            //}
            //catch (Exception excp)
            //{
            //    //hendlaj grešku
            //}
            //finally
            //{
            //    //zatvori
            //}
        }


        internal Player GetPlayer(string nick)
        {
            foreach (Player player in playerList)
            {
                if (player.Nick == nick)
                {
                    return player;
                }
            }

            return null;
        }

        internal Player AddPlayer(string nick)
        {
            if (GetPlayer(nick) != null)
            {
                string message = String.Format(
                    "Igrač s nadimkom {0} već postoji!",
                    nick);
                throw new Exception(message);
            }
            
            Player player = new Player(nick);
            playerList.Add(player);

            Save();

            return player;
        }
    }
}
