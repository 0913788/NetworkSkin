using BackGroundScanner.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using System.Threading;

namespace BackGroundScanner
{
    public sealed class BackGroundScanning : IBackgroundTask
    {
        ProbeAdapter adapter = new ProbeAdapter();
        StaticData data = new StaticData();
        DbCall dbCon = new DbCall();

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //int x = 0;
            //while (x<15)
            //{
            //    if (stopwatch.ElapsedMilliseconds > 1000)
            //    {
                    dbCon.PostLocation(1, (float)0.04+1*0.1f, (float)-1.42+1*0.1f);
            //        x++;
            //        stopwatch.Reset();
            //    }
            //}
        }
    }
}
    