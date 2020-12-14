using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day14 {
        ulong _mask0 = 0;
        ulong _mask1 = 0;
        string _mask;

        Dictionary<ulong, ulong> _program = new Dictionary<ulong, ulong>();

        void UpdateMask(string maskStr) {
            _mask = maskStr;
            _mask0 = 0;
            _mask1 = 0;
            for (int i = 0; i < 36; i++) {
                if (maskStr[maskStr.Length - i - 1] == '1')
                    _mask1 |= 1u << i;
                else if (maskStr[maskStr.Length - i - 1] == '0')
                    _mask0 |= 1u << i;
            }
        }
                
        ulong UseMask(ulong value) {

            string str = Convert.ToString((long)value, 2);
            char[] ret = new char[36];

            
            for (int i = 0; i < 36; i++) {
                int indexSrc = str.Length - i - 1;
                int indexDst = 36 - i - 1;
                if (indexSrc >= 0)
                    ret[indexDst] = str[indexSrc];
                else
                    ret[indexDst] = '0';

                if (_mask[indexDst] == '1')
                    ret[indexDst] = '1';
                else if (_mask[indexDst] == '0')
                    ret[indexDst] = '0';
            }

            return Convert.ToUInt64(new string(ret), 2);

            /*
            ulong ret = value;
            ret |= _mask1;
            ret &= ~_mask0;
            return ret;
            */
        }

        public void PartOne() {
            var lines = Tools.Common.ReadLines("input14.txt");            
                        
            foreach (var line in lines) {
                var splitedLine = line.Split('=');

                if (line.StartsWith("mask")) {
                    UpdateMask(splitedLine[1].Trim());
                }
                else {                    
                    ulong addr = ulong.Parse(Tools.Common.Substring(splitedLine[0], '[', ']'));
                    ulong val = ulong.Parse(splitedLine[1].Trim());
                    val = UseMask(val);
                    _program[addr] = val;
                }
            }

            ulong sum = 0;
            foreach (var val in _program.Values) {
                sum += val;
            }

            Console.WriteLine(sum);
        }

        class MaskGenerator {
            List<int> _indeces = new List<int>();
            char[] _actualMask = new char[36];

            public MaskGenerator(string pattern) {
                for (int i = 0; i < pattern.Length; i++) {
                    if (pattern[i] == 'X') {
                        _indeces.Add(i);
                        _actualMask[i] = '0';
                    }
                    else {
                        _actualMask[i] = pattern[i];
                    }
                }
            }

            public string Get() {
                return new string(_actualMask);
            }

            public bool Next() {
                for (int i = 0; i < _indeces.Count; i++) {
                    if (_actualMask[_indeces[i]] == '0') {
                        _actualMask[_indeces[i]] = '1';
                        return true;
                    }
                    else {
                        _actualMask[_indeces[i]] = '0';
                    }
                }

                return false;
            }             
        }

        List<ulong> GetAddresses(ulong address) {
            string str = Convert.ToString((long)address, 2);
            char[] chars = new char[36];


            for (int i = 0; i < 36; i++) {
                int indexSrc = str.Length - i - 1;
                int indexDst = 36 - i - 1;
                if (indexSrc >= 0)
                    chars[indexDst] = str[indexSrc];
                else
                    chars[indexDst] = '0';

                if (_mask[indexDst] == '1')
                    chars[indexDst] = '1';
                else if (_mask[indexDst] == 'X')
                    chars[indexDst] = 'X';
            }

            List<ulong> ret = new List<ulong>();
            MaskGenerator maskGenerator = new MaskGenerator(new string(chars));
            do {
                ret.Add(Convert.ToUInt64(maskGenerator.Get(), 2));
            }
            while (maskGenerator.Next());

            
            return ret;
        }

        public void PartTwo() {
            var lines = Tools.Common.ReadLines("input14.txt");

            foreach (var line in lines) {
                var splitedLine = line.Split('=');

                if (line.StartsWith("mask")) {
                    UpdateMask(splitedLine[1].Trim());
                }
                else {
                    var addrs = GetAddresses(ulong.Parse(Tools.Common.Substring(splitedLine[0], '[', ']')));
                    ulong val = ulong.Parse(splitedLine[1].Trim());
                    foreach (var addr in addrs)
                        _program[addr] = val;
                }
            }

            ulong sum = 0;
            foreach (var val in _program.Values) {
                sum += val;
            }

            Console.WriteLine(sum);
        }
    }
}
