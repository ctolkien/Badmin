
using System;
using System.Linq;

namespace Badmin
{
    public class DataConfiguration<T> 
    {
        public DataConfiguration()
        {
            VisibleInMenu = true;
        }

        public string Name { get; set; }

        public bool VisibleInMenu { get; set; }

        public IQueryable<T> Data { get; set; }

        public Type DataContextType { get; set; }

        
    }

    public class DataConfiguration
    {
        public DataConfiguration()
        {
            VisibleInMenu = true;
        }

        public string Name { get; set; }

        public bool VisibleInMenu { get; set; }

        public IQueryable Data { get; set; }

        public Type DataContextType { get; set; }


    }
}