using Backend.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Algorithms
{
    public class Minmax
    {
        public const int TreeDepth = 5;

        private readonly int ChosenDepth;
        private bool player;

        public Minmax(bool player1Turn, int depth)
        {
            this.ChosenDepth = depth;
            player = player1Turn;
        }

        public double AplhaBeta(Mancala currentInstance, int depth, bool max, bool emptyMove, double alpha, double beta)
        {
            if (currentInstance.CheckEnd() || depth == 0)
                return currentInstance.BoardPoints(player);

            int bestMove = 0;

            // If there is empty move, we move -1, which doesnt change game state
            int[] possibleMoves = new int[1] { -1 };

            if (!emptyMove)
                possibleMoves = currentInstance.GetAvaibleHoles();


            double result;
            if (max)
            {
                foreach (var move in possibleMoves)
                {
                    Mancala newInstance = new Mancala(currentInstance);
                    bool change = newInstance.Move(move);

                    bool maxNext = !max;
                    newInstance.player1Turn = !newInstance.player1Turn;

                    if (change)
                        emptyMove = false;
                    else
                        emptyMove = true;

                    // Empty move doesn't change board situation 
                    result = AplhaBeta(newInstance, depth - 1, maxNext, emptyMove, alpha, beta);

                    // Aplha-Beta part
                    if (result > alpha)
                    {
                        alpha = result; // Found a better best move
                        bestMove = move;
                    }
                    if (alpha >= beta && depth != ChosenDepth)
                        return alpha;
                }
                if (depth != ChosenDepth)
                    return alpha;

            }
            else
            {
                foreach (var move in possibleMoves)
                {
                    Mancala newInstance = new Mancala(currentInstance);
                    bool change = newInstance.Move(move);
                    bool maxNext = !max;
                    newInstance.player1Turn = !newInstance.player1Turn;

                    if (change)
                        emptyMove = false;
                    else
                        emptyMove = true;

                    // Empty move doesn't change board situation 
                    result = AplhaBeta(newInstance, depth - 1, maxNext, emptyMove, alpha, beta);

                    // Aplha-Beta part
                    if (result < beta)
                    {
                        beta = result; // Found a better best move
                        bestMove = move;
                    }
                    if (alpha >= beta && depth != ChosenDepth)
                        return beta;
                }
                if (depth != ChosenDepth)
                    return beta;
            }


            return bestMove;
        }

        public double MinmaxAlg(Mancala currentInstance, int depth, bool max, bool emptyMove)
        {
            if (currentInstance.CheckEnd() || depth == 0)
                return currentInstance.BoardPoints(player);

            int bestMove = 0;
            double score;
            if (max)
                score = double.MinValue;
            else
                score = double.MaxValue;

            int[] possibleMoves = new int[1] { -1 };

            if (!emptyMove)
                possibleMoves = currentInstance.GetAvaibleHoles();

            if(!player)
                possibleMoves = possibleMoves.Reverse().ToArray();


            double result;
            if (max)
            {
                foreach (var move in possibleMoves)
                {
                    Mancala newInstance = new Mancala(currentInstance);
                    bool change = newInstance.Move(move);
                    bool maxNext = !max;
                    newInstance.player1Turn = !newInstance.player1Turn;

                    if (change)
                        emptyMove = false;
                    else
                        emptyMove = true;

                    // Empty move doesn't change board situation 
                    result = MinmaxAlg(newInstance, depth - 1, maxNext, emptyMove);

                    if (result > score)
                    {
                        bestMove = move;
                        score = result;
                    }
                }

            }
            // Min
            else
            {
                foreach (var move in possibleMoves)
                {
                    Mancala newInstance = new Mancala(currentInstance);
                    bool change = newInstance.Move(move);
                    bool maxNext = !max;
                    newInstance.player1Turn = !newInstance.player1Turn;

                    if (change)
                        emptyMove = false;
                    else
                        emptyMove = true;

                    // Empty move doesn't change board situation 
                    result = MinmaxAlg(newInstance, depth - 1, maxNext, emptyMove);

                    if (result < score)
                    {
                        bestMove = move;
                        score = result;
                    }
                }
            }
            if (depth != ChosenDepth)
                return score;

            return bestMove;
        }
    }
}
