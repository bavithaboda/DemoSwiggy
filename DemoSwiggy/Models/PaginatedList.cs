using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSwiggy.Models
{
    public class PaginatedList<T>:List<T>
    {
        public int TotalRecords { get; private set; }
        public PaginatedList(List<T> source, int pageIndex, int pageSize)
        {
            TotalRecords = source.Count;
            //Eg. pageIndex=1 then it take 0 it means we are moving to beginning of the list
            //So it is skipping the first 0 records
            //If pageIndex is 3 then it will skip first 10 records
            var items = source.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            this.AddRange(items);
        }
    }
}
