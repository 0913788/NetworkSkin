using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSkin
{
    class StaticData{
        public static List<ApData> APData = new List<ApData>();
        
        public StaticData(){
            APData.Add(new ApData("f8:4f:57:3b:1a:00", -1.66f, 7.75f));
            APData.Add(new ApData("f8:4f:57:3b:31:e0", -11f, -0.13f));
            APData.Add(new ApData("f8:4f:57:78:b8:d0", -15.85f, -8.26f));
            APData.Add(new ApData("f8:4f:57:3b:35:30", -2.65f, -8.26f));
            APData.Add(new ApData("f8:4f:57:12:f1:70", -11.18f, -0.1f));
            APData.Add(new ApData("f8:4f:57:12:6f:50", -20.6f, -2.73f));
        }
    }
}
