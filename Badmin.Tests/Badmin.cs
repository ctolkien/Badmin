using System.Web.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Badmin;
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
    	
        public static Badmin CreateBadmin()
        {
            return new Badmin();
        }

        [Fact]
        public void BadminCanRegisterADataSource()
        {
            var badmin = CreateBadmin();
            Assert.IsType<DatabaseDummyContext>(badmin.CreateDataContextType<DatabaseDummyContext>());

        }

        [Fact]
        public void BadminCanRegisterTypesToBeModified()
        {

            var badmin = CreateBadmin();

            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies);

            Assert.Equal(1, badmin.DataConfigurations.Count);
            

            var first = badmin.DataConfigurations.First();
            Assert.IsType(typeof(DbSet<Dummy>), first.Data);
        }

        [Fact]
        public void BadminCanRegisterMultipleTypesOfTheSameType()
        {
            var badmin = CreateBadmin();

            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies);
            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies);

            Assert.Equal(2, badmin.DataConfigurations.Count);
        }


        [Fact]
        public void BadminCanSetNameOfRegisteredDataCollection()
        {
            var badmin = CreateBadmin();

            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies)
                .Name("SampleName");

            Assert.Equal("SampleName", badmin.DataConfigurations.First().Name);

        }
        [Fact]
        public void BadminCanRegisterMultipleTypesOfTheSameTypeButWithDifferentNames()
        {
            var badmin = CreateBadmin();

            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies).Name("Number 1");
            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies).Name("Number 2");

            Assert.Equal("Number 1", badmin.DataConfigurations.First().Name);
            Assert.Equal("Number 2", badmin.DataConfigurations.Last().Name);
        }

        [Fact]
        public void BadminCanSetAPropertyToDisableBeingDisplayedInTheMenu()
        {
            var badmin = CreateBadmin();

            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies).Name("Sample").VisibleInMenu(false);

            Assert.False(badmin.DataConfigurations.First().VisibleInMenu);

        }

        [Fact]
        public void BadminDataCollectionShouldDefaultToBeingVisible()
        {
            var badmin = CreateBadmin();

            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies).Name("Sample");

            Assert.True(badmin.DataConfigurations.First().VisibleInMenu);

        }

        [Fact]
        public void BadminDataCollectionShouldUseNameOfTypeIfNoNameIsSpecified()
        {
            var badmin = CreateBadmin();

            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies);

            Assert.Equal("Dummy", badmin.DataConfigurations.First().Name);
        }

        [Fact]
        public void BadminCanReturnDataOutOfDataCollections()
        {

            var badmin = CreateBadmin();

            badmin.Register<DatabaseDummyContext, Dummy>(x => x.Dummies);

            var dataConfiguration = badmin.DataConfigurations.Single(x => x.Name == "Dummy");


            Assert.Equal(1, dataConfiguration.GetDataAsIQueryable<Dummy>().Count());





        }


    }




}
