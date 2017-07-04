﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;

namespace BackGroundScanner.Code
{
    class ProbeAdapter
    {
        bool ActiveDevice = false;
        WiFiAdapter WifiDevice;
        

        public ProbeAdapter()
        {
            SetWifiDevice();
        }
        public IEnumerable<ProbeResult> ScanResults(List<string> MACs)
        {
            List<ProbeResult> results = new List<ProbeResult>();
            if (ActiveDevice)
            {
                Scan();                                                                                                   //check
                foreach (WiFiAvailableNetwork network in WifiDevice.NetworkReport.AvailableNetworks.Where(x => MACs.Contains(x.Bssid)).OrderByDescending(x => x.NetworkRssiInDecibelMilliwatts))
                {
                    results.Add(new ProbeResult(network.Bssid, network.Ssid, network.NetworkRssiInDecibelMilliwatts, network.ChannelCenterFrequencyInKilohertz));
                }
            }
            return results;
        }
        private async void SetWifiDevice()
        {
            var access = await WiFiAdapter.RequestAccessAsync();
            if (access != WiFiAccessStatus.Allowed)
            {
                throw new Exception("WiFiAccessStatus not allowed");
            }
            else
            {
                var WifiDeviceScan = await WiFiAdapter.FindAllAdaptersAsync();
                if (WifiDeviceScan.Count >= 1)
                {
                    WifiDevice = WifiDeviceScan[0];
                    ActiveDevice = true;
                }
            }
        }
        public async void Scan()
        {
            if (ActiveDevice) await WifiDevice.ScanAsync();
            else return;
        }
    }
}
