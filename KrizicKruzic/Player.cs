using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KrizicKruzic
{
    class Player
    {
        public Player(string nick)
        {
            this.Nick = nick;
        }

        public Player(string nick, int played, int won, int draw)
        {
            this.Nick = nick;
            this.GamesPlayed = played;
            this.GamesWon = won;
            this.GamesDraw = draw;
        }

        public string Nick { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesDraw { get; set; }

        public int GamesLost {
            get { return GamesPlayed - GamesWon - GamesDraw; }
        }

        public char Icon { get; set; }

        internal static Player FromString(string playerStr)
        {
            if (playerStr == null)
            {
                throw new ArgumentNullException("playerStr");
            }

            string[] playerData = playerStr.Split('\t');
            if (playerData.Length != 4)
            {
                string message = String.Format(
                    "playerStr: not enough data in '{0}'",
                    playerStr);
                throw new ArgumentException(message);
            }

            string nick = playerData[0];
            int played = 0;
            Int32.TryParse(playerData[1], out played);
            int won = 0;
            Int32.TryParse(playerData[2], out won);
            int draw = 0;
            Int32.TryParse(playerData[3], out draw);

            return new Player(nick, played, won, draw);
        }

        public override string ToString()
        {
            return String.Format("{0}\t{1}\t{2}\t{3}", 
                Nick, GamesPlayed, GamesWon, GamesDraw);
        }

    }
}
