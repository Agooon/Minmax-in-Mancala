using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Game
{
    public interface IEvaluatePoints
    {
        public double GetPoints(Mancala game, bool player1);
    }
}
