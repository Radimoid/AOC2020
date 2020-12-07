using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day07 {
        string[] _lines = Tools.Common.ReadLines("input07.txt");

        void CheckColor(string colorName, List<string> result) {
            foreach (var line in _lines) {
                var splited = Tools.Common.SplitStrIntoTwoParts(line, ' ', 2);
               
                if (splited[1].Contains(colorName)) {
                    if (!result.Contains(splited[0])) {
                        result.Add(splited[0]);
                        CheckColor(splited[0], result);
                    }
                }
            }
        }
                
        int FindCompleteContent(string color) {
            foreach (var line in _lines) {
                if (line.StartsWith(color)) {

                    if (line.Contains("no other"))
                        return 0;

                    string[] splited1 = line.Split("contain ");
                    string[] parts = splited1[1].Split(',');

                    int ret = 0;
                    for (int i = 0; i < parts.Length; i++) {
                        var trimmed = parts[i].TrimStart();
                        var splited2 = Tools.Common.SplitStrIntoTwoParts(trimmed, ' ', 1);
                        int num = int.Parse(splited2[0]);
                        string newcolor = Tools.Common.Words(splited2[1], 2);
                        
                        ret += num;
                        ret += num * FindCompleteContent(newcolor);
                    }
                    return ret;
                }
            }

            return 0;
        }

        public void PartOne() {
            var result = new List<string>();
            CheckColor("shiny gold", result);
            Console.WriteLine(result.Count());
        }

        public void PartTwo() {
            Console.WriteLine(FindCompleteContent("shiny gold"));
        }
    }
}
