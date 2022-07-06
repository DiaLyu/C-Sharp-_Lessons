using System;
using System.Collections.Generic;
using System.Text;
using labwork10;
using System.Linq;

namespace lab_work_14
{
    internal class ExtensionQuery : IQuery
    {
        public SortedDictionary<State, Republic> col4 { get; set; }

        public IEnumerable<IGrouping<int, Republic>> GroupTerm()
        {
            return col4.Values
                .GroupBy(item => item.TermOfOfficePresident);
        }

        public int CountTerr()
        {
            return col4
                .Where(item => item.Value.Territory > 500000)
                .Select(item => item.Value)
                .Count();
        }

        public double AverageTerr()
        {
            return col4
                .Select(item => item.Value.Territory)
                .Average();
        }

        public int MedianNumber()
        {
            var powers = col4
                .Select(item => item.Value.NumberOfCitizens)
                .ToArray();
            Array.Sort(powers);
            int median = powers.Count() / 2;
            return powers[median];
        }

        public IEnumerable<string> RepublicKingdom()
        {
            var internals = col4
                .Where(item => item.Value.Territory > 500000)
                .Select(item => item.Value.NameState);
            var reactives = col4
                .Where(item => item.Value.NumberOfCitizens > 500000)
                .Select(item => item.Value.NameState);
            return internals.Intersect(reactives);
        }
    }
}
