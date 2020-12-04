using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020 {
    class Day04 {
        class Passport : Dictionary<string, string> {
        }

        static List<Passport> ReadPassports(string[] lines) {
            var passportsList = new List<Passport>();
            var passport = new Passport();
            foreach (string line in lines) {
                if (string.IsNullOrEmpty(line)) {
                    passportsList.Add(passport);
                    passport = new Passport();
                    continue;
                }

                var pairs = line.Split(' ');
                foreach (string pair in pairs) {
                    var splittedPair = pair.Split(':');
                    passport.Add(splittedPair[0], splittedPair[1]);
                }
            }
            passportsList.Add(passport);
            return passportsList;
        }

        List<Passport> _passports = ReadPassports(Tools.Common.ReadLines("input04.txt"));

        readonly string[] requiredKeys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        bool HasAllRequired(Passport passport) {
            foreach (string required in requiredKeys) {
                if (!passport.ContainsKey(required))
                    return false;
            }

            return true;
        }


        void CheckInt(string str, int digits, int min, int max) {
            if (str.Length != digits)
                throw new Exception();
            Tools.Common.CheckInt(int.Parse(str), min, max);
        }

        void CheckHgt(string str) {
            if (str.EndsWith("cm")) {
                Tools.Common.CheckInt(int.Parse(str.Substring(0, str.Length - 2)), 150, 193);
            }
            else if (str.EndsWith("in")) {
                Tools.Common.CheckInt(int.Parse(str.Substring(0, str.Length - 2)), 59, 76);
            }
            else {
                throw new Exception();
            }
        }

        void CheckHcl(string str) {
            if (str.Length != 7)
                throw new Exception();
            if (str[0] != '#')
                throw new Exception();
            for (int i = 1; i <= 6; i++) {
                if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'a' && str[i] <= 'f')) {
                    // ok
                }
                else {
                    throw new Exception();
                }
            }
        }

        void CheckEcl(string str) {
            List<string> allowed = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (!allowed.Contains(str))
                throw new Exception();
        }

        void CheckPid(string str) {
            if (str.Length != 9)
                throw new Exception();
            for (int i = 0; i < 9; i++) {
                if (str[i] >= '0' && str[i] <= '9') {
                    // ok
                }
                else {
                    throw new Exception();
                }
            }
        }


        bool HasAllValid(Passport passport) {
            try {
                CheckInt(passport["byr"], 4, 1920, 2002);
                CheckInt(passport["iyr"], 4, 2010, 2020);
                CheckInt(passport["eyr"], 4, 2020, 2030);
                CheckHgt(passport["hgt"]);
                CheckHcl(passport["hcl"]);
                CheckEcl(passport["ecl"]);
                CheckPid(passport["pid"]);
            }
            catch (Exception e) {
                return false;
            }

            return true;
        }

        public void PartOne() {
            int cnt = 0;
            foreach (var passport in _passports) {
                if (HasAllRequired(passport))
                    cnt++;
            }
            Console.WriteLine(cnt);
        }

        public void PartTwo() {
            int cnt = 0;
            foreach (var passport in _passports) {
                if (HasAllValid(passport))
                    cnt++;
            }
            Console.WriteLine(cnt);
        }
    }
}
