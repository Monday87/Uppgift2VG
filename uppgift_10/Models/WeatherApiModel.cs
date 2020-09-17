using System;
using System.Collections.Generic;
using System.Text;

namespace uppgift_10.Models
{
    class WeatherApiModel
    {

    }

    public class Rootobject
    {
       
        public Current current { get; set; }

        public override string ToString()
        {
            return $"{current}";
        }
    }

    public class Current
    {
    
        public float temp { get; set; }
        public override string ToString()
        {
            return $"{temp}";
        }

    }

    

}
