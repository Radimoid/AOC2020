using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day17 {
        List<Tools.Coordination> _directions = null;
        List<Tools.Coordination> _active = null;
        
        void ReadInput(int dim) {
            _directions = Tools.DirectionsEx.Generate(dim);
            var lines = Tools.Common.ReadLines("input17.txt");
            _active = new List<Tools.Coordination>();
            for (int y = 0; y < lines.Length; y++) {
                for (int x = 0; x < lines[y].Length; x++) {                    
                    if (lines[y][x] == '#') {
                        var coord = new Tools.Coordination(dim);
                        for (int i = 0; i < dim; i++)
                            coord[i] = 0;
                        coord[0] = x;
                        coord[1] = y;
                        _active.Add(coord);
                    }                        
                }
            }
        }
            
        bool IsInList(Tools.Coordination cube, List<Tools.Coordination> list) {
            foreach (var coord in list)
                if (coord.Equals(cube))
                    return true;
            return false;
        }
        bool IsActive(Tools.Coordination cube) {
            return IsInList(cube, _active);
        }

        int NumActiveNeighbours(Tools.Coordination cube) {
            int num = 0;
            foreach (var direction in _directions) {
                var coord = cube + direction;
                //Console.WriteLine("{0}, {1}, {2}", direction[0], direction[1], direction[2]);
                if (IsActive(coord))
                    num++;
            }
            return num;
        }

        bool WillBeActive(Tools.Coordination cube) {
            bool isActive = IsActive(cube);
            int numActiveNeighbours = NumActiveNeighbours(cube);
            if (isActive) {
                if (numActiveNeighbours == 2 || numActiveNeighbours == 3)
                    return true;
                else
                    return false;
            }
            else {
                if (numActiveNeighbours == 3)
                    return true;
                else
                    return false;
            }
        }

        void Expand() {
            var newActive = new List<Tools.Coordination>();
            
            foreach (var cube in _active) {
                if (WillBeActive(cube))
                    if (!IsInList(cube, newActive)) {
                       // Console.WriteLine("{0}, {1}, {2}", cube[0], cube[1], cube[2]);
                        newActive.Add((Tools.Coordination)cube.Clone());
                    }
                
                foreach (var direction in _directions) {
                    var neighbourCube = cube + direction;
                    if (IsActive(neighbourCube))
                        continue;
                    if (WillBeActive(neighbourCube))
                        if (!IsInList(neighbourCube, newActive)) {
                            //Console.WriteLine("{0}, {1}, {2}", neighbourCube[0], neighbourCube[1], neighbourCube[2]);
                            newActive.Add((Tools.Coordination)neighbourCube.Clone());
                        }
                }
            }
            
            _active = newActive;
        }

        public void PartOne() {
            ReadInput(3);
            for (int i = 0; i < 6; i++) {
                /*
                foreach (var coord in _active) {
                    Console.WriteLine("{0}, {1}, {2}", coord[0], coord[1], coord[2]);
                    
                }
                Console.WriteLine();
                */
                Expand();
            }

            Console.WriteLine(_active.Count);
        }

        public void PartTwo() {
            ReadInput(4);
            for (int i = 0; i < 6; i++) {
                /*
                foreach (var coord in _active) {
                    Console.WriteLine("{0}, {1}, {2}", coord[0], coord[1], coord[2]);
                    
                }
                Console.WriteLine();
                */
                Expand();
            }

            Console.WriteLine(_active.Count);
        }
    }
}
