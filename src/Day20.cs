using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AOC2020 {
    class Day20 {
        List<Tile> _tiles;
        int _rows;
        int _cols;

        enum Direction {
            Top,
            Right,
            Bottom,
            Left
        };

        static Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));


        class Tile {
            public long _id = 0;
            Tools.Map _map = null;
            Tile[] _neighbours = new Tile[] { null, null, null, null };
            static Tile border = new Tile();

            Tile() {
            }

            public Tile(string[] lines) {
                var match = Regex.Match(lines[0], @"Tile (\d+):$");
                if (!match.Success)
                    throw new Exception();

                _id = long.Parse(match.Groups[1].Value);
                _map = new Tools.Map(lines.ToList().GetRange(1, lines.Length - 1).ToArray());
            }        
            
            public Tools.Map Map() {
                return _map;
            }

            public long Id() {
                return _id;
            }

            public void ResetNeighbours() {
                for (int i = 0; i < 4; i++)
                    _neighbours[i] = null;
            }

            public void SetNeighbour(Tile tile, Direction direction) {
                _neighbours[(int)direction] = tile;
            }

            public string Border(Direction i) {
                if (_neighbours[(int)i] == null) {
                    switch (i) {
                        case Direction.Top:
                            return _map.FirstRow();
                        case Direction.Right:
                            return _map.LastCol();
                        case Direction.Bottom:
                            return _map.LastRow();
                        case Direction.Left:
                            return _map.FirstCol();
                    }
                }
                
                return string.Empty;
            }

            public string Border((int, int) direction) {
                switch (direction) {
                    case (1, 0):
                        return _map.LastCol();
                    case (-1, 0):
                        return _map.FirstCol();
                    case (0, 1):
                        return _map.LastRow();
                    case (0, -1):
                        return _map.FirstRow();
                }

                return null;
            }

            public List<Tools.Map> GetArrangements() {
                var ret = new List<Tools.Map>();

                var newmap = _map.Identity();
                for (int i = 0; i < 4; i++) {
                    ret.Add(newmap);
                    ret.Add(newmap.FlipHor());
                    ret.Add(newmap.FlipVer());
                    ret.Add(newmap.FlipHor().FlipVer());
                    newmap = newmap.Rotate();
                }

                return ret;
            }
        }

        class TileMap {
            Dictionary<(int, int), Tile> _arrangement;
            List<Tile> _unarranged;
            int _len;
            int _minx, _maxx, _miny, _maxy;

            static (int, int)[] directions = new (int, int)[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

            (int, int) AddCoords((int, int) coord1, (int, int) coord2) {
                return (coord1.Item1 + coord2.Item2, coord1.Item1 + coord2.Item2);
            }
                   
            bool CheckInterval(int currentMin, int currentMax, int maybeNew) {
                if (maybeNew < currentMin) {
                    return (currentMax - maybeNew) < (_len - 1);
                }
                else if (maybeNew > currentMax) {
                    return (maybeNew - currentMin) < (_len - 1);
                }

                return true;
            }

            bool CheckShape((int, int) coord) {
                return CheckInterval(_minx, _maxx, coord.Item1) && CheckInterval(_miny, _maxy, coord.Item2);
            }
            
            bool IsOk(Tools.Map tileArrangement, (int, int) coord) {
                var leftTile = _arrangement[(coord.Item1 - 1, coord.Item2)];
                if (leftTile != null)

            }
            public void Create(List<Tile> tiles) {
                _unarranged = new List<Tile>();
                foreach (var tile in tiles)
                    _unarranged.Add(tile);

                _len = (int)Math.Sqrt(tiles.Count);

                (int, int) coord = (0, 0);
                _arrangement = new Dictionary<(int, int), Tile>();
                _arrangement[coord] = _unarranged[0];
                _unarranged.Remove(_unarranged[0]);



                Tile tile1 = _arrangement[coord];
                foreach (var direction in directions) {
                    var coord2 = AddCoords(coord, direction);
                    if (_arrangement.Keys.Contains(coord2))
                        continue;
                    if (!CheckShape(coord2))
                        continue;
                    string border1 = tile1.Border(direction);
                    foreach (var tile2 in _unarranged) {
                        var tileArrangements = tile2.GetArrangements();
                        foreach (var tileArrangement in tileArrangements) {

                        }                        
                    }
                }
            }
        }

        void ReadInput() {
            _tiles = new List<Tile>();
            var alltext = Tools.Common.ReadAllText("input20.txt");
            var tiletexts = alltext.Split("\r\n\r\n");
            foreach (var tiletext in tiletexts)
                _tiles.Add(new Tile(tiletext.Split("\r\n")));
            _rows = _cols = (int)Math.Sqrt(_tiles.Count);
        }

        int GetIndexFromPos(int col, int row) {
            return row * _cols + col;
        }

        List<Tile> CheckArrangement(int[] arrangement) {
            List<Tile> rearrangedTiles = new List<Tile>();
            for (int i = 0; i < _tiles.Count(); i++)
                rearrangedTiles.Add(_tiles[arrangement[i]]);

            return null;
            /*
            for (int row = 0; row < _rows; row++) {
                for (int col = 0; col < _cols; col++) {
                    int index = GetIndexFromPos(col, row);
                    int indexInTiles1 = arrangement[indexInArrangement1];
                    Tile tile1 = _tiles[indexInTiles1];
                    long id1 = tile1.Id();

                    string col1 = tile1.Map().FirstCol();
                    string row1 = tile1.Map().FirstRow();
                    
                    if (row > 0) {
                        int indexInArrangement = GetIndexFromPos(col, row - 1);
                        int indexInTiles = arrangement[indexInArrangement];
                        Tile tile2 = _tiles[indexInTiles];
                        long id2 = tile2.Id();
                        string row2 = tile2.Map().LastRow();
                        if (row1 != row2)
                            return null;
                    }

                    if (col > 0) {
                        int indexInArrangement = GetIndexFromPos(col - 1, row);
                        int indexInTiles = arrangement[indexInArrangement];
                        Tile tile2 = _tiles[indexInTiles];
                        long id2 = tile2.Id();
                        string col2 = tile2.Map().LastCol();
                        if (col1 != col2)
                            return null;
                    }

                    
                }
            }
            return true;
            */
        }
        
        public void PartOne() {
            ReadInput();

            
            for (int i = 0; i < _tiles.Count; i++) {
                var arrangements1 = _tiles[i].GetArrangements();
                for (int j = 0; j < _tiles.Count; j++) {
                    var arrangements2 = _tiles[j].GetArrangements();

                }
            }

            var row1 = _tiles[0].Map().FirstRow();
            List<string> suitable = new List<string>();
            foreach (var tile in _tiles) {
                var s1 = tile.Map().FirstRow();
                var s2 = tile.Map().LastRow();
                var s3 = tile.Map().FirstCol();
                var s4 = tile.Map().LastCol();

                if (s1 == row1)
                    suitable.Add(s1);

                if (s2 == row1)
                    suitable.Add(s2);

                if (s3 == row1)
                    suitable.Add(s3);

                if (s4 == row1)
                    suitable.Add(s4);

            }

            /*
            var testArrangement = new int[] { 1, 0, 8, 7, 3, 5, 6, 4, 2 };
            var testResult = CheckArrangement(testArrangement);
            
            var indeces = new Tools.Indeces(_tiles.Count, _tiles.Count);
            int[] rightArrangement = null;
            while (indeces.IsOk()) {
                var arrangement = indeces.Get();
                if (CheckArrangement(arrangement)) {
                    rightArrangement = arrangement;
                    break;
                }                    
                indeces.Next();
            }

            foreach (var index in rightArrangement)
                Console.WriteLine(_tiles[index].Id());

            long result = _tiles[GetIndexFromPos(0, 0)].Id() *
                _tiles[GetIndexFromPos(_cols - 1, 0)].Id() *
                _tiles[GetIndexFromPos(0, _rows - 1)].Id() *
                _tiles[GetIndexFromPos(_cols - 1, _rows - 1)].Id();

            Console.WriteLine(result);
            */
        }

        public void PartTwo() {

        }
    }
}
