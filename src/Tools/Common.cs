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

        public static string[] ReadLines(string input) {
            return System.IO.File.ReadAllLines(@"..\..\..\inputs\" + input);
        }

        public static int[] ReadInts(string input) {
            return StrsToInts(ReadLines(input));
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
    }
}
