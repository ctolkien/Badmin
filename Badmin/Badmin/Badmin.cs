using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Badmin
{
    public class Badmin : IBadmin
    {

        public Badmin()
        {
            DataConfigurations = new List<DataConfiguration>();
        }

        public ICollection<DataConfiguration> DataConfigurations { get; private set; }

        public DataConfiguration Register<T, TResult>(Func<T, IQueryable<TResult>> data) where TResult : class where T : class
        {

            var dataContext = this.CreateDataContextType<T>();

            var invokedData = data.Invoke(dataContext);


            var dataConfiguration = new DataConfiguration
            {
                Data = invokedData,
                Name = GetTypeName(invokedData.ElementType.FullName),
                
            };


            DataConfigurations.Add(dataConfiguration);

            return dataConfiguration;

        }


        public T CreateDataContextType<T>() where T: class
        {
            var dataContext = typeof(T).GetConstructor(System.Type.EmptyTypes).Invoke(null);

            return dataContext as T;

        }

        private string GetTypeName(string fullName)
        {
            if (!fullName.Contains('.'))
                return fullName;

            return fullName.Substring(fullName.LastIndexOf('.') + 1);
        }

    }
}