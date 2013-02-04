using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace KrizicKruzic
{
    class GameManager
    {
        #region Privatne varijable
        private Player playerOne;
        private Player playerTwo;

        private PlayerManager playerManager;
        #endregion

        #region Konstruktor i inicijalizacija
        internal void Run()
        {
            try
            {
                playerManager = new PlayerManager();
            }
            catch (Exception excp)
            {
                Console.WriteLine("Pogreška kod inicijalizacije: {0}", 
                    excp.Message);
                return;
            }

            while (true)
            {
                ShowMenu();
                MenuOptions odabir = LoadOption();
                if (odabir == MenuOptions.Quit)
                {
                    return;
                }

                DoAction(odabir);
            }
        }
        #endregion

        #region Menu
        private void DoAction(MenuOptions odabir)
        {
            switch (odabir)
            {
                case MenuOptions.NewGame:
                    StartNewGame();
                    break;
                case MenuOptions.NewPlayer:
                    CreateNewPlayer();
                    break;
                case MenuOptions.PlayerList:
                    ShowPlayerList();
                    break;
                case MenuOptions.RemovePlayer:
                    RemovePlayer();
                    break;
                default:
                    break;
            }
        }

        private MenuOptions LoadOption()
        {
            while (true)
            {
                Console.Write("Odaberite> ");
                string unos = Console.ReadLine();
                switch (unos)
                {
                    case "1":
                        return MenuOptions.NewGame;
                    case "2":
                        return MenuOptions.NewPlayer;
                    case "3":
                        return MenuOptions.PlayerList;
                    case "4":
                        return MenuOptions.RemovePlayer;
                    case "5":
                        return MenuOptions.Quit;
                    default:
                        Console.WriteLine("Nepostojeća opcija, pokušajte ponovo!");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private enum MenuOptions { NewGame, NewPlayer, PlayerList, RemovePlayer, Quit };

        private void ShowMenu()
        {
            Console.WriteLine("KrizicKruzic v0.01a");
            string separator = new string('-', 60);
            Console.WriteLine(separator);
            Console.WriteLine("1. Nova igra");
            Console.WriteLine("2. Novi igrač");
            Console.WriteLine("3. Lista igrača");
            Console.WriteLine("4. Obriši igrača");
            Console.WriteLine("5. Kraj");
        }
        #endregion

        #region Player handling
        private void RemovePlayer()
        {
            throw new NotImplementedException();
        }

        private void ShowPlayerList()
        {
            Console.WriteLine("Svi igrači");
            string separator = new string('=', 60);
            Console.WriteLine(separator);
            Console.WriteLine("Nick\tPlayed\tWon\tLost\tDraw");
            Console.WriteLine(separator);
            foreach (Player player in playerManager.Players)
            {
                string info = String.Format(
                    "{0}\t{1}\t{2}\t{3}\t{4}",
                    player.Nick, player.GamesPlayed, player.GamesWon,
                    player.GamesLost, player.GamesDraw
                    );
                Console.WriteLine(info);
            }
            Console.WriteLine(separator);
        }

        private void CreateNewPlayer()
        {
            throw new NotImplementedException();
        }

        private void StartNewGame()
        {
            //ako nemamo učitane igrače,
            //traži od korisnika podatke
            if (playerOne == null || playerTwo == null)
            {
                LoadPlayers();
                if (playerOne == null || playerTwo == null)
                {
                    return;
                }
            }

            //za dva igrača, pokreni novu igru
            GameBoard board = new GameBoard(playerOne, playerTwo);
            board.PlayGame();

            playerManager.Save();
        }

        private void LoadPlayers()
        {
            playerOne = LoadOnePlayer("Prvi igrač (nick): ");
            if (playerOne != null)
            {
                playerTwo = LoadOnePlayer("Drugi igrač (nick): ");
            }
        }

        private Player LoadOnePlayer(string message)
        {
            Console.Write(message);
            string nick = Console.ReadLine();
            if (nick.Length > 0)
            {
                Player player = playerManager.GetPlayer(nick);

                if (player == null)
                {
                    Console.WriteLine(
                        "Igrač s nadimkom {0} ne postoji, želite li kreirati novog (D/n)?",
                        nick);
                    string novi = Console.ReadLine();
                    if (novi == String.Empty || novi == "d" || novi == "D")
                    {
                        player = playerManager.AddPlayer(nick);
                    }
                }
                return player;
            }
            return null;
        }

        #endregion
    }
}
