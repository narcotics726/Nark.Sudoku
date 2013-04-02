using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nark.Sudoku.Model
{
    public class Map
    {
        public GameEnum.MapStat MapStatus = GameEnum.MapStat.Empty;

        List<Square> squareList;
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
                s.Peers = squareList.FindAll(obj => (obj.Column == s.Column || obj.Row == s.Row || s.IsInOneUnit(obj)));
                s.Peers.Remove(s);
            }
        }

        public void SetSquareValue(int row, int col, string value)
        {
            Square s = this.squareList.Find(obj => obj.Row == row && obj.Column == col);
            s.SquareValue = value;
            CheckGameStatus();
        }

        private void CheckGameStatus()
        {
            if (MapStatus == GameEnum.MapStat.Filling)
            {
                foreach (Square s in squareList)
                {
                    if (!s.IsValidate)
                        return;
                }
                MapStatus = GameEnum.MapStat.Completed;
            }
        }

        public Square GetSquare(int row, int col)
        {
            return this.squareList.Find(obj => obj.Row == row && obj.Column == col);
        }

        public void Init()
        {
            List<List<string>> triedValue = new List<List<string>>();
            for (int i = 0; i < this.squareList.Count; i++)
            {
                triedValue.Add(new List<string>());
            }
            Random r = new Random();

            for (int i = 0; i < this.squareList.Count; i++)
            {
                Square s = squareList[i];
                List<string> plist = s.ValidateValue.FindAll(obj => (!triedValue[i].Exists(obj2 => (obj2 == obj))));
                if (plist.Count > 0)
                {
                    s.SquareValue = plist[r.Next(0, plist.Count)];
                    triedValue[i].Add(s.SquareValue);

                }
                else    //trace back
                {
                    s.SquareValue = "0";
                    //Once we trace a square back and fill it again with a new value, the following square's tried history should be cleared;
                    for (int j = i + 1; j < this.squareList.Count; j++)
                    {
                        triedValue[j].Clear();
                    }
                    i -= 2;

                }
            }
        }

        public void EraseRandomSquare()
        {
            Random r = new Random();
            while (true)
            {
                Square s = squareList[r.Next(0, squareList.Count)];
                string temp = s.SquareValue;
                s.SquareValue = "0";
                if (!this.HasUniqueSolution())
                {
                    s.SquareValue = temp;
                    break;
                }
            }
        }

        public bool HasUniqueSolution()
        {
            throw new NotImplementedException();
        }

        public List<List<Square>> Solver(List<Square> puzzle)
        {
            List<List<string>> triedValues = new List<List<string>>();
            throw new NotImplementedException();
        }


        #region forTest
        public void DrawMap()
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine();
                if ((i) % 3 == 0)
                    Console.WriteLine("---------| ---------| ---------|");
                for (int j = 0; j < 9; j++)
                {
                    Square s = this.SquareList.Find(obj => obj.Row == i && obj.Column == j);
                    if (!s.IsValidate)
                        Console.Write("!");
                    else
                        Console.Write(" ");
                    Console.Write(s.SquareValue + " ");
                    if ((j + 1) % 3 == 0)
                        Console.Write("| ");
                }
            }
            Console.WriteLine();
            Console.WriteLine("---------| ---------| ---------|");
        }
        #endregion


    }
}
