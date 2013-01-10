using System.Web.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Badmin.Tests
{

    public class Dummy
    {
        public string Name { get; set; }
        public string Description { get; set; }


    }

    public class DatabaseDummyContext : DbContext
    {

        public IDbSet<Dummy> Dummies { get; set; }

    }


    public class Tests
    {
    	
        [Fact]
        public void BadminCanRegisterADataSource()
        {
            var badmin = new Badmin<DatabaseDummyContext>();
            Assert.IsType<DatabaseDummyContext>(badmin.CreateDataContextType());

        }

        [Fact]
        public void BadminCanRegisterTypesToBeModified()
        {
        	
            var badmin = new Badmin<DatabaseDummyContext>();

            badmin.Register<Dummy>(x => x.Dummies);

            Assert.Equal(1, badmin.DataConfigurations.Count);
            

            var first = badmin.DataConfigurations.First();
            Assert.IsType(typeof(DbSet<Dummy>), first.Data);
        }

        [Fact]
        public void BadminCanRegisterMultipleTypesOfTheSameType()
        {
            var badmin = new Badmin<DatabaseDummyContext>();

            badmin.Register<Dummy>(x => x.Dummies);
            badmin.Register<Dummy>(x => x.Dummies);

            Assert.Equal(2, badmin.DataConfigurations.Count);
        }

        [Fact]
        public void BadminCanSetNameOfRegisteredDataCollection()
        {
            var badmin = new Badmin<DatabaseDummyContext>();

            badmin.Register<Dummy>(x => x.Dummies)
                .Name("TooCool");

            Assert.Equal("TooCool", badmin.DataConfigurations.First().Name);

        }

    }

    public static class BadminExtensions
    {
    	public static BadminConfiguration Name(this BadminConfiguration config, string name)
    	{
            config.DataConfiguration.Name = name;
            return config;
    	}


    }

    public class BadminConfiguration
    {
        
        public BadminConfiguration(DataConfiguration dataConfiguration)
        {
            this.DataConfiguration = dataConfiguration;
        }


        public DataConfiguration DataConfiguration { get; set; }
        
	
    }
    
    public class Badmin<T> where T: class 
    {

        public Badmin()
        {
            DataConfigurations = new List<DataConfiguration>();
        }


        public ICollection<DataConfiguration> DataConfigurations { get; set; }

        

        public BadminConfiguration Register<TResult>(Func<T, IQueryable<TResult>> data) where TResult: class
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
    public class DataConfiguration
    {
        public string Name { get; set; }
        public IQueryable Data { get; set; }
        public Type Type { get; set; }
    }

    
    //public class Badmin<T>
    //{
    //    private readonly DatabaseDummyContext dataContext;

    //    public Badmin(DatabaseDummyContext dataContext)
    //    {
    //        this.dataContext = dataContext;
    //    }

    //    public BadminConfiguration Register<TResult>(Func<T, IQueryable<TResult>> data) where TResult : class
    //    {
            
    //        var invokedDelegate = data.Invoke(this.dataContext);

    //        return new BadminConfiguration(invokedDelegate));
    //    }

    //    public IEnumerable<BadminConfiguration> Configurations { get; private set; }
       
    //}

    public class BadminDataSource<T> //T is the underlying data type.
    {
    	
        public readonly string Name;

        public BadminDataSource(IQueryable dataSource, string name)
        {
            this.Name = name;
        }


    }



}
