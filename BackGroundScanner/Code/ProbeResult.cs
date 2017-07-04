using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackGroundScanner.Code
{
    public sealed class ProbeResult
    {
        private float X;
        private float Y;
        public float Distance { get; set; }
        string MAC, SSID;
        double RSSI;
        int Frequency;

        public ProbeResult(string mac, string ssid, double rssi, int frequency, float x, float y)
        {
            MAC = mac;
            SSID = ssid;
            RSSI = rssi;
            Frequency = frequency;
            X = x;
            Y = y;
        }

        public float getX()
        {
            return X; 
        }
        public float getY()
        {
            return Y;
        }
        public int GetFrequency()
        {
            return Frequency;
        }

        public string GetMAC()
        {
            return MAC;
        }

        public double GetRSSI()
        {
            return RSSI;
        }

        public string GetSSID()
        {
            return SSID;
        }
    }
}
