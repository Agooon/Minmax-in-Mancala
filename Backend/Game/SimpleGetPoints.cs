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
                    return game.Player1Well[0] + game.Player1Holes.Sum(x => x) - game.Player2Well[0] - game.Player2Holes.Sum(x => x);
                else
                    return game.Player2Well[0] + game.Player2Holes.Sum(x => x) - game.Player1Well[0] - game.Player1Holes.Sum(x => x);
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

    // Hole points (for combo creation):
    // Hole 1 with 1 gets 1,0
    // Hole 2 with 2 gets 1,5 (additional 1,0, if hole 1,5 is empty)
    // Hole 3 with 3 gets 2,0 (additional 1,0, if hole 1,5 is empty)
    // Hole 4 with 4 gets 2,5 (additional 1,0, if hole 1,5 is empty)
    // Hole 5 with 5 gets 3,0 (additional 1,0, if hole 1,5 is empty)
    // Hole 6 with 6 gets 4,0 (additional 1,0, if hole 1,5 is empty)
    // + Total number of stones in our holes *HoleValue
    // + Well count number*WellValue
    // + ending when possition is winning

    // Checking for traps
    // Hole 2 gets point (number of stone equal to enemy hole 6)
    public class ExpandedGetPoints : IEvaluatePoints
    {
        public ExpandedGetPoints(double wellValue = 7, double holeValue = 0.1, double comboValue = 1.5)
        {
            WellValue = wellValue;
            HoleValue = holeValue;
            ComboValue = comboValue;
        }
        public const int winningVal = 1000;

        public double WellValue { get; set; }
        public double HoleValue { get; set; }
        public double ComboValue { get; set; }

        public double GetPoints(Mancala game, bool player1)
        {
            double result = 0;
            if (player1)
            {

                // Winning it's good
                if (game.CheckEnd())
                {
                    return game.Player1Well[0] + game.Player1Holes.Sum(x => x) - game.Player2Well[0] - game.Player2Holes.Sum(x => x);
                }
                // Game continue, so we calculate points like above functions

                // Adding points when we have it
                double raisingVal = 1;
                for (int i = 0; i < game.Player1Holes.Length; i++)
                {
                    if (game.Player1Holes[i] == i + 1)
                    {
                        result += raisingVal;
                        // Because it can combo up
                        if (i > 0 && game.Player1Holes[0] == 0)
                            result += ComboValue;
                    }
                    raisingVal += 0.5;
                }
                result += game.Player1Holes.Sum(x => x) * this.HoleValue;
                result += game.Player1Well[0] * WellValue;

                // Subtracting points from
                raisingVal = 1;
                for (int i = game.Player1Holes.Length - 1; i >= 0; i--)
                {
                    if (game.Player2Holes[i] == i + 1)
                    {
                        result -= raisingVal;
                        // Because it can combo up
                        if (i > 0 && game.Player2Holes[0] == 0)
                            result -= ComboValue;
                    }
                    raisingVal += 0.5;
                }
                result -= game.Player2Holes.Sum(x => x) * this.HoleValue;
                result -= game.Player2Well[0] * WellValue;

                return result;
            }
            else
            {
                if (game.CheckEnd())
                {
                    return game.Player2Well[0] + game.Player2Holes.Sum(x => x) - game.Player1Well[0] - game.Player1Holes.Sum(x => x);
                }
                // Game continue, so we calculate points like above functions

                // Abosite adding and Subtracting, than above
                double raisingVal = 1;
                for (int i = 0; i < game.Player2Holes.Length; i++)
                {
                    if (game.Player1Holes[i] == i + 1)
                    {
                        result -= raisingVal;
                        // Because it can combo up
                        if (i > 0 && game.Player1Holes[0] == 0)
                            result -= ComboValue;
                    }
                    raisingVal -= 0.5;
                }
                result -= game.Player1Holes.Sum(x => x) * this.HoleValue;
                result -= game.Player1Well[0] * WellValue;

                // Subtracting points from
                raisingVal = 1;
                for (int i = game.Player2Holes.Length - 1; i >= 0; i--)
                {
                    if (game.Player2Holes[i] == i + 1)
                    {
                        result += raisingVal;
                        // Because it can combo up
                        if (i > 0 && game.Player2Holes[0] == 0)
                            result += ComboValue;
                    }
                    raisingVal += 0.5;
                }
                result += game.Player2Holes.Sum(x => x) * this.HoleValue;
                result += game.Player2Well[0] * WellValue;

                return result;
            }
        }
    }
}
