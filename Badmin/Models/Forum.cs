using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badmin.Models
{
    public class Forum
    {

        public int Id { get; set; }
        public string Name { get; set; }


    }

    public class Thread : Post
    {
        public string Title { get; set; }

    }

    public class Post
    {

        public string PostBody { get; set; }
        public virtual  User PostedBy { get; set; }



    }

    public class User
    {
        
    }
}