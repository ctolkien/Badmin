﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Badmin
{

    public class Badmin : IBadmin
    {

        public static DbContext CreateDataContext<T>(DataConfiguration<T> dataConfig)
        {
            if (dataConfig == null) throw new ArgumentNullException("dataConfig");
            return dataConfig.DataContextType.GetConstructor(Type.EmptyTypes).Invoke(null) as DbContext;
        }

        public DbContext CreateDataContext(string type)
        {
            //hack: kill me now...
            var dataConfig = Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
            return CreateDataContext(dataConfig);
        }

        public DataConfiguration<object> GetDataConfiguration<TType>(string type) where TType: class
        {
            return Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
        }

        public DataConfiguration<object> GetDataConfiguration(string type)
        {
            return Configurations.SingleOrDefault(x => x.Name.ToUpper() == type.ToUpper());
        }

        //TODO think this should be be made non-generic, or changed to 'type'
        public ICollection<DataConfiguration<dynamic>> Configurations { get; private set; }

        public DataConfiguration<dynamic> Register<T, TResult>(Func<T, IQueryable<TResult>> data)
            where TResult : class
            where T : DbContext
        {
            //what the hell is all this shit... needs to be changed....
            var dataContext = CreateDataContextForType<T>();
            
            var dataConfiguration = new DataConfiguration<dynamic>
            {
                Data = data.Invoke(dataContext),
                Name = typeof(TResult).Name,
                DataContextType = typeof(T)
                
            };

            if (Configurations == null)
            {
                Configurations = new Collection<DataConfiguration<dynamic>>();
            }

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