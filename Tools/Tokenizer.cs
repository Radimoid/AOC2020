using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020.Tools {
    class Tokenizer {
        string _str;
        int _index;

        public Tokenizer(string str) {
            _str = str;
        }

        public string Next(string separator) {
            int startIndex = _index;
            _index = _str.IndexOf(separator, startIndex);
            string ret = _str.Substring(startIndex, _index - startIndex);
            _index += separator.Length;
            return ret;
        }

        public string Next() {
            return _str.Substring(_index);
        }
    }
}
