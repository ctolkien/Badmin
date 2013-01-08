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

    public class Monkey
    {

        [Fact]
        public void BadminCanRegisterIndividualIDbSets()
        {

            var badmin = new Badmin<DatabaseDummyContext>();

            badmin.Register<Dummy>(x => x.Dummies);


        }
    }

    public class Badmin<T> where T : class
    {
        
        //ICollection<IDbSet<TResult>> things;

        public void Register<TResult>(Func<T, IDbSet<TResult>> wootah) where TResult: class
        {
            
        }
    	
    }



}
