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
        public static Map Solve(Map m)
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
                    for (int ii = i + 1; ii < emptyList.Count; ii++)
                    {
                        triedValues[ii].Clear();    //we should clear the triedHistory before a tracing back
                    }
                    if (i >= 1)
                    {
                        i -= 2;
                    }
                    else
                    {
                        return null;    //no result! should be impossible -,-
                    }
                }
                //m.DrawMap();
            }
            return m;
        }
    }
}
