using System.Collections.Generic;

namespace AOC2020.Tools {
    class Common {
        public static int[] StrsToInts(string[] strs) {
            int[] ret = new int[strs.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = int.Parse(strs[i]);
            return ret;
        }

        public static string[] ReadLines(string input) {
            return System.IO.File.ReadAllLines(@"..\..\..\inputs\" + input);
        }

        public static int[] ReadInts(string input) {
            return StrsToInts(ReadLines(input));
        }

        public static void CheckInt(int val, int min, int max) {
            if (val < min)
                throw new System.Exception();
            if (val > max)
                throw new System.Exception();
        }

        static public bool IsCharInEveryString(char c, List<string> strings) {
            foreach (string str in strings)
                if (!str.Contains(c))
                    return false;
            return true;
        }
    }
}
