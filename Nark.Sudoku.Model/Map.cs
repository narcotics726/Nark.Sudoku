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
                    if (s.SquareValue == "0" || !s.IsValidate)
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

            MapStatus = GameEnum.MapStat.Init;
        }

        public void FullInit()
        {
            foreach (Square s in this.squareList)
            {
                s.SquareValue = "0";
            }
            SudokuHelper.Solve(this, true);
            MapStatus = GameEnum.MapStat.Init;
        }

        public void EraseRandomSquare(int eraseCount = 40)
        {
            Random r = new Random();
            List<Square> emptySquare = new List<Square>();
            while (emptySquare.Count < eraseCount)
            {
                //emptySquare = squareList.FindAll(obj => obj.SquareValue == "0");
                Square s = squareList[r.Next(0, squareList.Count)];
                if (s.SquareValue != "0")
                {
                    string temp = s.SquareValue;
                    if (!SudokuHelper.CanDig(this, s))
                        continue;
                    s.SquareValue = "0";
                    emptySquare.Add(s);
                    emptySquare.ForEach(obj => obj.SquareValue = "0");
                    //DrawMap();
                }
            }
            MapStatus = GameEnum.MapStat.Filling;
        }

        public bool HasUniqueSolution()
        {
            List<Square> emptySquares = squareList.FindAll(obj => obj.SquareValue == "0");
            emptySquares.Sort(delegate(Square x, Square y) { return y.ValidateValue.Count.CompareTo(x.ValidateValue.Count); });
            List<List<string>> triedValue = new List<List<string>>();
            for (int i = 0; i < this.squareList.Count; i++)
            {
                triedValue.Add(new List<string>());
            }
            Random r = new Random();
            for (int i = 0; i < emptySquares.Count; i++)
            {
                Square s = emptySquares[i];
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
                    if (i < 0)
                    {
                        foreach (Square ss in emptySquares)
                        {
                            ss.SquareValue = "0";
                        }
                        return false;
                    }
                }
            }
            foreach (Square s in emptySquares)
            {
                s.SquareValue = "0";
            }
            return true;
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
