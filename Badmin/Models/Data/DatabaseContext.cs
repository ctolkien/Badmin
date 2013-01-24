using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Badmin.Models.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Forum> Forums { get; set; }
    }
}