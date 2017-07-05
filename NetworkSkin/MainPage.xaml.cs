using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Background;
using BackGroundScanner.Code;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NetworkSkin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            textBlock.Text = "Stopped";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "Stopped";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "Running";
            doShit();
            //Task x = new Task(()=>doShit());
            //x.Start();
            //var x = RegisterBackgroundTask(typeof(BackGroundScanner.BackGroundScanning).ToString(), "Scanner", new SystemTrigger(SystemTriggerType.InternetAvailable, false), null);


        }
        public void doShit()
        {
            Stopwatch stopwatch = new Stopwatch();
            DbCall DbCon = new DbCall();
            ProbeAdapter adapter = new ProbeAdapter();
            StaticData Data = new StaticData();
            stopwatch.Start();
            int x = 0;
            while (x < 10)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    x++;
                    stopwatch.Reset();
                    stopwatch.Start();
                    try
                    {
                        List<ProbeResult> Results = new List<ProbeResult>(); 
                        while (true)
                        {
                            if (Results.Count < 3) Results = adapter.ScanResults(StaticData.APData).ToList();
                            else break;
                        }
                        foreach (var result in Results)
                        {
                            result.Distance = (float)calculateDistance(result.GetRSSI(), result.GetFrequency());
                        }

                        ProbeResult[] distanceResults = new ProbeResult[3];
                        distanceResults[0] = null;
                        distanceResults[1] = null;
                        distanceResults[2] = null;
                        bool tmp1, tmp2, tmp3;

                        foreach (var result in Results)
                        {
                            tmp1 = false; tmp2 = false; tmp3 = false;
                            //Fill if  nulls;
                            if (distanceResults[0] == null) distanceResults[0] = result;
                            else if (distanceResults[1] == null) distanceResults[1] = result;
                            else if (distanceResults[2] == null) distanceResults[2] = result;
                            else //No nulls?
                            {
                                for (int i = 0; i < 3 ; i++)
                                {
                                    if (comp(result.Distance, distanceResults[i].Distance))
                                    {
                                        switch (i)
                                        {
                                            case 0:
                                                tmp1 = true;
                                                break;
                                            case 1:
                                                tmp2 = true;
                                                break;
                                            case 2:
                                                tmp3 = true;
                                                break;
                                        }
                                    }
                                }
                                if ((tmp1 && tmp2) && tmp3)
                                {
                                    if (comp(distanceResults[0].Distance, distanceResults[1].Distance))
                                    {
                                        if (comp(distanceResults[1].Distance, distanceResults[2].Distance)) distanceResults[2] = result;
                                        else distanceResults[1] = result;
                                    }
                                    else
                                    {
                                        if (comp(distanceResults[0].Distance, distanceResults[1].Distance)) distanceResults[1] = result;
                                        else distanceResults[0] = result;
                                    }
                                }
                                else if (tmp1 && tmp2)
                                {
                                    if (comp(distanceResults[0].Distance, distanceResults[1].Distance)) distanceResults[1] = result;
                                    else distanceResults[0] = result;
                                }
                                else if (tmp2 && tmp3)
                                {
                                    if (comp(distanceResults[1].Distance, distanceResults[2].Distance)) distanceResults[2] = result;
                                    else distanceResults[1] = result;
                                }
                                else if (tmp1 && tmp3)
                                {
                                    if (comp(distanceResults[0].Distance, distanceResults[2].Distance)) distanceResults[2] = result;
                                    else distanceResults[0] = result;
                                }
                                else if (tmp1) distanceResults[0] = result;
                                else if (tmp2) distanceResults[1] = result;
                                else if (tmp3) distanceResults[2] = result;
                            }
                        }
                        int asbc = 0;
                        Vector2 v2 = GetPosition(new Vector2(distanceResults[0].getX(), distanceResults[0].getY()),
                                                new Vector2(distanceResults[1].getX(), distanceResults[1].getY()),
                                                new Vector2(distanceResults[2].getX(), distanceResults[2].getY()),
                                                distanceResults[0].Distance, distanceResults[1].Distance, distanceResults[2].Distance);
                        Debug.WriteLine("brap");
                        int asc = 0;
                        DbCon.PostLocation(1, v2.X, v2.Y);
                    }
                    catch
                    {
                        
                    }
                    continue;
                }
                continue;
            }
        }

        private bool comp(float st, float nd)
        {
            if (st >= nd) return false;
            else return true;
        }

        public static BackgroundTaskRegistration RegisterBackgroundTask(string taskEntryPoint,
                                                                string taskName,
                                                                IBackgroundTrigger trigger,
                                                                IBackgroundCondition condition)
        {

            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {

                if (cur.Value.Name == taskName)
                {
                    return (BackgroundTaskRegistration)(cur.Value);
                }
            }

            var builder = new BackgroundTaskBuilder();

            builder.Name = taskName;
            builder.TaskEntryPoint = taskEntryPoint;
            builder.SetTrigger(trigger);

            if (condition != null)
            {

                builder.AddCondition(condition);
            }

            BackgroundTaskRegistration task = builder.Register();

            return task;
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

        public double calculateDistance(double levelInDb, double freqInMHz)
        {
            double exp = (27.55 - (20 * Math.Log10(freqInMHz)) + Math.Abs(levelInDb)) / 20.0;
            return Math.Pow(10.0, exp);
        }

    }
}
