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
            DrawMap(m);
            while (true)
            {
                Console.WriteLine("Input: rowNum,lineNum,value");
                string inputLine = Console.ReadLine();
                if (inputLine == "win")
                {
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
                else
                {

                    try
                    {
                        string[] input = inputLine.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        m.SetSquareValue(int.Parse(input[0]), int.Parse(input[1]), input[2]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                }

                DrawMap(m);
                if (m.IsWin)
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
