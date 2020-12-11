using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day11 {
        Tools.Map _seats = new Tools.Map(Tools.Common.ReadLines("input11.txt"));
        bool _partTwo = false;

        static readonly List<Tuple<int, int>> _directions = new List<Tuple<int, int>> {
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(0, -1),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(-1, 0),

            new Tuple<int, int>(1, 1),
            new Tuple<int, int>(1, -1),
            new Tuple<int, int>(-1, -1),
            new Tuple<int, int>(-1, 1),
        };

        const char _occupied = '#';
        const char _empty = 'L';
        const char _floor = '.';
        const char _boundary = 'x';


        char CurrentState(int x, int y) {
            if (x < 0 || y < 0 || x >= _seats.Cols() || y >= _seats.Rows())
                return _boundary;
            return _seats.Get(x, y);
        }

        char CurrentState(int x, int y, Tuple<int, int> direction) {
            if (!_partTwo) {
                return CurrentState(x + direction.Item1, y + direction.Item2);
            }
            else {
                char ret = _floor;
                while (ret == _floor) {
                    x = x + direction.Item1;
                    y = y + direction.Item2;
                    ret = CurrentState(x, y);
                }
                return ret;
            }            
        }

        bool OccupiedExceed(int x, int y, int max) {
            int cnt = 0;
            foreach (var direction in _directions) {
                if (CurrentState(x, y, direction) == _occupied) {
                    cnt++;
                    if (cnt > max)
                        return true;
                }
            }
            return false;
        }

        
        char NewState(int x, int y) {
            var current = CurrentState(x, y);
            if (current == _empty) {
                if (!OccupiedExceed(x, y, 0))
                    return _occupied;
            }
            else if (current == _occupied) {
                if (OccupiedExceed(x, y, _partTwo ? 4 : 3))
                    return _empty;
            }
            return current;
        }

        Tools.Map ApplyModel() {
            var ret = new Tools.Map(_seats.Cols(), _seats.Rows());
            for (int x = 0; x < ret.Cols(); x++) {
                for (int y = 0; y < ret.Rows(); y++) {
                    ret.Set(x, y, NewState(x, y));
                }
            }
            return ret;
        }

        public void PartOne() {
            while (true) {
                //_seats.Print();
                //Console.WriteLine();
                //Console.ReadKey();
                var next = ApplyModel();                
                if (next.Equals(_seats))
                    break;

                _seats = next;
            }

            int cnt = 0;
            for (int x = 0; x < _seats.Cols(); x++)
                for (int y = 0; y < _seats.Rows(); y++)
                    if (_seats.Get(x, y) == _occupied)
                        cnt++;

            Console.WriteLine(cnt);
        }

        public void PartTwo() {
            _partTwo = true;
            PartOne();
        }
    }
}
