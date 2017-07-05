using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace NetworkSkin
{
    class DbCall
    {
        public async void PostLocation(int empID, float x, float y)
        {
            using (var client = new HttpClient())
            {
                var response =
                    //TODO: Check format string
                    await client.GetStringAsync(String.Format("http://145.24.222.153/Webserver/NewPosition/?empId={0}&X={1}&Y={2}", empID.ToString(), x.ToString(), y.ToString()));
            }
        }
        public async void CheckCon()
        {
            using (var client = new HttpClient())
            {
                var response =
                    //TODO: Check format string
                    await client.GetStringAsync("http://145.24.222.153/Webserver");
            }
        }

    }
}
