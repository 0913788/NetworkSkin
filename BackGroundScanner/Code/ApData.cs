using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackGroundScanner.Code
{
    class ApData
    {
        public string MAC { get; set; }
        public float X {get; set;}
        public float Y {get; set;}
        public float dist {get; set;}

        public ApData(string mac, float x, float y)
        {
            MAC = mac;
            X = x;
            Y = y;
        }
    }


}
