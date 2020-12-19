using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AOC2020 {
    class Day19 {
        class Rule {
            string _str;
            List<int[]> _subrules = null;

            public Rule(string str) {
                _str = Tools.Common.Substring(str, '"', '"');
                if (!string.IsNullOrEmpty(_str))
                    return;

                _subrules = new List<int[]>();
                var parts = str.Split('|');
                foreach (var part in parts) {
                    _subrules.Add(Tools.Common.ParseIntsLine(part.Trim(), ' '));
                    var nums = part.Trim().Split(' ');
                }
            }

            public void AddSubrule(int[] subrule) {
                _subrules.Add(subrule);
            }

            public List<string> MakeCombinations(Dictionary<int, Rule> rules) {
                var ret = new List<string>();

                if (!string.IsNullOrEmpty(_str)) {
                    ret.Add(_str);
                }
                else {
                    foreach (var subrule in _subrules) {
                        var subret = new List<string>();
                        foreach (var num in subrule) {
                            List<string> combinations = rules[num].MakeCombinations(rules);
                            if (subret.Count == 0) {
                                subret = combinations;
                            }
                            else {
                                var subret2 = new List<string>();
                                foreach (var prev in subret) {
                                    foreach (var combination in combinations) {
                                        subret2.Add(prev + combination);
                                    }
                                }
                                subret = subret2;
                            }
                        }
                        ret.AddRange(subret);
                    }
                }

                return ret;
            }

            public List<string> MakeCombinations(Dictionary<int, Rule> rules, string msg, int ruleNum) {
                var ret = new List<string>();

                if (!string.IsNullOrEmpty(_str)) {
                    if (msg.StartsWith(_str))
                        ret.Add(_str);
                }
                else {
                    foreach (var subrule in _subrules) {
                        var subret = new List<string>();
                        for (int i = 0; i < subrule.Length; i++) { 
                            int num = subrule[i];
                            if (i == 0) {
                                List<string> combinations = rules[num].MakeCombinations(rules, msg, num);
                                foreach (var combination in combinations) {
                                    if (msg.StartsWith(combination)) {
                                        subret.Add(combination);
                                    }
                                }
                            }
                            else {
                                var subret2 = new List<string>();
                                foreach (var msgBegin in subret) {
                                    if (msgBegin.Length >= msg.Length)
                                        continue;
                                    var msgEnd = msg.Substring(msgBegin.Length);
                                    List<string> combinations = rules[num].MakeCombinations(rules, msgEnd, num);                                    
                                    foreach (var combination in combinations) {
                                        if (msgEnd.StartsWith(combination)) {
                                            subret2.Add(msgBegin + combination);
                                        }
                                    }
                                }
                                subret = subret2;
                            }
                            if (subret.Count == 0) {
                                break;
                            }
                        }

                        ret.AddRange(subret);
                    }
                }

                return ret;
            }

            public bool IsValidMessage(Dictionary<int, Rule> rules, string msg, int ruleNum) {
                var combinations = MakeCombinations(rules, msg, ruleNum);
                foreach (var combination in combinations)
                    if (combination == msg)
                        return true;
                return false;
            }
        }

        Dictionary<int, Rule> ReadRules(string[] lines) {
            var ret = new Dictionary<int, Rule>();
            foreach (var line in lines) {
                var splited = line.Split(':');
                var key = int.Parse(splited[0]);
                var value = new Rule(splited[1]);
                ret.Add(key, value);
            }
            return ret;
        }

        public void PartOne() {
            var alltext = Tools.Common.ReadAllText("input19.txt");
            var items = alltext.Split("\r\n\r\n");
            var rules = ReadRules(items[0].Split("\r\n"));
            var messages = items[1].Split("\r\n");
            var combinations = rules[0].MakeCombinations(rules);

            foreach (var combination in combinations) {
                Console.WriteLine(combination);
            }
            Console.WriteLine();

            int sum = 0;
            foreach (var message in messages) {
                if (combinations.Contains(message)) {
                    Console.WriteLine(message);
                    sum++;
                }
            }
            Console.WriteLine();

            //int[] ns = new int[] { 8, 42, 31, 11 };
            foreach (var n in rules.Keys) {
                Console.WriteLine(n);
                var combinations2 = rules[n].MakeCombinations(rules);
                foreach (var combination2 in combinations2) {
                    Console.WriteLine(combination2);
                }
                Console.WriteLine();

            }
            Console.WriteLine(sum);
        }
        public void PartTwo() {
            var alltext = Tools.Common.ReadAllText("input19.txt");
            var items = alltext.Split("\r\n\r\n");
            var rules = ReadRules(items[0].Split("\r\n"));
            var messages = items[1].Split("\r\n");

            var keys = rules.Keys.ToList();
            keys.Sort();
            foreach (var n in keys) {
                Console.WriteLine(n);
                var combinations2 = rules[n].MakeCombinations(rules);
                if (combinations2.Count < 20) {
                    foreach (var combination2 in combinations2) {
                        Console.WriteLine(combination2);
                    }
                }
            
                Console.WriteLine();
            }           

            
            rules[8].AddSubrule(new int[] { 42, 8 });
            rules[11].AddSubrule(new int[] { 42, 11, 31 });
            /*
            List<string> matches = new List<string> {
                "bbabbbbaabaabba",
"babbbbaabbbbbabbbbbbaabaaabaaa",
"aaabbbbbbaaaabaababaabababbabaaabbababababaaa",
"bbbbbbbaaaabbbbaaabbabaaa",
"bbbababbbbaaaaaaaabbababaaababaabab",
"ababaaaaaabaaab",
"ababaaaaabbbaba",
"baabbaaaabbaaaababbaababb",
"abbbbabbbbaaaababbbbbbaaaababb",
"aaaaabbaabaaaaababaa",
"aaaabbaabbaaaaaaabbbabbbaaabbaabaaa",
"aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba",
            };

            bool test = rules[0].IsValidMessage(rules, "abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa", 0);
            */
            int sum = 0;
            foreach (var message in messages) {
                if (rules[0].IsValidMessage(rules, message, 0)) {
                    /*if (!matches.Contains(message))
                        Console.WriteLine(message);*/
                    sum++;
                }
            }
            
            Console.WriteLine(sum);
        }
    }
}
