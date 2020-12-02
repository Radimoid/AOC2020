using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020 {
    class Day2 {
        struct PasswordPolicy {
            public int num1;
            public int num2;
            public char c;
            public string password;
        }

        static PasswordPolicy StrToPasswordPolicy(string str) {
            Tokenizer tok = new Tokenizer(str);
            PasswordPolicy ret;
            ret.num1 = int.Parse(tok.Next("-"));
            ret.num2 = int.Parse(tok.Next(" "));
            ret.c = char.Parse(tok.Next(": "));
            ret.password = tok.Next();
            return ret;
        }

        static PasswordPolicy[] ParseLines(string[] lines) {
            PasswordPolicy[] ret = new PasswordPolicy[lines.Length];
            for(int i = 0; i < ret.Length; i++)
                ret[i] = StrToPasswordPolicy(lines[i]);
            return ret;
        }

        static PasswordPolicy[] ReadPasswordPolicies(string input) {
            return ParseLines(Common.ReadLines(input));
        }

        static bool CheckPasswordPolicy(PasswordPolicy passwordPolicy) {
            int count = 0;
            foreach(char c in passwordPolicy.password)
                if(c == passwordPolicy.c)
                    count++;
            return count >= passwordPolicy.num1 && count <= passwordPolicy.num2;
        }

        static bool CheckPasswordPolicy2(PasswordPolicy passwordPolicy) {
            int matchCount = 0;
            if(passwordPolicy.password[passwordPolicy.num1 - 1] == passwordPolicy.c)
                matchCount++;
            if(passwordPolicy.password[passwordPolicy.num2 - 1] == passwordPolicy.c)
                matchCount++;
            return matchCount == 1;
        }

        PasswordPolicy[] passwordPolicies = ReadPasswordPolicies("input2.txt");

        public void PartOne() {
            int count = 0;
            foreach(PasswordPolicy passwordPolicy in passwordPolicies)
                if(CheckPasswordPolicy(passwordPolicy))
                    count++;
            Console.WriteLine(count);
        }

        public void PartTwo() {
            int count = 0;
            foreach(PasswordPolicy passwordPolicy in passwordPolicies)
                if(CheckPasswordPolicy2(passwordPolicy))
                    count++;
            Console.WriteLine(count);
        }
    }
}
