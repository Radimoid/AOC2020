using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day16 {
        class Range {
            int _min;
            int _max;

            public Range(int min, int max) {
                _min = min;
                _max = max;
            }

            public bool Contains(int val) {
                return (_min <= val && val <= _max);
            }
        }
                
        void LineToRanges(string line, List<Range> ranges) {
            string[] s1 = line.Split(':');
            string[] s2 = s1[1].Split("or");
            string[] s3 = s2[0].Split("-");
            string[] s4 = s2[1].Split("-");

            int num1 = int.Parse(s3[0].Trim());
            int num2 = int.Parse(s3[1].Trim());
            int num3 = int.Parse(s4[0].Trim());
            int num4 = int.Parse(s4[1].Trim());

            ranges.Add(new Range(num1, num2));
            ranges.Add(new Range(num3, num4));
        }

        bool IsNumInRanges(int num, List<Range> ranges) {
            foreach (var range in ranges)
                if (range.Contains(num))
                    return true;
            return false;
        }
        bool IsTicketValid(int[] ticket, List<Range> ranges) {
            foreach (var num in ticket)
                if (!IsNumInRanges(num, ranges))
                    return false;
            return true;
        }

        public void PartOne() {
            var lines = Tools.Common.ReadLines("input16.txt");
            var ranges = new List<Range>();
            int[] myTicket = null;
            List<int[]> nearbyTickests = new List<int[]>();

            int state = 0;
            foreach (var line in lines) {
                if (string.IsNullOrEmpty(line)) {
                    state++;
                    continue;
                }

                switch (state) {
                    case 0:
                        LineToRanges(line, ranges);
                        break;
                    case 1: // přeskočí nadpis
                        state++;
                        break;
                    case 2:
                        myTicket = Tools.Common.ParseIntsLine(line);
                        break;
                    case 3: // přeskočí nadpis
                        state++;
                        break;
                    case 4:
                        nearbyTickests.Add(Tools.Common.ParseIntsLine(line));
                        break;
                }
            }

            int sum = 0;
            foreach (var nearbyTicket in nearbyTickests) {
                foreach (var num in nearbyTicket)
                    if (!IsNumInRanges(num, ranges))
                        sum = sum + num;
            }

            Console.WriteLine(sum);
        }

        class Field {
            string _title;
            Range _range1 = null;
            Range _range2 = null;

            public Field(string line) {
                string[] s1 = line.Split(':');
                string[] s2 = s1[1].Split("or");
                string[] s3 = s2[0].Split("-");
                string[] s4 = s2[1].Split("-");

                int num1 = int.Parse(s3[0].Trim());
                int num2 = int.Parse(s3[1].Trim());
                int num3 = int.Parse(s4[0].Trim());
                int num4 = int.Parse(s4[1].Trim());

                _title = s1[0];
                _range1 = new Range(num1, num2);
                _range2 = new Range(num3, num4);
            }

            public bool Contains(int num) {
                return _range1.Contains(num) || _range2.Contains(num);
            }

            public string Title() {
                return _title;
            }
        }

        bool IsNumInFields(int num, List<Field> fields) {
            foreach (var field in fields)
                if (field.Contains(num))
                    return true;
            return false;
        }
        bool IsTicketValid(int[] ticket, List<Field> fields) {
            foreach (var num in ticket)
                if (!IsNumInFields(num, fields))
                    return false;
            return true;
        }

        List<Field> FindUniqueField(List<int> nums, List<Field> fields) {
            List<Field> ret = new List<Field>();
            foreach (var field in fields) {                                   
                bool isOk = true;
                foreach (var num in nums) {                
                    if (!field.Contains(num)) {
                        isOk = false;
                        break;
                    }
                }

                if (isOk)
                    ret.Add(field);
            }

            return ret;
        }

        public void PartTwo() {
            var lines = Tools.Common.ReadLines("input16.txt");
            var fields = new List<Field>();
            int[] myTicket = null;
            List<int[]> nearbyTickests = new List<int[]>();
            var titleToField = new Dictionary<string, Field>();

            int state = 0;
            foreach (var line in lines) {
                if (string.IsNullOrEmpty(line)) {
                    state++;
                    continue;
                }

                switch (state) {
                    case 0:
                        var field = new Field(line);
                        fields.Add(field);
                        titleToField[field.Title()] = field;
                        break;
                    case 1: // přeskočí nadpis
                        state++;
                        break;
                    case 2:
                        myTicket = Tools.Common.ParseIntsLine(line);
                        break;
                    case 3: // přeskočí nadpis
                        state++;
                        break;
                    case 4:
                        var nearbeTicket = Tools.Common.ParseIntsLine(line);
                        if (IsTicketValid(nearbeTicket, fields))
                            nearbyTickests.Add(nearbeTicket);
                        break;
                }
            }


            var orderedFields = new List<Field>[fields.Count];
            
            for (int i = 0; i < myTicket.Length; i++) {
                List<int> nums = new List<int>();
                nums.Add(myTicket[i]);
                foreach (var nearbyTicket in nearbyTickests) {
                    nums.Add(nearbyTicket[i]);
                }
                orderedFields[i] = FindUniqueField(nums, fields);                
            }

            var used = new List<Field>();
            var orderedFields2 = new Field[fields.Count];
            for (int i = 1; i <= fields.Count; i++) {
                for (int j = 0; j < orderedFields.Length; j++) {
                    var list = orderedFields[j];
                    if (list.Count == i) {
                        foreach (var field in list) {
                            if (!used.Contains(field)) {
                                used.Add(field);
                                orderedFields2[j] = field;
                            }
                        }
                    }
                }
            }

            long num = 1;          
            for (int i = 0; i < fields.Count; i++) {
                if (orderedFields2[i].Title().StartsWith("departure"))
                    num *= myTicket[i];
            }
          

            Console.WriteLine(num);
        }
    }
}
