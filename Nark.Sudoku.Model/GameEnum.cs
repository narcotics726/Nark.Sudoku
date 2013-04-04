using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nark.Sudoku.Model
{
    public static class GameEnum
    {
        public enum MapStat
        {
            Empty,
            Init,
            Filling,
            Completed
        }
    }
}
