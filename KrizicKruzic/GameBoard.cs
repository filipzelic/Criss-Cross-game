using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KrizicKruzic
{
    class GameBoard
    {
        #region Privatne varijable
        private Player playerOne;
        private Player playerTwo;
        //1 - playerOne, 2 - playerTwo
        private int currentPlayer = 1;
        #endregion

        /// <summary>
        /// Sadrži podatke o trenutnoj igri
        /// 0 | 1 | 2
        /// 3 | 4 | 5
        /// 6 | 7 | 8
        /// 
        /// Mjesta 0-8 su popunjena znakovima:
        ///   ' ' (space)   - nepopunjeno polje
        ///   'x'           - odigran križić
        ///   'o'           - odigran kružić
        /// </summary>
        private char[] gameData = new char[9];

        public GameBoard(Player playerOne, Player playerTwo)
        {
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;

            for (int i = 0; i < gameData.Length; i++)
            {
                gameData[i] = ' ';
            }
        }

        internal void PlayGame()
        {
            playerOne.GamesPlayed += 1;
            playerTwo.GamesPlayed += 1;

            playerOne.Icon = 'x';
            playerTwo.Icon = 'o';
            while (!GameFinished())
            {
                DrawGame();
                PlayOneMove();
            }

            //tko je pobjednik
            DrawGame();
            Console.WriteLine();
            if (WonPlayer(playerOne))
            {
                Console.WriteLine("Bravo! Pobijedio je {0}", playerOne.Nick);
                playerOne.GamesWon += 1;
            }
            else if (WonPlayer(playerTwo))
            {
                Console.WriteLine("Bravo! Pobijedio je {0}", playerTwo.Nick);
                playerTwo.GamesWon += 1;
            }
            else
            {
                Console.WriteLine("Nema pobjednika!");
                playerOne.GamesDraw += 1;
                playerTwo.GamesDraw += 1;
            }
        }

        private void DrawGame()
        {
            string divider = "-+-+-";
            Console.WriteLine();
            Console.WriteLine("{0}|{1}|{2}", gameData[0], gameData[1], gameData[2]);
            Console.WriteLine(divider);
            Console.WriteLine("{0}|{1}|{2}", gameData[3], gameData[4], gameData[5]);
            Console.WriteLine(divider);
            Console.WriteLine("{0}|{1}|{2}", gameData[6], gameData[7], gameData[8]);
        }

        private void PlayOneMove()
        {
            Player activePlayer = GetActivePlayer();

            while (true)
            {
                Console.Write(activePlayer.Nick + " (1-9, 0 za kraj): ");
                string input = Console.ReadLine();

                int pos = -1;
                if (Int32.TryParse(input, out pos))
                {
                    if (pos == 0)
                    {
                        //TODO: kraj igre
                        return;
                    }
                    else if (pos < 0 || pos > 9)
                    {
                        Console.WriteLine("Nemoguća pozicija {0}!", pos);
                    }
                    else if (gameData[pos - 1] == ' ')
                    {
                        gameData[pos - 1] = activePlayer.Icon;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Pozicija {0} je već igrana!", pos);
                    }
                }
                else
                {
                    Console.WriteLine("Ne razumijem '{0}', pokušajte ponovo!", input);
                }
            }
        }

        private Player GetActivePlayer()
        {
            if (currentPlayer == 1)
            {
                currentPlayer = 2;
                return playerOne;
            }
            else
            {
                currentPlayer = 1;
                return playerTwo;
            }
        }

        private bool GameFinished()
        {
            //igra je gotova ako
            //a) je neki redak sastavljen od istih znakova (x ili o)
            //b) je neki stupac sastavljen od istih znakova (x ili o)
            //c) je neka od dijagonala sastavljena od istih znakova (x ili o)
            //d) sva polja su popunjena a nema pobjednika

            bool wonFirst = WonPlayer(playerOne);
            bool wonSecond = WonPlayer(playerTwo);
            bool draw = PlayerDraw();

            return wonFirst || wonSecond || draw;
        }

        //igra je nerješena ako nema pobjednika i 
        //sva polja su popunjena
        private bool PlayerDraw()
        {
            if (WonPlayer(playerOne) || WonPlayer(playerTwo))
            {
                return false;
            }

            for (int i = 0; i < gameData.Length; i++)
            {
                if (gameData[i] == ' ')
                {
                    return false;
                }
            }

            return true;
        }

        private bool WonPlayer(Player player)
        {
            bool won = false;
            won |= CheckRow(0, player.Icon);
            won |= CheckRow(1, player.Icon);
            won |= CheckRow(2, player.Icon);

            won |= CheckColumn(0, player.Icon);
            won |= CheckColumn(1, player.Icon);
            won |= CheckColumn(2, player.Icon);

            won |= CheckDiagonals(playerOne.Icon);

            return won;
        }

        private bool CheckDiagonals(char icon)
        {
            bool leftDiagonal = 
                gameData[0] == icon && 
                gameData[4] == icon && 
                gameData[8] == icon;

            bool rightDiagonal =
                gameData[2] == icon &&
                gameData[4] == icon &&
                gameData[6] == icon;

            return leftDiagonal || rightDiagonal;
        }

        private bool CheckColumn(int col, char icon)
        {
            bool win = true;
            win &= gameData[col + 0] == icon;
            win &= gameData[col + 3] == icon;
            win &= gameData[col + 6] == icon;

            return win;
        }

        private bool CheckRow(int row, char icon)
        {
            bool win = true;
            win &= gameData[row * 3 + 0] == icon;
            win &= gameData[row * 3 + 1] == icon;
            win &= gameData[row * 3 + 2] == icon;

            return win;
        }
    }
}
