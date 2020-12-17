using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Tools {
    public class Coordination : IComparable, ICloneable {
        public int[] _coord;

        public Coordination(int dim) {
            _coord = new int[dim];
        }

        public Coordination(int[] coord) {
            _coord = coord;
        }

        int IComparable.CompareTo(object obj) {
            var other = (Coordination)obj;
            for (int i = 0; i < _coord.Length; i++)
                if (_coord[i] != other._coord[i])
                    return -1;
            return 0;
        }

        public int Dim() {
            return _coord.Length;
        }

        public object Clone() {
            return new Coordination((int[])_coord.Clone());
        }

        public int this[int index] {
            get => _coord[index];
            set => _coord[index] = value;
        }

        public static Coordination operator+(Coordination a, Coordination b) {
            var ret = new Coordination(a._coord.Length);
            for (int i = 0; i < ret.Dim(); i++) {
                ret[i] = a[i] + b[i];
            }
            return ret;
        }

        public bool Equals(Coordination other) {
            for (int i = 0; i < _coord.Length; i++)
                if (_coord[i] != other._coord[i])
                    return false;
            return true;
        }
    }
}
