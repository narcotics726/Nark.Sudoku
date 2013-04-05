using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nark.Sudoku.Model;

namespace Nark.Sudoku.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Map m = new Map();
            while (true)
            {
                Console.WriteLine("Input: rowNum,lineNum,value");
                string inputLine = Console.ReadLine();
                System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
                if (inputLine == "win")
                {
                    m = new Map();
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            string a = Console.ReadLine();
                            m.SetSquareValue(i, j, a);
                            DrawMap(m);
                        }
                    }
                }
                else if (inputLine == "gen")
                {
                    m = new Map();
                    st.Start();
                    m.Init();
                    m.EraseRandomSquare(30);
                    DrawMap(m);
                    st.Stop();
                    Console.WriteLine(st.Elapsed.TotalSeconds);
                }
                else if (inputLine == "erase")
                {
                    m.EraseRandomSquare();
                    DrawMap(m);
                }
                else if (inputLine == "solve")
                {
                    st.Restart();
                    m = SudokuHelper.Solve(m);
                    st.Stop();
                    DrawMap(m);
                    Console.WriteLine(st.Elapsed.TotalSeconds);
                }
                else
                {

                    try
                    {
                        m = new Map();
                        string[] input = inputLine.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        m.SetSquareValue(int.Parse(input[0]), int.Parse(input[1]), input[2]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                }

                if (m.MapStatus == GameEnum.MapStat.Completed)
                {
                    Console.WriteLine("Win");
                    break;
                }
            }
        }

        private static void DrawMap(Map m)
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine();
                if ((i) % 3 == 0)
                    Console.WriteLine("---------| ---------| ---------|");
                for (int j = 0; j < 9; j++)
                {
                    Square s = m.SquareList.Find(obj => obj.Row == i && obj.Column == j);
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
    }
}
