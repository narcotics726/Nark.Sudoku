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
                squareValue = value;
            }
        }


        public bool IsValidate
        {
            get
            {
                foreach (Square s in this.peers)
                {
                    if (s.squareValue == this.squareValue)
                        return false;
                }
                return true;
            }
        }

        List<Square> peers;

        public List<Square> Peers
        {
            get { return peers; }
            set { peers = value; }
        }

        List<string> validateValue;

        public List<string> ValidateValue
        {
            get
            {
                List<string> initPossibleValues = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                foreach (Square s in this.peers)
                {
                    initPossibleValues.Remove(s.squareValue);
                }
                return initPossibleValues;
            }
            set { validateValue = value; }
        }

        public Square(int _row, int _col, string sValue = "0")
        {
            column = _col;
            row = _row;
            squareValue = sValue;
            peers = new List<Square>();
            validateValue = new List<string>();
            for (int i = 1; i <= 9; i++)
            {
                validateValue.Add(i.ToString());
            }
        }

        public bool IsInOneUnit(Square obj)
        {
            return (obj.Column * 3 / 9 == Column * 3 / 9
                 && obj.Row * 3 / 9 == Row * 3 / 9);
        }

        //List<string> initPossibleValues = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    }
}
