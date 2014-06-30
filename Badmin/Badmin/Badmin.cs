using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Badmin
{

    public class Badmin : IBadmin
    {

        public static DbContext CreateDataContext(DataConfiguration dataConfig)
        {
            if (dataConfig == null) throw new ArgumentNullException("dataConfig");
            return dataConfig.ElementType.GetConstructor(Type.EmptyTypes).Invoke(null) as DbContext;
        }

        public DbContext CreateDataContext(string type)
        {
            //hack: kill me now...
            var dataConfig = Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
            return CreateDataContext(dataConfig);
        }

        public DataConfiguration GetDataConfiguration<TType>(string type) where TType: class
        {
            return Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
        }

        public DataConfiguration GetDataConfiguration(string type)
        {
            return Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
        }

        //TODO think this should be be made non-generic, or changed to 'type'
        public ICollection<DataConfiguration> Configurations { get; private set; }



        public DataConfiguration Register<T, TResult>(Func<T, IQueryable<TResult>> query)
            where TResult : class
            where T : DbContext
        {

            if (Configurations == null)
            {
                Configurations = new Collection<DataConfiguration>();
            }

            var dataConfiguration = new DataConfiguration
            {
                Name = typeof(TResult).Name,
                ElementType = typeof(T)
            };

            Configurations.Add(dataConfiguration);

            return dataConfiguration;

        }


        public T CreateDataContextForType<T>() where T: class
        {
            var dataContext = typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null);

            return dataContext as T;

        }





    }
}