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
        Stopwatch stopwatch = new Stopwatch();
        DbCall DbCon = new DbCall();


        public void Run(IBackgroundTaskInstance taskInstance)
        {
            doShit();
        }

        public void doShit()
        {
            stopwatch.Start();
            int x = 0;
            while (x < 150)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    x++;
                    stopwatch.Reset();
                    stopwatch.Start();
                    try
                    {
                        DbCon.PostLocation(1, (float)2.04 + x * 0.1f, (float)-5.42 + x * 0.1f);
                    }
                    catch
                    {
                        continue;
                    }
                    continue;
                }
                continue;
            }
        }
    }
}
    