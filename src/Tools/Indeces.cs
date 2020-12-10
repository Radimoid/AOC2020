using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Tools {
    public class Indeces {
		int _indecesCount;
		int[] _result;
		
		public Indeces(int indecesCount, int resultLen) {
			_indecesCount = indecesCount;
			_result = new int[resultLen];
			for (int i = 0; i < _result.Length; i++)
				_result[i] = i;
		}

		public int[] Get() {
			return _result;
        }

		public bool IsOk() {
			return _result != null;
        }
				
        void Shift(int i) {
            i--;
            if (i < 0) {
                _result = null;
                return;
            }
            _result[i]++;

            for (int j = i + 1; j < _result.Length; j++) {
                _result[j] = _result[j - 1] + 1;
                if (_result[j] >= _indecesCount) {
                    Shift(i);
                    break;
                }
            }
        }

		public void Next() {
			int i = _result.Length - 1;
			_result[i]++;

            if (_result[i] >= _indecesCount) {
				Shift(i);
            }
        }
    }
}
