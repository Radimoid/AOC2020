using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Tools {
    class Map {
        char[,] _map;

        public Map(string[] lines) {
            _map = Common.StrsToChars(lines);
        }

        public Map(int cols, int rows) {
            _map = new char[cols, rows];
        }

        public int Rows() {
            return _map.GetUpperBound(1) + 1;
        }

        public int Cols() {
            return _map.GetUpperBound(0) + 1;
        }

        public char Get(int x, int y) {
            return _map[x, y];
        }

        public void Set(int x, int y, char c) {
            _map[x, y] = c;
        }

        public bool Equals(Map other) {            
            for (int x = 0; x < Cols(); x++)
                for (int y = 0; y < Rows(); y++)
                    if (_map[x, y] != other._map[x, y])
                        return false;
            return true;
        }

        public void Print() {
            for (int row = 0; row < Rows(); row++) {
                for (int col = 0; col < Cols(); col++) {
                    Console.Write(_map[col, row]);
                }
                Console.WriteLine();
            }
        }
    }
}
