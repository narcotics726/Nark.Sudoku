using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nark.Sudoku.Model
{
    public class Map
    {
        List<Square> squareList;

        bool isWin;

        public bool IsWin
        {
            get { return isWin; }
            set { isWin = value; }
        }

        public List<Square> SquareList
        {
            get { return squareList; }
            set { squareList = value; }
        }

        readonly int columnNum = 9;
        readonly int rowNum = 9;

        public Map()
        {
            squareList = new List<Square>(columnNum * rowNum);
            for (int row = 0; row < rowNum; row++)
            {
                for (int col = 0; col < columnNum; col++)
                {
                    Square s = new Square(row, col);
                    squareList.Add(s);
                }
            }

            foreach (Square s in squareList)
            {
                s.Peers = squareList.FindAll(obj => (obj.Column == s.Column || obj.Column == s.Column || IsInOneUnit(obj, s)));
                s.Peers.Remove(s);
            }
        }

        private bool IsInOneUnit(Square obj, Square s)
        {
            return (obj.Column * 3 / columnNum == s.Column * 3 / columnNum
                 && obj.Row * 3 / rowNum == s.Row * 3 / rowNum);
        }

        public void SetSquareValue(int row, int col, string value)
        {
            Square s = this.squareList.Find(obj => obj.Row == row && obj.Column == col);
            s.SquareValue = value;
            isWin = CheckGameStatusIsWin();
        }

        private bool CheckGameStatusIsWin()
        {
            foreach (Square s in squareList)
            {
                if (!s.IsValidate)
                    return false;
            }
            return true;
        }

        public Square GetSquare(int row, int col)
        {
            return this.squareList.Find(obj => obj.Row == row && obj.Column == col);
        }

        public void SetValue(int[][] a)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SetSquareValue(i, j, a[i][j].ToString());
                }
            }
        }
    }
}
