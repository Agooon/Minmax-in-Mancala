using Backend.Algorithms;
using Backend.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Game
{
    public static class PlayerType
    {
        public const string Human = "Human";
        public const string Ai = "  Ai ";
        public const string Ai_AB = "Ai_AB";
    }
    public class Mancala
    {

        public const int HoleNumber = 6;
        private bool firstMove = true;
        public bool player1Turn { get; set; }

        // Player 1 values
        private readonly string Player1;
        private readonly string Player2;
        public int Player1Depth { get; set; } = 16;
        public int Player2Depth { get; set; } = 16;
        public int MoveNumber { get; set; } = 0;


        // Holes
        private int[] Player1Holes = new int[6]
        {
            4,4,4,4,4,4
        };
        private int[] Player1Well = new int[1] { 0 };

        private int[] Player2Holes = new int[6]
        {
            4,4,4,4,4,4
        };
        private int[] Player2Well = new int[1] { 0 };


        //private int[] Player1Holes = new int[6]
        //{
        //    4,4,4,4,4,4
        //};
        //private int[] Player1Well = new int[1] { 0 };
        //private int[] Player2Holes = new int[6]
        //{
        //    4,4,4,4,4,4
        //};
        //private int[] Player2Well = new int[1] { 0 };

        // gameMode = 1 - H vs H
        // gameMode = 2 - H vs Ai
        // gameMode = 3 - Ai vs Ai
        // gameMode = 4 - H vs Ai_AB
        // gameMode = 5 - Ai_AB vs Ai_AB
        // gameMode = 6 - Ai_AB vs Ai

        // For deepCopy
        public Mancala(Mancala mancala)
        {
            firstMove = mancala.firstMove;
            player1Turn = mancala.player1Turn;

            Player1 = mancala.Player1;
            Player1Holes = mancala.Player1Holes.Select(x => x).ToArray();
            Player1Well = mancala.Player1Well.Select(x => x).ToArray();

            Player2 = mancala.Player2;
            Player2Holes = mancala.Player2Holes.Select(x => x).ToArray();
            Player2Well = mancala.Player2Well.Select(x => x).ToArray();
        }
        public Mancala(int gameMode = 1, bool player1Turn = true)
        {
            switch (gameMode)
            {
                case 1:
                    Player1 = PlayerType.Human;
                    Player2 = PlayerType.Human;
                    break;
                case 2:
                    Player1 = PlayerType.Human;
                    Player2 = PlayerType.Ai;
                    break;
                case 3:
                    Player1 = PlayerType.Ai;
                    Player2 = PlayerType.Ai;
                    break;
                case 4:
                    Player1 = PlayerType.Human;
                    Player2 = PlayerType.Ai_AB;
                    break;
                case 5:
                    Player1 = PlayerType.Ai_AB;
                    Player2 = PlayerType.Ai_AB;
                    break;
                case 6:
                    Player1 = PlayerType.Ai;
                    Player2 = PlayerType.Ai_AB;
                    break;
            }

            this.player1Turn = player1Turn;
        }

        public void StartGame()
        {
            firstMove = false;
            bool emptyMove = false;
            while (true)
            {
                if (!emptyMove)
                    Console.WriteLine(ShowGameState());
                else
                    Console.WriteLine("SKIP");
                int playerMove = GetPlayerMove(emptyMove);
                bool change = Move(playerMove);
                player1Turn = !player1Turn;
                if (change)
                    emptyMove = false;
                else
                    emptyMove = true;
                if (CheckEnd())
                {
                    Console.WriteLine(ShowGameState());
                    Console.WriteLine("ENDING");
                    Player1Well[0] += Player1Holes.Sum(x => x);
                    Player2Well[0] += Player2Holes.Sum(x => x);
                    Player1Holes = Player1Holes.Select(x => 0).ToArray();
                    Player2Holes = Player2Holes.Select(x => 0).ToArray();
                    Console.WriteLine(ShowGameState());
                    Console.WriteLine($"Player 1 scored {Player1Well[0]} points");
                    Console.WriteLine($"Player 2 scored {Player2Well[0]} points");


                    if (Player1Well[0] == Player2Well[0])
                    {
                        Console.WriteLine("A draw!");
                        return;
                    }
                    if (Player1Well[0] > Player2Well[0])
                    {
                        Console.WriteLine($"Player 1 won by {Player1Well[0] - Player2Well[0]} points!");
                        return;
                    }
                    if (Player1Well[0] < Player2Well[0])
                    {
                        Console.WriteLine($"Player 2 won by {Player2Well[0] - Player1Well[0]} points!");
                        return;
                    }
                }
            }
        }

        private int GetPlayerMove(bool emptyMove)
        {
            if (emptyMove)
                return -1;
            if (player1Turn)
            {
                if (Player1 == PlayerType.Human)
                    return GetHumanMove();
                else if (Player1 == PlayerType.Ai)
                {
                    int aiMove = GetAiMove();
                    Console.WriteLine("Ai moved: " + aiMove);
                    return aiMove;
                }
                // Aplha-Beta
                else
                {
                    int aiMove = GetAiMoveAB();

                    Console.WriteLine("Ai_AB moved: " + aiMove);
                    return aiMove;
                }
            }
            else
            {
                if (Player2 == PlayerType.Human)
                    return GetHumanMove();
                else if (Player2 == PlayerType.Ai)
                {
                    int aiMove = GetAiMove();

                    Console.WriteLine("Ai moved: " + aiMove);
                    return aiMove;
                }
                // Aplha-Beta
                else
                {
                    int aiMove = GetAiMoveAB();

                    Console.WriteLine("Ai_AB moved: " + aiMove);
                    return aiMove;
                }
            }
        }

        private int GetAiMove()
        {
            if (firstMove)
            {
                firstMove = false;
                return new Random().Next(1, 7);
            }
            if (player1Turn)
            {
                Minmax minmax = new Minmax(player1Turn, Player1Depth);
                return minmax.MinmaxAlg(this, Player1Depth, true, false);
            }
            else
            {
                Minmax minmax = new Minmax(player1Turn, Player2Depth);
                return minmax.MinmaxAlg(this, Player2Depth, true, false);
            }
        }

        private int GetAiMoveAB()
        {
            if (firstMove)
            {
                firstMove = false;
                return new Random().Next(1, 7);
            }
            if (player1Turn)
            {
                Minmax minmax = new Minmax(player1Turn, Player1Depth);
                return minmax.AplhaBeta(this, Player1Depth, true, false, int.MinValue, int.MaxValue);
            }
            else
            {
                Minmax minmax = new Minmax(player1Turn, Player2Depth);
                return minmax.AplhaBeta(this, Player2Depth, true, false, int.MinValue, int.MaxValue);
            }
        }


        private int GetHumanMove()
        {
            do
            {
                firstMove = false;
                string answer;
                int answerInt;
                try
                {
                    Console.WriteLine($"Choose hole: {string.Join(" ", GetAvaibleHoles())}");
                    answer = Console.ReadLine();
                    answerInt = Convert.ToInt32(answer);
                    if (GetAvaibleHoles().Any(x => x == answerInt))
                        return answerInt;
                    Console.WriteLine("Wrong option!");
                }
                catch
                {
                    Console.WriteLine("Wrong option!");
                }
            } while (true);
        }

        // Returns true if the turn ends
        public bool Move(int playerMove)
        {
            bool change = true;
            if (playerMove != -1)
            {
                int[] actualHoles;
                int[] actualWell;
                int stones;
                bool activePlayer = player1Turn;
                bool currentPlayerHoles = player1Turn;
                if (player1Turn)
                {
                    actualHoles = Player1Holes;
                }
                else
                {
                    actualHoles = Player2Holes;
                }
                stones = actualHoles[playerMove - 1];
                actualHoles[playerMove - 1] = 0;
                bool endOfSection = false;
                int startingId = playerMove;



                while (stones > 0)
                {
                    if (currentPlayerHoles)
                    {
                        actualHoles = Player1Holes;
                        actualWell = Player1Well;
                    }
                    else
                    {
                        actualHoles = Player2Holes;
                        actualWell = Player2Well;
                    }
                    if (endOfSection)
                        startingId = 0;

                    // Until end of section
                    for (int i = startingId; i < HoleNumber; i++)
                    {
                        if (stones > 0)
                        {
                            stones--;
                            actualHoles[i]++;
                            if (activePlayer == currentPlayerHoles && stones == 0 && actualHoles[i] == 1
                                && GetEnemyHoles()[HoleNumber - 1 - i] != 0)
                            {
                                actualHoles[i] = 0;
                                actualWell[0] += GetEnemyHoles()[HoleNumber - 1 - i] + 1;
                                GetEnemyHoles()[HoleNumber - 1 - i] = 0;
                            }
                        }
                    }
                    if (activePlayer == currentPlayerHoles && stones > 0)
                    {
                        actualWell[0]++;
                        stones--;
                        if (stones == 0)
                        {
                            change = false;
                        }
                    }
                    endOfSection = true;
                    // Stones expand to enemy
                    currentPlayerHoles = !currentPlayerHoles;
                }
            }
            return change;
        }


        public int[] GetAvaibleHoles()
        {
            List<int> possibleMoves = new List<int>();
            if (player1Turn)
                for (int i = Player1Holes.Length - 1; i >= 0; i--)
                {
                    if (Player1Holes[i] != 0)
                        possibleMoves.Add(i + 1);
                }
            else
                for (int i = 0; i < Player2Holes.Length; i++)
                {
                    if (Player2Holes[i] != 0)
                        possibleMoves.Add(i + 1);
                }


            return possibleMoves.ToArray();
        }

        private ref int[] GetEnemyHoles()
        {
            if (player1Turn)
                return ref Player2Holes;
            else
                return ref Player1Holes;
        }
        public int BoardPoints(bool player1)
        {
            if (CheckEnd())
            {
                if (player1)
                    return Player1Well[0] + Player1Holes.Sum(x => x) - Player2Well[0] + Player2Holes.Sum(x => x);
                else
                    return Player2Well[0] + Player2Holes.Sum(x => x) - Player1Well[0] + Player1Holes.Sum(x => x);
            }
            else
            {
                if (player1)
                    return Player1Well[0] - Player2Well[0];
                else
                    return Player2Well[0] - Player1Well[0];
            }

        }

        public bool CheckEnd()
        {
            bool p1Move = false;
            bool p2Move = false;
            foreach (int holeValue in Player1Holes)
                if (holeValue != 0)
                    p1Move = true;

            foreach (int holeValue in Player2Holes)
                if (holeValue != 0)
                    p2Move = true;

            return !p1Move || !p2Move;
        }

        public string ShowGameState()
        {
            string output = "";
            string p1;
            string p2;
            if (player1Turn)
            {
                p1 = " --> Player 1: " + Player1 + "|Depth: " + Hole(Player1Depth);
                p2 = "     Player 2: " + Player2 + "|Depth: " + Hole(Player2Depth);
            }
            else
            {
                p1 = "     Player 1: " + Player1 + "|Depth: " + Hole(Player1Depth);
                p2 = " --> Player 2: " + Player2 + "|Depth: " + Hole(Player2Depth);
            }
            MoveNumber++;
            output += $" ________________________________________\n" +
                      $"|{p1}          |\n" +
                      $"|________________________________________|\n" +
                      $"|      {Hole(Player1Holes[5])} | {Hole(Player1Holes[4])} | {Hole(Player1Holes[3])} | {Hole(Player1Holes[2])} | {Hole(Player1Holes[1])} | {Hole(Player1Holes[0])} |     |\n" +
                      $"| {Hole(Player1Well[0])} -------------------------------  {Hole(Player2Well[0])} |\n" +
                      $"|    | {Hole(Player2Holes[0])} | {Hole(Player2Holes[1])} | {Hole(Player2Holes[2])} | {Hole(Player2Holes[3])} | {Hole(Player2Holes[4])} | {Hole(Player2Holes[5])}       |\n" +
                      $"|________________________________________|\n" +
                      $"|{p2}          |\n" +
                      $"| Move number: {Hole(MoveNumber)}                        |\n" +
                      $"|________________________________________|\n";

            return output;
        }

        private static string Hole(int number)
        {
            if (number < 10)
                return number.ToString() + " ";
            else
                return number.ToString();
        }
    }
}
