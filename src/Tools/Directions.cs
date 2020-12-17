using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Tools {
    public class Directions {
        int[] _actual = new int[3] { -1, -1, -1 };

        public bool Next() {
            for (int i = 0; i < 3; i++) {
                _actual[i]++;
                if (_actual[i] <= 1)
                    return true;
                else
                    _actual[i] = -1;
            }
            return false;
        }
        public (int, int, int) Get() {
            return (_actual[0], _actual[1], _actual[2]);
        }

        public List<(int, int, int)> GetAll() {
            var ret = new List<(int, int, int)>();
            do {
                var direction = Get();
                if (direction != (0, 0, 0))
                    ret.Add(direction);
            } while (Next());
            return ret;
        }

        static public List<(int, int, int)> Generate() {
            var directions = new Directions();
            return directions.GetAll();
        }
    }

    public class DirectionsEx {
        Coordination _actual = null;

        DirectionsEx(int dim) {
            _actual = new Coordination(dim);
            for (int i = 0; i < _actual.Dim(); i++) {
                _actual[i] = -1;
            }               
        }

        bool IsNulDirection() {
            for (int i = 0; i < _actual.Dim(); i++) {
                if (_actual[i] != 0)
                    return false;
            }
            return true;
        }

        public bool Next() {
            for (int i = 0; i < _actual.Dim(); i++) {
                _actual[i]++;
                if (_actual[i] <= 1)
                    return true;
                else
                    _actual[i] = -1;
            }
            return false;
        }
        public Coordination Get() {
            return _actual;
        }

        public List<Coordination> GetAll() {
            var ret = new List<Coordination>();
            do {                
                if (!IsNulDirection())
                    ret.Add((Coordination)_actual.Clone());
            } while (Next());
            return ret;
        }

        static public List<Coordination> Generate(int dim) {
            var directions = new DirectionsEx(dim);
            return directions.GetAll();
        }
    }
}
