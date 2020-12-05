using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020 {
    class Day05 {
        string[] _lines = Tools.Common.ReadLines("input05.txt");

        static int CalcId(int row, int col) {
            return row * 8 + col;
        }

        static int DecodeId(string specification) {
            int row = 0;
            int mul = 64;
            for (int i = 0; i < 7; i++) {
                if (specification[i] == 'B')
                    row += mul;
                mul /= 2;
            }

            int col = 0;
            mul = 4;
            for (int i = 7; i < 10; i++) {
                if (specification[i] == 'R')
                    col += mul;
                mul /= 2;
            }

            return CalcId(row, col);
        }

        public void PartOne() {
            int max = 0;
            foreach (var line in _lines) {
                var id = DecodeId(line);
                if (id > max)
                    max = id;
            }

            Console.WriteLine(max);
        }

        public void PartTwo() {
            var ids = new List<int>();
            foreach (var line in _lines) {
                ids.Add(DecodeId(line));
            }

            for (int row = 1; row < 127; row++) {
                for (int col = 0; col < 8; col++) {
                    int id = CalcId(row, col);
                    if (!ids.Contains(id) && ids.Contains(id - 1) && ids.Contains(id + 1))
                        Console.WriteLine(id);
                }
            }
        }
    }
}
