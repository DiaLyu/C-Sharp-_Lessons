using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using labwork10;

namespace lab_work_14
{
    internal class LinqQuery : IQuery
    {
        public SortedDictionary<State, Republic> col4 { get; set; }

        public IEnumerable<IGrouping<int, Republic>> GroupTerm()
        {
            return from item in col4.Values
                   group item by item.TermOfOfficePresident;
        }

        public int CountTerr()
        {
            return (from item in col4
                    where item.Value.Territory > 500000
                    select item.Value)
                    .Count();
        }

        public double AverageTerr()
        {
            return (from item in col4
                    select item.Value.Territory)
                    .Average();
        }

        public int MedianNumber()
        {
            var powers = (from item in col4
                          select item.Value.NumberOfCitizens).ToArray();
            Array.Sort(powers);
            int median = powers.Count() / 2;
            return powers[median];
        }

        public IEnumerable<string> RepublicKingdom()
        {
            var internals = from item in col4
                            where item.Value.Territory > 500000
                            select item.Value.NameState;
            var reactives = from item in col4
                            where item.Value.NumberOfCitizens > 500000
                            select item.Value.NameState;
            return internals.Intersect(reactives);
        }
    }
}
