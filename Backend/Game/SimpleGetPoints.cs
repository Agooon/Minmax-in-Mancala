using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Game
{
    public class SimpleGetPoints : IEvaluatePoints
    {
        public double GetPoints(Mancala game, bool player1)
        {
            if (game.CheckEnd())
            {
                if (player1)
                    return game.Player1Well[0] + game.Player1Holes.Sum(x => x) - game.Player2Well[0] + game.Player2Holes.Sum(x => x);
                else
                    return game.Player2Well[0] + game.Player2Holes.Sum(x => x) - game.Player1Well[0] + game.Player1Holes.Sum(x => x);
            }
            else
            {
                if (player1)
                    return game.Player1Well[0] - game.Player2Well[0];
                else
                    return game.Player2Well[0] - game.Player1Well[0];
            }
        }
    }

    public class ExpandedGetPoints : IEvaluatePoints
    {
        public double GetPoints(Mancala game, bool player1)
        {
            if (game.CheckEnd())
            {
                if (player1)
                    return game.Player1Well[0] + game.Player1Holes.Sum(x => x) - game.Player2Well[0] + game.Player2Holes.Sum(x => x);
                else
                    return game.Player2Well[0] + game.Player2Holes.Sum(x => x) - game.Player1Well[0] + game.Player1Holes.Sum(x => x);
            }
            else
            {
                if (player1)
                    return game.Player1Well[0] - game.Player2Well[0];
                else
                    return game.Player2Well[0] - game.Player1Well[0];
            }
        }
    }
}
