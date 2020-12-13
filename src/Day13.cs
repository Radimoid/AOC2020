using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020 {
    class Day13 {
        int _estimate;
        int[] _ids;
                
        int WaitingTime(int id) {
            if (_estimate % id == 0)
                return 0;

            var tmp = _estimate / id;
            return tmp * id + id - _estimate; 
        }

        public void PartOne() {
            var lines = Tools.Common.ReadLines("input13.txt");
            _estimate = int.Parse(lines[0]);
            var idsStrs = lines[1].Split(',');
            var idsTmp = new List<int>();
            foreach (var idStr in idsStrs) {
                if (idStr[0] != 'x') {
                    idsTmp.Add(int.Parse(idStr));
                }
            }
            _ids = idsTmp.ToArray();

            int bestId = _ids[0];
            int bestTime = WaitingTime(bestId);
            for (int i = 0; i < _ids.Length; i++) {
                var t = WaitingTime(_ids[i]);
                if (t < bestTime) {
                    bestTime = t;
                    bestId = _ids[i];
                }
            }

            Console.WriteLine(bestTime * bestId);
        }

        bool Check(long t) {
            for (int i = 1; i < _ids.Length; i++) {
                if (_ids[i] == 0)
                    continue;
                if ((t + i) % _ids[i] != 0)
                    return false;
            }

            return true;
        }
        public void PartTwo() {
            var lines = Tools.Common.ReadLines("input13.txt");            
            var idsStrs = lines[1].Split(',');
            var idsTmp = new List<int>();
            foreach (var idStr in idsStrs) {
                if (idStr[0] == 'x')
                    idsTmp.Add(0);
                else
                    idsTmp.Add(int.Parse(idStr));
            }
            _ids = idsTmp.ToArray();

            int i = _ids[0];
            int id2 = _ids[i];
            long step = Tools.Common.LCM(i, id2);

            long t = step;
            while (!Check(t - i))
                t += step;

            Console.WriteLine(t - i);

        }
    }
}
