using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Badmin.Badmin
{
    public class Badmin<T> where T : class
    {

        public Badmin()
        {
            DataConfigurations = new List<DataConfiguration>();
        }


        public ICollection<DataConfiguration> DataConfigurations { get; set; }



        public BadminConfiguration Register<TResult>(Func<T, IQueryable<TResult>> data) where TResult : class
        {

            var dataContext = this.CreateDataContextType();

            var invokedData = data.Invoke(dataContext);

            var dataConfiguration = new DataConfiguration
            {
                Data = invokedData
            };

            DataConfigurations.Add(dataConfiguration);

            return new BadminConfiguration(dataConfiguration);

        }

        public T CreateDataContextType()
        {
            var dataContext = typeof(T).GetConstructor(System.Type.EmptyTypes).Invoke(null);

            return dataContext as T;

        }




    }
}