using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackGroundScanner.Code
{
    public sealed class ProbeResult
    {
        string MAC, SSID;
        double RSSI;
        int Frequency;

        public ProbeResult(string mac, string ssid, double rssi, int frequency)
        {
            MAC = mac;
            SSID = ssid;
            RSSI = rssi;
            Frequency = frequency;
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
