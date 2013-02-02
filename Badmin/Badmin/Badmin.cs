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


        public static DbContext CreateDataContext<T>(DataConfiguration<T> dataConfig)
        {
            return dataConfig.DataContextType.GetConstructor(System.Type.EmptyTypes).Invoke(null) as DbContext;
        }

        public DbContext CreateDataContext(string type)
        {
            var dataConfig = this.Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
            return Badmin.CreateDataContext(dataConfig);
        }

        public DataConfiguration<object> GetDataConfiguration<TType>(string type) where TType: class
        {
            return this.Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
        }

        public DataConfiguration<object> GetDataConfiguration(string type)
        {
            return this.Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
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