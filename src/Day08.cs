using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day08 {
        Tools.Computer _computer = new Tools.Computer(Tools.Common.ReadLines("input08.txt"));

        public void PartOne() {
            _computer.RunAndCheckRepetition();
            Console.WriteLine(_computer.GetAccumulator());
        }

        public void PartTwo() {
            Tools.ProgramModifier modifier = new Tools.ProgramModifier(_computer.GetProgram());
            while (modifier.InterchangeNext("jmp", "nop")) {
                if (_computer.RunAndCheckRepetition())
                    break;
                modifier.Interchange("jmp", "nop");
            }
            Console.WriteLine(_computer.GetAccumulator());
        }
    }
}
