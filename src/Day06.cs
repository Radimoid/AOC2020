using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day06 {
        static int CountAny(List<string> group) {
            var answered = new List<char>();
            foreach (string line in group) {
                foreach (char c in line)
                    if (!answered.Contains(c))
                        answered.Add(c);
            }
            return answered.Count();
        } 

        static int CountEvery(List<string> group) {
            int sum = 0;
            foreach (char c in group[0]) {
                if (Tools.Common.IsCharInEveryString(c, group))
                    sum++;

            }
            return sum;
        }

        string[] _lines = Tools.Common.ReadLines("input06.txt");

        public void PartOne() {
            var group = new List<string>();
            int sum = 0;
            foreach (string line in _lines) {
                if (string.IsNullOrEmpty(line)) {
                    sum += CountAny(group);
                    group = new List<string>();
                }
                else {
                    group.Add(line);
                }
            }
            sum += CountAny(group);
            Console.WriteLine(sum);
        }

        public void PartTwo() {
            var group = new List<string>();
            int sum = 0;
            foreach (string line in _lines) {
                if (string.IsNullOrEmpty(line)) {
                    sum += CountEvery(group);
                    group = new List<string>();
                }
                else {
                    group.Add(line);
                }
            }
            sum += CountEvery(group);
            Console.WriteLine(sum);
        }
    }
}
