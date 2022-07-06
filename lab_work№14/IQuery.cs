using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using labwork10;

namespace lab_work_14
{
    internal interface IQuery
    {
        SortedDictionary<State, Republic> col4 { get; }

        IEnumerable<IGrouping<int, Republic>> GroupTerm();

        int CountTerr();

        double AverageTerr();

        int MedianNumber();

        IEnumerable<string> RepublicKingdom();
    }
}
