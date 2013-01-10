using System;
namespace Badmin
{
    public interface IBadmin
     
    {
        T CreateDataContextType<T>() where T: class;
        System.Collections.Generic.ICollection<DataConfiguration> DataConfigurations { get; }
        DataConfiguration Register<T, TResult>(Func<T, System.Linq.IQueryable<TResult>> data) where TResult : class;
    }
}
