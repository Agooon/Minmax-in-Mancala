using Backend.Game;
using System;

namespace ConsoleView
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Mancala!");

            Mancala mancala = new Mancala(5,player1Turn:false);

            mancala.StartGame();
        }
    }
}
