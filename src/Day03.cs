using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020 {
    class Day03 {
        string[] lines = Tools.Common.ReadLines("input03.txt");

        int CheckSlope(int dx, int dy) {
            int x = 0;
            int y = 0;
            int cnt = 0;

            while(y < lines.Length) {
                if(x >= lines[y].Length)
                    x -= lines[y].Length;

                if(lines[y][x] == '#')
                    cnt++;

                x += dx;
                y += dy;
            }
            Console.WriteLine(cnt);
            return cnt;
        }

        public void PartOne() {
            CheckSlope(3, 1);           
        }

        public void PartTwo() {
            System.Numerics.BigInteger mul = 1;
            mul *= CheckSlope(1, 1);
            mul *= CheckSlope(3, 1);
            mul *= CheckSlope(5, 1);
            mul *= CheckSlope(7, 1);
            mul *= CheckSlope(1, 2);
            Console.WriteLine();
            Console.WriteLine(mul);
        }
    }
}
