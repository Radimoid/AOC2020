using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020 {
    class Day1 {
        int[] nums = Tools.Common.ReadInts("input01.txt");

        public void PartOne() {
            for (int i = 0; i < nums.Length; i++)
                for (int j = 0; j < nums.Length; j++) {
                    if (i != j) {
                        if (nums[i] + nums[j] == 2020) {
                            Console.WriteLine(nums[i] * nums[j]);
                            return;
                        }
                    }
                }
        }

        public void PartTwo() {
            for(int i = 0; i < nums.Length; i++)
                for(int j = 0; j < nums.Length; j++)
                    for(int k = 0; k < nums.Length; k++) {
                        if(i != j && i != k && j != k) {
                            if(nums[i] + nums[j] + nums[k] == 2020) {
                                Console.WriteLine(nums[i] * nums[j] * nums[k]);
                                return;
                            }
                        }
                    }
        }
    }
}
