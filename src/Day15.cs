using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day15 {
        int[] _input = Tools.Common.ReadIntsRow("input15.txt");
            
        int GetNext(List<int> nums) {
            int i1 = nums.Count - 1;
            int num1 = nums[i1];
            for (int i = i1 - 1; i >= 0; i--) {
                if (nums[i] == num1)
                    return i1 - i;
            }
            return 0;
        }
        public void PartOne() {
            List<int> nums = new List<int>(_input);
            for (int iTurn = _input.Length; iTurn < 2020; iTurn++) {
                nums.Add(GetNext(nums));
            }

            Console.WriteLine(nums.Last());
        }

        public void PartTwo() {
            List<int> nums = new List<int>(_input);
            for (int iTurn = _input.Length; iTurn < 30000000; iTurn++) {
                nums.Add(GetNext(nums));
            }

            Console.WriteLine(nums.Last());
        }
    }
}
