using System.Web.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Badmin.Badmin;
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

    public class BadminTests
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
        public void BadminCanRegisterMultipleTypesOfTheSameTypeButWithDifferentNames()
        {
            var badmin = new Badmin<DatabaseDummyContext>();

            badmin.Register<Dummy>(x => x.Dummies).Name("Number 1");
            badmin.Register<Dummy>(x => x.Dummies).Name("Number 2");

            Assert.Equal("Number 1", badmin.DataConfigurations.First().Name);
            Assert.Equal("Number 2", badmin.DataConfigurations.Last().Name);
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




}
