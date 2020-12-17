using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.src.Tools {
    class Map3D {
        Dictionary<(int, int, int), char> _map = new Dictionary<(int, int, int), char>();

        public Map3D(string[] lines, int z = 0) {
            for (int y = 0; y < lines.Length; y++) {
                for (int x = 0; x < lines[y].Length; x++) {
                    _map[(x, y, z)] = lines[y][x];
                }
            }            
        }
    }
}
