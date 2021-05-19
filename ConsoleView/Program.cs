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
            bool allow = false;
            IEvaluatePoints simpleGetPoints = new SimpleGetPoints();
            IEvaluatePoints expandedGetPoints = new ExpandedGetPoints();
            ////IEvaluatePoints simpleGetPoints2 = new SimpleGetPoints();
            ////IEvaluatePoints simpleGetPoints2 = new ExpandedGetPoints();
            Mancala mancala1 = new Mancala(ref expandedGetPoints, ref simpleGetPoints, 5, player1Turn: false, 8, 8);
            mancala1.PrintText= true;
            mancala1.StartGame();


            if (allow)
            {


                Console.WriteLine(double.MinValue + 1 - double.MinValue);

                int maxDepth = 12;

                int numberOfGames = 50;

                Console.WriteLine("Hello Mancala!");
                FileStream fs = new FileStream("data.txt", FileMode.Create);
                using StreamWriter writeText = new StreamWriter(fs);

                List<double> avgTimes = new List<double>();
                List<double> avgMoves = new List<double>();

                List<double> wrP1 = new List<double>();
                List<double> avgLead1 = new List<double>();
                List<double> wrP2 = new List<double>();
                List<double> avgLead2 = new List<double>();
                // From depth to depth
                for (int i = 1; i <= maxDepth; i++)
                {
                    double avgTime = 0;
                    double avgNumberM = 0;
                    double avgTimeTotal = 0;
                    double avgNumberMTotal = 0;
                    double winsP1 = 0;
                    double leadP1 = 0;
                    double winsP2 = 0;
                    double leadP2 = 0;
                    //writeText.WriteLine("\nIteration\tDepth\tAverage Time(ms)\tNumber of moves");
                    // Every stage is repeated 6 times
                    for (int j = 0; j < numberOfGames; j++)
                    {
                        Console.WriteLine("Current Game: " + j);
                        Mancala mancala = new Mancala(ref simpleGetPoints, ref expandedGetPoints, 5, player1Turn: false, i, i);
                        mancala.PrintText = false;
                        mancala.StartGame();

                        // Draw, winns the player, that started second, in this case player1 wins
                        if (mancala.Player1Well[0] > mancala.Player2Well[0])
                        {
                            leadP1 += mancala.Player1Well[0] - mancala.Player2Well[0];
                            avgTime = mancala.TimesOfPlayer1.Sum(x => x.TotalMilliseconds) / mancala.TimesOfPlayer1.Count;
                            avgNumberM = mancala.MovesOfPlayer1;
                            winsP1++;
                        }
                        else if (mancala.Player1Well[0] < mancala.Player2Well[0])
                        {
                            leadP2 += mancala.Player2Well[0] - mancala.Player1Well[0];
                            avgTime = mancala.TimesOfPlayer2.Sum(x => x.TotalMilliseconds) / mancala.TimesOfPlayer2.Count;
                            avgNumberM = mancala.MovesOfPlayer2;
                            winsP2++;
                        }
                        //writeText.WriteLine($"{j}\t{i}\t{avgTime}\t{avgNumberM}");
                        avgTimeTotal += avgTime;
                        avgNumberMTotal += avgNumberM;
                    }
                    avgTimeTotal = avgTimeTotal / 8;
                    avgNumberMTotal = avgNumberMTotal / 8;
                    //writeText.WriteLine($"AvgTimeTotal:\t{avgTimeTotal}");
                    //writeText.WriteLine($"avgNumberMTotal:\t{avgNumberMTotal}");

                    avgTimes.Add(avgTimeTotal);
                    avgMoves.Add(avgNumberMTotal);
                    wrP1.Add(winsP1 / numberOfGames);
                    wrP2.Add(winsP2 / numberOfGames);
                    avgLead1.Add(leadP1 / winsP1);
                    avgLead2.Add(leadP2 / winsP2);
                }

                //writeText.WriteLine("\n\n\nDepth\tAverage Time(ms)\tNumber of moves");
                //for (int i = 0; i < maxDepth; i++)
                //{
                //    writeText.WriteLine($"{i + 1}\t{avgTimes[i]}\t{avgMoves[i]}");
                //}

                writeText.WriteLine("\n\n\nDepth\tPlayer 1 simple WR %\tPlayer 2 Expanded WR %\t AvgLead P1 Simple\t AvgLead P2 Expanded");

                for (int i = 0; i < maxDepth; i++)
                {
                    writeText.WriteLine($"{i + 1}\t{wrP1[i]}\t{wrP2[i]}\t{avgLead1[i]}\t{avgLead2[i]}");
                }

            }
        }
    }
}
