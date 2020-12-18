using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day18 {
                
        class ExpressEval {
            int _index = 0;
            string _express = null;

            long MakeOpe(long num1, long num2, int ope) {
                switch (ope) {
                    case 0:
                        return num1 + num2;
                    case 1:
                        return num1 * num2;
                }

                throw new Exception();
            }

            void SkipSpaces() {
                while (_index < _express.Length && _express[_index] == ' ')
                    _index++;
            }
            long ReadNum() {
                SkipSpaces();

                char[] chars = new char[] { ' ', ')' };
                int i2 = _express.IndexOfAny(chars, _index + 1);

                long ret = 0;
                if (i2 > -1) {
                    var s = _express.Substring(_index, i2 - _index);
                    ret = long.Parse(s);
                    _index = i2;                    
                }
                else {
                    var s = _express.Substring(_index);
                    ret = long.Parse(s);
                    _index = _express.Length;
                }

                return ret;
            }

            int ReadOpe() {
                SkipSpaces();

                int ret = 0;
                char c = _express[_index];
                if (c == '+')
                    ret = 0;
                else if (c == '*')
                    ret = 1;
                else
                    throw new Exception();
                _index += 2;
                return ret;
            }

            public ExpressEval(string express) {
                _express = express;
            }

            public long Evaluate() {
                long ret = 0;
                if (_express[_index] == '(') {
                    _index++;
                    ret = Evaluate();
                }
                else {
                    ret = ReadNum();
                }
                
                while (_index < _express.Length) {
                    if (_express[_index] == ')') {
                        _index++;
                        return ret;
                    }

                    int ope = ReadOpe();

                    long num2 = 0;
                    if (_express[_index] == '(') {
                        _index++;
                        num2 = Evaluate();
                    }
                    else if (_express[_index] == ')') {
                        _index++;
                        return ret;
                    }
                    else {
                        num2 = ReadNum();
                    }
                                        
                    ret = MakeOpe(ret, num2, ope);
                }

                return ret;
            }
        }

        public void PartOne() {
            var lines = Tools.Common.ReadLines("input18.txt");
            long sum = 0;
            foreach (var line in lines) {
                var eval = new ExpressEval(line);
                sum += eval.Evaluate();
            }

            Console.WriteLine(sum);
        }

        class ExpressEval2 {
            int _index = 0;
            string _express = null;
                        
            void SkipSpaces() {
                while (_index < _express.Length && _express[_index] == ' ')
                    _index++;
            }
            long ReadNum() {
                SkipSpaces();

                char[] chars = new char[] { ' ', ')' };
                int i2 = _express.IndexOfAny(chars, _index + 1);

                long ret = 0;
                if (i2 > -1) {
                    var s = _express.Substring(_index, i2 - _index);
                    ret = long.Parse(s);
                    _index = i2;
                }
                else {
                    var s = _express.Substring(_index);
                    ret = long.Parse(s);
                    _index = _express.Length;
                }

                return ret;
            }

            int ReadOpe() {
                SkipSpaces();

                int ret = 0;
                char c = _express[_index];
                if (c == '+')
                    ret = 0;
                else if (c == '*')
                    ret = 1;
                else
                    throw new Exception();
                _index += 2;
                return ret;
            }
                        
            public long Evaluate(string express) {
                var ir = express.IndexOf(')');
                if (ir < 0) return Evaluate2(express);

                var il = express.LastIndexOf('(', ir);
                if (il < 0) throw new Exception();


                var len = ir - il + 1;

                var one = express.Substring(il + 1, ir - 1 - il);
                var res = Evaluate2(one);

                express = express.Remove(il, len);
                express = express.Insert(il, res.ToString());

                return Evaluate(express);
            }         

            public long Evaluate2(string express) {
                _express = express;
                _index = 0;
               
                List<long> nums = new List<long>();
                long num = ReadNum();
                while (_index < express.Length) {
                    int ope = ReadOpe();
                    if (ope == 0) {
                        num += ReadNum();
                    }
                    else if (ope == 1) {
                        nums.Add(num);
                        num = ReadNum();                        
                    }
                }
                nums.Add(num);
                long sum = 1;
                foreach (var n in nums) {
                    sum *= n;
                }
                
                return sum;                
            }
        }

        public void PartTwo() {
            var lines = Tools.Common.ReadLines("input18.txt");
            long sum = 0;
            foreach (var line in lines) {
                var eval = new ExpressEval2();
                sum += eval.Evaluate(line);
            }

            Console.WriteLine(sum);
        }
    }
}
