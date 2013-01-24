using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Badmin
{

    public class Badmin : IBadmin
    {

        public Badmin()
        {
            Configurations = new List<DataConfiguration<object>>();

        }


        public static DbContext CreateDataCotext(DataConfiguration<object> dataConfig)
        {
            return dataConfig.DataContextType.GetConstructor(System.Type.EmptyTypes).Invoke(null) as DbContext;
        }

        public ICollection<DataConfiguration<object>> Configurations { get; private set; }

        public DataConfiguration<object> Register<T, TResult>(Func<T, IQueryable<TResult>> data)
            where TResult : class
            where T : DbContext
        {

            var dataContext = this.CreateDataContextType<T>();
            
            var invokedData = data.Invoke(dataContext);

            var dataConfiguration = new DataConfiguration<object>
            {
                Data = invokedData,
                Name = typeof(TResult).Name,
                DataType = typeof(TResult),
                DataContextType = typeof(T)
                
            };


            Configurations.Add(dataConfiguration);

            return dataConfiguration;

        }


        public T CreateDataContextType<T>() where T: class
        {
            var dataContext = typeof(T).GetConstructor(System.Type.EmptyTypes).Invoke(null);

            return dataContext as T;

        }


    }
}