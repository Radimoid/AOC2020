using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020.Tools {
    class Common {
        public static int[] StrsToInts(string[] strs) {
            int[] ret = new int[strs.Length];
            for(int i = 0; i < ret.Length; i++)
                ret[i] = int.Parse(strs[i]);
            return ret;
        }

        public static string[] ReadLines(string input) {
            return System.IO.File.ReadAllLines("..\\..\\..\\" + input);
        }

        public static int[] ReadInts(string input) {
            return StrsToInts(ReadLines(input));
        }
    }
}
