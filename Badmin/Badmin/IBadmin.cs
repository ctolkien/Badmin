using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Badmin
{
    public interface IBadmin
     
    {
        T CreateDataContextForType<T>() where T: class;

        ICollection<DataConfiguration> Configurations { get; }

        DataConfiguration Register<T, TResult>(Func<T, IQueryable<TResult>> data)
            where TResult : class
            where T : DbContext;

        DataConfiguration GetDataConfiguration<TType>(string type) where TType: class;
        DataConfiguration GetDataConfiguration(string type);

        DbContext CreateDataContext(string type);

    }
}
