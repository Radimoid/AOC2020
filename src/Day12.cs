using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
       
    class Day12 {
        struct Instruction {
            public char cmd;
            public int val;

            static public Instruction Parse(string str) {
                Instruction ret = new Instruction();
                ret.cmd = str[0];
                ret.val = int.Parse(str.Substring(1));
                return ret;
            }
        }

        static Instruction[] Parse(string[] strs) {
            var ret = new Instruction[strs.Length];
            for (int i = 0; i < ret.Length; i++) {
                ret[i] = Instruction.Parse(strs[i]);
            }
            return ret;
        }


        int _face = 0;
        int _x = 0;
        int _y = 0;
        int _wx = 10;
        int _wy = -1;

        Instruction[] _instructions = Parse(Tools.Common.ReadLines("input12.txt"));
        readonly char[] _faces = new char[] { 'E', 'S', 'W', 'N' };

        int DecodeTurnVal(int val) {
            if (val == 90)
                return 1;
            else if (val == 180)
                return 2;
            else if (val == 270)
                return 3;

            throw new Exception();
        }

        void TurnLeft(int val) {
            _face -= DecodeTurnVal(val);
            if (_face < 0)
                _face += 4;            
        }

        void TurnRight(int val) {
            _face += DecodeTurnVal(val);
            if (_face >= 4)
                _face -= 4;
        }

        void ExecuteInstruction(Instruction instruction) {
            switch (instruction.cmd) {
                case 'N':
                    _y -= instruction.val;
                    break;
                case 'S':
                    _y += instruction.val;
                    break;
                case 'E':
                    _x += instruction.val;
                    break;
                case 'W':
                    _x -= instruction.val;
                    break;
                case 'L':
                    TurnLeft(instruction.val);
                    break;
                case 'R':
                    TurnRight(instruction.val);
                    break;
                case 'F':
                    instruction.cmd = _faces[_face];
                    ExecuteInstruction(instruction);
                    break;
            }
        }

        void ExecuteInstruction2(Instruction instruction) {
            switch (instruction.cmd) {
                case 'N':
                    _wy -= instruction.val;
                    break;
                case 'S':
                    _wy += instruction.val;
                    break;
                case 'E':
                    _wx += instruction.val;
                    break;
                case 'W':
                    _wx -= instruction.val;
                    break;
                case 'L':
                    for (int i = 0; i < DecodeTurnVal(instruction.val); i++) {
                        int tmp = _wx;
                        _wx = _wy;
                        _wy = -tmp;
                    }
                    break;
                case 'R':
                    for (int i = 0; i < DecodeTurnVal(instruction.val); i++) {
                        int tmp = _wx;
                        _wx = -_wy;
                        _wy = tmp;
                    }
                    break;
                case 'F':
                    _x += instruction.val * _wx;
                    _y += instruction.val * _wy;
                    break;
            }
        }

        public void PartOne() {
            foreach (var instruction in _instructions) {
                ExecuteInstruction(instruction);
            }

            Console.WriteLine(Math.Abs(_x) + Math.Abs(_y));
        }

        public void PartTwo() {
            foreach (var instruction in _instructions) {
                ExecuteInstruction2(instruction);
            }

            Console.WriteLine(Math.Abs(_x) + Math.Abs(_y));
        }
    }
}
