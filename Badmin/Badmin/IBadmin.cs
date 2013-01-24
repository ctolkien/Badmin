using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Badmin
{
    public interface IBadmin
     
    {
        T CreateDataContextType<T>() where T: class;
        ICollection<DataConfiguration<object>> Configurations { get; }

        DataConfiguration<object> Register<T, TResult>(Func<T, IQueryable<TResult>> data) where TResult : class where T: DbContext;

        DataConfiguration<object> GetDataConfigurationForType<TType>(string type) where TType: class;

    }
}
