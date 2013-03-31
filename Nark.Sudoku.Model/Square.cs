using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nark.Sudoku.Model
{
    public class Square
    {
        int column;

        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        int row;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        string squareValue;

        public string SquareValue
        {
            get { return squareValue; }
            set
            {
                isValidate = possibleValue.Exists(obj => obj == value);
                squareValue = value;
                if (isValidate)
                {
                    foreach (Square s in this.peers)
                    {
                        s.PossibleValue.Remove(value);
                    }
                }
            }
        }

        bool isValidate;
        public bool IsValidate
        {
            get { return isValidate; }
            set { isValidate = value; }
        }

        List<Square> peers;

        public List<Square> Peers
        {
            get { return peers; }
            set { peers = value; }
        }

        List<string> possibleValue;

        public List<string> PossibleValue
        {
            get { return possibleValue; }
            set { possibleValue = value; }
        }

        public Square(int _row, int _col, string sValue = "0")
        {
            column = _col;
            row = _row;
            squareValue = sValue;
            peers = new List<Square>();
            possibleValue = new List<string>();
            for (int i = 1; i <= 9; i++)
            {
                possibleValue.Add(i.ToString());
            }
        }


    }
}
