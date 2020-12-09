using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AOC2020 {
    class Day09 {
        BigInteger[] _nums = Tools.Common.ReadBigIntegers("input09.txt");
        int preambleLen = 25;

        bool CheckNumber(int i) {
            for (int j = i - 1; j > (i - preambleLen); j--) {
                for (int k = j - 1; k >= (i - preambleLen); k--) {
                    if (_nums[i] == _nums[j] + _nums[k])
                        return true;
                }
            }

            return false;
        }

        BigInteger FindInvalid() {
            for (int i = preambleLen; i < _nums.Length; i++) {
                if (!CheckNumber(i)) {
                    return _nums[i];
                }
            }

            return -1;
        }
        public void PartOne() { 
            Console.WriteLine(FindInvalid());
        }

        List<BigInteger> FindList(BigInteger invalid) {
            List<BigInteger> actualList = new List<BigInteger>();
            for (int i = 0; i < _nums.Length; i++) {
                actualList.Add(_nums[i]);
                for (int j = i + 1; j < _nums.Length; j++) {
                    actualList.Add(_nums[j]);
                    var sum = Tools.Common.SumBigIntegers(actualList);
                    if (sum > invalid) {
                        actualList.Clear();
                        break;
                    }

                    if (sum == invalid) {
                        return actualList;
                    }
                }
            }
            return null;
        }
        public void PartTwo() {
            var invalid = FindInvalid();
            var list = FindList(invalid);
            var min = Tools.Common.MinBigInteger(list);
            var max = Tools.Common.MaxBigInteger(list);
            Console.WriteLine(min + max);
        }
    }
}
