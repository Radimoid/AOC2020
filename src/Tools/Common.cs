using System.Collections.Generic;
using System.Numerics;

namespace AOC2020.Tools {
    class Common {
        public static int[] StrsToInts(string[] strs) {
            int[] ret = new int[strs.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = int.Parse(strs[i]);
            return ret;
        }

        public static BigInteger[] StrsToBigIntegers(string[] strs) {
            BigInteger[] ret = new BigInteger[strs.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = BigInteger.Parse(strs[i]);
            return ret;
        }

        static public char[,] StrsToChars(string[] strs) {
            var ret = new char[strs[0].Length, strs.Length];
            for (int row = 0; row < strs.Length; row++)
                for (int col = 0; col < strs[0].Length; col++)
                    ret[col, row] = strs[row][col];
            return ret;
        }

        public static string[] ReadLines(string path) {
            return System.IO.File.ReadAllLines(@"..\..\..\inputs\" + path);
        }

        public static string ReadAllText(string path) {
            return System.IO.File.ReadAllText(@"..\..\..\inputs\" + path);
        }

        public static int[] ReadInts(string input) {
            return StrsToInts(ReadLines(input));
        }

        public static int[] ParseIntsLine(string line, char splitter = ',') {
            string[] splited = line.Split(splitter);
            int[] ret = new int[splited.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = int.Parse(splited[i].Trim());
            return ret;
        }

        public static int[] ReadIntsRow(string path) {
            string line = System.IO.File.ReadAllText(@"..\..\..\inputs\" + path);
            return ParseIntsLine(line);
        }

        public static BigInteger[] ReadBigIntegers(string input) {
            return StrsToBigIntegers(ReadLines(input));
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

        static public string Words(string str, int count, char separator = ' ') {
            int index = -1;
            for (int i = 0; i < count; i++) {
                index = str.IndexOf(separator, index + 1);
            }

            return str.Substring(0, index);
        }

        static public string[] SplitStrIntoTwoParts(string str, char separator, int num) {
            int index = -1;
            for (int i = 0; i < num; i++) {
                index = str.IndexOf(separator, index + 1);
            }

            string[] ret = new string[2];
            ret[0] = str.Substring(0, index);
            ret[1] = str.Substring(index + 1);
            return ret;
        }

        static public BigInteger SumBigIntegers(List<BigInteger> list) {
            BigInteger ret = list[0];
            for (int i = 1; i < list.Count; i++) {
                ret = ret + list[i];
            }
            return ret;
        }

        static public BigInteger MinBigInteger(List<BigInteger> list) {
            BigInteger ret = list[0];
            for (int i = 1; i < list.Count; i++) {
                if (list[i] < ret)
                    ret = list[i];
            }
            return ret;
        }

        static public BigInteger MaxBigInteger(List<BigInteger> list) {
            BigInteger ret = list[0];
            for (int i = 1; i < list.Count; i++) {
                if (list[i] > ret)
                    ret = list[i];
            }
            return ret;
        }

        static public void WriteInts(List<int> list) {
            for (int i = 0; i < list.Count; i++) 
                System.Console.Write("{0}, ", list[i]);
            System.Console.WriteLine();
        }

        static public void WriteInts(int[] list) {
            for (int i = 0; i < list.Length; i++)
                System.Console.Write("{0}, ", list[i]);
            System.Console.WriteLine();
        }

        static public int LCM(int a, int b) {
            int num1, num2;
            if (a > b) {
                num1 = a; num2 = b;
            }
            else {
                num1 = b; num2 = a;
            }

            for (int i = 1; i < num2; i++) {
                int mult = num1 * i;
                if (mult % num2 == 0) {
                    return mult;
                }
            }
            return num1 * num2;
        }

        static public string Substring(string str, char c1, char c2) {
            int i1 = str.IndexOf(c1);
            if (i1 < 0)
                return string.Empty;
            int i2 = str.IndexOf(c2, i1 + 1);
            if (i2 < 0)
                return string.Empty;
            return str.Substring(i1 + 1, i2 - i1 - 1);
        }

        static public int FindNum(string str, int startPos) {
            char[] numChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            return str.IndexOfAny(numChars);
        }

        static public Dictionary<(int, int, int), char> Parse3DMap(string[] lines) {
            var ret = new Dictionary<(int, int, int), char>();
            for (int y = 0; y < lines.Length; y++) {
                for (int x = 0; x < lines[y].Length; x++) {
                    ret[(x, y, 0)] = lines[y][x];
                }
            }
            return ret;
        }      
    }
}
