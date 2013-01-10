
using System;
using System.Linq;

namespace Badmin
{
    public class DataConfiguration
    {
        public DataConfiguration()
        {
            VisibleInMenu = true;
        }

        public string Name { get; set; }

        public bool VisibleInMenu { get; set; }

        public IQueryable Data { get; set; }

        
    }
}