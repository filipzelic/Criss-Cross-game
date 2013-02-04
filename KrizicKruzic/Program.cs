using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//GameManager
//Player
//Icon
//GameBoard
//
namespace KrizicKruzic
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager manager = new GameManager();
            manager.Run();

            //Window mojProzor = new Window();
            //mojProzor.SetTitle("Moj prvi prozor");

            //Console.WriteLine(mojProzor.GetTitle());

            //mojProzor.Oznaka = "1642";
            //Console.WriteLine("Oznaka: {0}", 
            //        mojProzor.Oznaka);

            //mojProzor.Position = new Point(10, 16);
        }
    }
}
