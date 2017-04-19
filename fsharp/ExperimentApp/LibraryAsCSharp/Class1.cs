using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAsCSharp
{
    public class Class1
    {
        public int GetCountMethod() { return CountProperty; }

        public int CountProperty { get; set; }
    }
}


/*
 * 
 * 
 * let c1 = Class1()
    c1.CountProperty <- 10
    let p1 = c1.CountProperty
    let count = c1.GetCountMethod()    
 */
