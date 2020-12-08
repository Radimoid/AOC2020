using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Tools {
    class ProgramModifier {
        int _interchangeIndex = -1;
        Computer.Program.Instruction[] _instructions;

        public ProgramModifier(Computer.Program program) {
            _instructions = program.GetInstructions();
        }

        public bool Interchange(string first, string second) {
            if (_instructions[_interchangeIndex].operation == first) {
                _instructions[_interchangeIndex].operation = second;
                return true;
            }

            if (_instructions[_interchangeIndex].operation == second) {
                _instructions[_interchangeIndex].operation = first;
                return true;
            }

            return false;
        }

        public bool InterchangeNext(string first, string second) {
            _interchangeIndex++;
            for (; _interchangeIndex < _instructions.Length; _interchangeIndex++) {
                if (Interchange(first, second))
                    return true;
            }

            return false;
        }
    }
}
