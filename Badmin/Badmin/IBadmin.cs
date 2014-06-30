using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Badmin
{
    public interface IBadmin
     
    {
        T CreateDataContextForType<T>() where T: class;

        ICollection<DataConfiguration<object>> Configurations { get; }

        DataConfiguration<TResult> Register<T, TResult>(Func<T, IQueryable<TResult>> data)
            where TResult : class
            where T : DbContext;

        DataConfiguration<object> GetDataConfiguration<TType>(string type) where TType: class;
        DataConfiguration<object> GetDataConfiguration(string type);

        DbContext CreateDataContext(string type);

    }
}
