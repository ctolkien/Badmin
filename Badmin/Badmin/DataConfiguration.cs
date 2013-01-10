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

        public IQueryable<T> GetDataAsIQueryable<T>()
        {
            return Data as IQueryable<T>;
        }

        public Type Type { get; set; }
    }
}