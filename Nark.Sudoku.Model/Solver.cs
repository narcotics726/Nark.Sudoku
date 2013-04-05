using Nark.Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nark.Sudoku.Model
{
    public class SudokuHelper
    {
        public static Map Solve(Map m, bool init)
        {
            List<Square> emptyList = m.SquareList.FindAll(obj => obj.SquareValue == "0");
            emptyList.Sort(delegate(Square x, Square y) { return x.ValidateValue.Count.CompareTo(y.ValidateValue.Count); });
            List<List<string>> triedValues = new List<List<string>>(emptyList.Count);
            for (int i = 0; i < emptyList.Count; i++)
            {
                triedValues.Add(new List<string>(9));
            }
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < emptyList.Count; i++)
            {
                List<string> possibleList = emptyList[i].ValidateValue.FindAll(obj => (!triedValues[i].Exists(obj2 => obj == obj2)));   //find all values that is validated for this square and hasn't been tried
                if (possibleList.Count > 0)
                {

                    emptyList[i].SquareValue = possibleList[rand.Next(0, possibleList.Count)];
                    triedValues[i].Add(emptyList[i].SquareValue);
                }
                else
                {
                    emptyList[i].SquareValue = "0";

                    if (i >= 1)
                    {
                        for (int ii = init ? i + 1 : i; ii < emptyList.Count; ii++)
                        {
                            if (triedValues[ii].Count > 0)
                                triedValues[ii].Clear();    //we should clear the triedHistory before a tracing back
                        }
                        i -= 2;
                    }
                    else
                    {
                        //m = SudokuHelper.Solve(m);
                        return null;    //no result! should be impossible -,-
                    }
                }
                //m.DrawMap();
            }
            return m;
        }

        public static bool CanDig(Map m, Square s)
        {
            string originValue = s.SquareValue;
            if (s.ValidateValue.Count > 1)
            {
                foreach (string v in s.ValidateValue)
                {
                    if (v != originValue)
                    {
                        s.SquareValue = v;
                        if (Solve(m, false) != null)       //substitute the square's value with another validated one and try if there's another solution besides the unique one
                        {
                            s.SquareValue = originValue;
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
