using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AOC2020 {
    class Day10 {
        List<int> _adapters = new List<int>(Tools.Common.ReadInts("input10.txt"));
        
        public void PartOne() {
            _adapters.Sort();

            int nDif1 = 1;
            int nDif3 = 1;
            for (int i = 1; i < _adapters.Count; i++) {
                if ((_adapters[i] - _adapters[i - 1]) == 1)
                    nDif1++;
                else if ((_adapters[i] - _adapters[i - 1]) == 3)
                    nDif3++;
            }

            Console.WriteLine(nDif1);
            Console.WriteLine(nDif3);
            Console.WriteLine(nDif1 * nDif3);
        }

        bool IsValid(List<int> adapters, int before) {
            if (adapters.Count == 0)
                return false;

            if ((adapters[0] - before) > 3)
                return false;
            for (int i = 1; i < adapters.Count; i++) {
                if ((adapters[i] - adapters[i - 1]) > 3)
                    return false;
            }
            return true;
        }
                
        List<int> CreateList(List<int> src, int exclude) {
            var ret = new List<int>();
            for (int i = 0; i < src.Count; i++) {
                if (i != exclude)
                    ret.Add(src[i]);
            }
            return ret;
        }

        long CountValid2(List<int> adapters, int before) {
            long ret = 0;
            if (IsValid(adapters, before)) {
                
               // Console.WriteLine("adapteers valid");
                //Tools.Common.WriteInts(adapters);
                
                for (int i1 = 0; i1 < adapters.Count - 1; i1++) {
                    var adapters2 = CreateList(adapters, i1);
                    if (!IsValid(adapters2, before)) {
                        long retLeft = 1;
                        if (i1 > 0) {
                            var adaptersLeft = adapters.GetRange(0, i1 + 1);
                            retLeft = CountValid2(adaptersLeft, before);
                        }

                        long retRight = 1;
                        if (i1 < adapters.Count - 1) {
                            var adaptersRight = adapters.GetRange(i1 + 1, adapters.Count - i1 - 1);
                            retRight = CountValid2(adaptersRight, adapters[i1]);
                            
                        }

                        return retLeft * retRight;
                    }
                }

                return CountEffectively(adapters, before);
                
            }
            return ret;
        }

        List<int> CreateList(List<int> src, int[] exclude) {
            var ret = new List<int>();
            for (int i = 0; i < src.Count; i++) {
                if (!exclude.Contains(i))
                    ret.Add(src[i]);
            }
            return ret;
        }

        long CountEffectively(List<int> adapters, int before) {
            if (adapters.Count == 0)
                return 0;
            long ret = 1;
            
            for (int i = 1; i < adapters.Count; i++) {
                Tools.Indeces indeces = new Tools.Indeces(adapters.Count - 1, i);
                while (indeces.IsOk()) {
                    if (i == 3) {
                        //Tools.Common.WriteInts(indeces.Get());
                    }
                    var adapters2 = CreateList(adapters, indeces.Get());
                   
                    if (IsValid(adapters2, before)) {
                        if (i == 4) {

                        }
                        //Tools.Common.WriteInts(indeces.Get());
                        //Tools.Common.WriteInts(adapters2);
                        ret++;
                    }
                        
                    indeces.Next();
                }
            }

            return ret;
        }

        
      
        public void PartTwo() {      
            
            _adapters.Sort();

            Console.WriteLine(CountValid2(_adapters, 0));

            //Console.WriteLine(CountEffectively(_adapters, 0));
        }
    }
}
