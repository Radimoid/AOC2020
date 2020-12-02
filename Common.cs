using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020 {
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

    class Tokenizer {
        string _str;
        int _index;

        public Tokenizer(string str) {
            _str = str;
        }

        public string Next(string separator) {
            int startIndex = _index;
            _index = _str.IndexOf(separator, startIndex);
            string ret = _str.Substring(startIndex, _index - startIndex);
            _index += separator.Length;
            return ret;
        }

        public string Next() {
            return _str.Substring(_index);
        }
    }
}
