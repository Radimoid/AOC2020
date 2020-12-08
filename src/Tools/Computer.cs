using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Tools {
    class Computer {
        public class Program {
            public struct Instruction {
                public string operation;
                public int operand;

                public Instruction(string operation, int operand) {
                    this.operation = operation;
                    this.operand = operand;
                }
            };

            static Instruction ParseLine(string line) {
                string[] splitted = line.Split(' ');
                return new Instruction(splitted[0], int.Parse(splitted[1]));
            }
            
            Instruction[] _instructions;

            public Program(string[] lines) {
                _instructions = new Instruction[lines.Length];

                for (int i = 0; i < lines.Length; i++) {
                    _instructions[i] = ParseLine(lines[i]);
                }
            }

            public Instruction[] GetInstructions() {
                return _instructions;
            }
        };

        

        int _accumulator = 0;
        int _index = 0;
        Program _program = null;
       

        public Computer(string[] lines) {
            _program = new Program(lines);
        }

        public Computer(Program program) {
            _program = program;
        }

        void ProcessInstruction(Program.Instruction instruction) {
            switch (instruction.operation) {
                case "nop":
                    _index++;
                    break;
                case "acc":
                    _accumulator += instruction.operand;
                    _index++;
                    break;
                case "jmp":
                    _index += instruction.operand;
                    break;
            }
        }
               
        public void Reset() {
            _accumulator = 0;
            _index = 0;
        }

        public bool RunAndCheckRepetition() {
            Reset();
            var instructions = _program.GetInstructions();
            bool[] executed = Enumerable.Repeat<bool>(false, instructions.Length).ToArray();
            while (true) {
                executed[_index] = true;
                ProcessInstruction(instructions[_index]);
                if (_index >= instructions.Length)
                    return true;
                if (executed[_index])
                    return false;                
            }
        }

        public int GetAccumulator() {
            return _accumulator;
        }

        public Program GetProgram() {
            return _program;
        }
    }
}
