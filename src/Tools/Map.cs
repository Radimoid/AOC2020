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

        public Map(char[,] map) {
            _map = map;
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

        public string GetRow(int y) {
            List<char> chars = new List<char>();
            for (int x = 0; x < Cols(); x++)
                chars.Add(_map[x, y]);
            return new string(chars.ToArray());
        }

        public string GetCol(int x) {
            List<char> chars = new List<char>();
            for (int y = 0; y < Rows(); y++)
                chars.Add(_map[x, y]);
            return new string(chars.ToArray());
        }

        public string FirstRow() {
            return GetRow(0);
        }

        public string LastRow() {
            return GetRow(Rows() - 1);
        }

        public string FirstCol() {
            return GetCol(0);
        }
        public string LastCol() {
            return GetCol(Cols() - 1);
        }

        public Map Identity() {
            char[,] newMatrix = new char[Cols(), Rows()];
            for (int row = 0; row < Rows(); row++) {
                for (int col = 0; col < Cols(); col++) {
                    newMatrix[col, row] = _map[col, row];
                }
            }
            return new Map(newMatrix);

        }
        public Map Rotate() {
            char[,] newMatrix = new char[_map.GetLength(1), _map.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = _map.GetLength(1) - 1; oldColumn >= 0; oldColumn--) {
                newColumn = 0;
                for (int oldRow = 0; oldRow < _map.GetLength(0); oldRow++) {
                    newMatrix[newRow, newColumn] = _map[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return new Map(newMatrix);
        }

        public Map FlipVer() {
            char[,] newMatrix = new char[Cols(), Rows()];
            for (int row = 0; row < Rows(); row++) {
                for (int col = 0; col < Cols(); col++) {
                    newMatrix[col, Rows() - row - 1] = _map[col, row];
                }
            }
            return new Map(newMatrix);
        }

        public Map FlipHor() {
            char[,] newMatrix = new char[Cols(), Rows()];
            for (int row = 0; row < Rows(); row++) {
                for (int col = 0; col < Cols(); col++) {
                    newMatrix[Cols() - col - 1, row] = _map[col, row];
                }
            }
            return new Map(newMatrix);
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
