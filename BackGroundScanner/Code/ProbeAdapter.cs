using System;
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
        public IEnumerable<ProbeResult> ScanResults(List<ApData> MACs)
        {
            List<string> macStrings = new List<string>();
            List<ProbeResult> results = new List<ProbeResult>();
            foreach (ApData apData in MACs)
            {
                macStrings.Add(apData.MAC);
            }
            if (ActiveDevice)
            {
                Scan();                                                                                                   //check
                foreach (WiFiAvailableNetwork network in WifiDevice.NetworkReport.AvailableNetworks.Where(x => macStrings.Contains(x.Bssid)).OrderByDescending(x => x.NetworkRssiInDecibelMilliwatts))
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

        public double calculateDistance(double levelInDb, double freqInMHz)
        {
            double exp = (27.55 - (20 * Math.Log10(freqInMHz)) + Math.Abs(levelInDb)) / 20.0;
            return Math.Pow(10.0, exp);
        }
    }
}
