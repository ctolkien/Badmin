using System;
using System.Linq;

namespace Badmin.Badmin
{
    public class DataConfiguration
    {
        public string Name { get; set; }

        public IQueryable Data { get; set; }

        public Type Type { get; set; }
    }
}