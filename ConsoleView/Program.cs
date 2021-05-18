using Backend.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleView
{
    class Program
    {
        static void Main(string[] args)
        {
            IEvaluatePoints simpleGetPoints = new SimpleGetPoints();
            Mancala mancala = new Mancala(ref simpleGetPoints, 6, player1Turn: true, 9,9);
            mancala.StartGame();

            //int maxDepth = 17;
            //Console.WriteLine("Hello Mancala!");
            //FileStream fs = new FileStream("data.txt", FileMode.Create);
            //using StreamWriter writeText = new StreamWriter(fs);

            //List<double> avgTimes = new List<double>();
            //List<double> avgMoves = new List<double>();
            //// From depth to depth
            //for (int i = 17; i <= maxDepth; i++)
            //{
            //    double avgTime = 0;
            //    double avgNumberM = 0;
            //    double avgTimeTotal = 0;
            //    double avgNumberMTotal = 0;
            //    writeText.WriteLine("\nIteration\tDepth\tAverage Time(ms)\tNumber of moves");
            //    // Every stage is repeated 6 times
            //    for (int j = 0; j < 6; j++)
            //    {

            //        Mancala mancala = new Mancala(5, player1Turn: false, i, i);
            //        mancala.StartGame();

            //        // Draw, winns the player, that started second, in this case player1 wins
            //        if (mancala.Player1Well[0] >= mancala.Player2Well[0])
            //        {
            //            avgTime = mancala.TimesOfPlayer1.Sum(x => x.TotalMilliseconds) / mancala.TimesOfPlayer1.Count;
            //            avgNumberM = mancala.MovesOfPlayer1;
            //        }
            //        else if (mancala.Player1Well[0] < mancala.Player2Well[0])
            //        {
            //            avgTime = mancala.TimesOfPlayer2.Sum(x => x.TotalMilliseconds) / mancala.TimesOfPlayer2.Count;
            //            avgNumberM = mancala.MovesOfPlayer2;
            //        }
            //        writeText.WriteLine($"{j}\t{i}\t{avgTime}\t{avgNumberM}");
            //        avgTimeTotal += avgTime;
            //        avgNumberMTotal += avgNumberM;
            //    }
            //    avgTimeTotal = avgTimeTotal / 8;
            //    avgNumberMTotal = avgNumberMTotal / 8;
            //    writeText.WriteLine($"AvgTimeTotal:\t{avgTimeTotal}");
            //    writeText.WriteLine($"avgNumberMTotal:\t{avgNumberMTotal}");

            //    avgTimes.Add(avgTimeTotal);
            //    avgMoves.Add(avgNumberMTotal);
            //}

            //writeText.WriteLine("\n\n\nDepth\tAverage Time(ms)\tNumber of moves");
            //for (int i = 16; i < maxDepth; i++)
            //{
            //    writeText.WriteLine($"{i + 1}\t{avgTimes[i]}\t{avgMoves[i]}");
            //}

        }
    }
}
