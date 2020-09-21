using System;
using System.Collections.Generic;
using System.Text;

namespace SharedServices.Models
{
    
    
        public class Rootobject
        {
            public Current current { get; set; }
        }

        public class Current
        {
            public double temp { get; set; }
            public int humidity { get; set; }
        }
    
}
