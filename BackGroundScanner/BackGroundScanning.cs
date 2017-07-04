using BackGroundScanner.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Numerics;
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
                        var tempp = GetPosition(new Vector2(-11.0f, -0.13f), new Vector2(-1.66f, -7.57f),
                            new Vector2(-2.65f, -8.26f), 11.0f, 7.75f, 8.56f);
                        DbCon.PostLocation(1, tempp.X, tempp.Y);
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

        public static float Pow(float i)
        {
            return i * i;
        }

        //static function which returns the triangulated position in a vector2
        public Vector2 GetPosition(Vector2 posA, Vector2 posB, Vector2 posC, float distToA, float distToB, float distToC)
        {
            //set the variables from the parameters
            Vector2 A = posA;
            Vector2 B = posB;
            Vector2 C = posC;
            var dA = distToA;
            var dB = distToB;
            var dC = distToC;

            //calculate some stuff cant explain and not necesary to know what this does
            var W = Pow(dA) - Pow(dB) - Pow(A.X) - Pow(A.Y) + Pow(B.X) + Pow(B.Y);
            var Z = Pow(dB) - Pow(dC) - Pow(B.X) - Pow(B.Y) + Pow(C.X) + Pow(C.Y);

            //calculate the x coordinate of the target
            var x = (W * (C.Y - B.Y) - Z * (B.Y - A.Y)) / (2 * ((B.X - A.X) * (C.Y - B.Y) - (C.X - B.X) * (B.Y - A.Y)));

            //calculate the y coordinate of the target y2 is for possible errors and redundency
            var y = (W - 2 * x * (B.X - A.X)) / (2 * (B.Y - A.Y));
            var y2 = (Z - 2 * x * (C.X - B.X)) / (2 * (C.Y - B.Y));
            y = (y + y2) / 2;

            //create a new vector2 with the coordinates of the target and return it
            var P = new Vector2(x, y);
            return P;
        }

    }
}
