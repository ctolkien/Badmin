using System;
using System.Collections.Generic;

namespace Badmin
{
    public interface IBadmin
     
    {
        T CreateDataContextType<T>() where T: class;
        ICollection<DataConfiguration<object>> Configurations { get; } 

        DataConfiguration<object> Register<T, TResult>(Func<T, System.Linq.IQueryable<TResult>> data)
            where TResult : class
            where T : class;
    }
}
