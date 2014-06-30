using System;

namespace Badmin
{
    public class DataConfiguration 
    {
        public DataConfiguration()
        {
            VisibleInMenu = true; //todo: remove this?
        }

        public string Name { get; set; }

        public bool VisibleInMenu { get; set; }

        public Type ElementType { get; set; }
        public Type DbContextType { get; set; }

        
    }

    
}