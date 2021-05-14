using Backend.Game;
using System;
using System.Collections.Generic;
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

        public int MinmaxAlg(Mancala currentInstance, int depth, bool max)
        {
            if (currentInstance.CheckEnd() || depth == 0)
                return currentInstance.BoardPoints(player);

            int bestMove = 0;
            int score;
            if (max)
                score = int.MinValue;
            else
                score = int.MaxValue;

            int[] possibleMoves = currentInstance.GetAvaibleHoles();
            foreach (var move in possibleMoves)
            {
                Mancala newInstance = new Mancala(currentInstance);
                bool change = newInstance.Move(move);
                bool maxNext;
                if (change)
                {
                    maxNext = !max;
                    newInstance.player1Turn = !newInstance.player1Turn;
                }
                else
                {
                    maxNext = max;
                }
                int result = MinmaxAlg(newInstance, depth - 1, maxNext);

                if (max)
                {
                    if (result > 0 && newInstance.CheckEnd() && depth != ChosenDepth)
                        return int.MaxValue;
                    if (result > score)
                    {
                        bestMove = move;
                        score = result;
                    }
                }
                else
                {
                    if (result < 0 && newInstance.CheckEnd() && depth != ChosenDepth)
                        return int.MinValue;
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
