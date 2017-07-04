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

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "Running";
            var access = await BackgroundExecutionManager.RequestAccessAsync();
            var task = new BackgroundTaskBuilder { Name = "Scanner", TaskEntryPoint=typeof(BackGroundScanner.BackGroundScanning).ToString()};
            task.SetTrigger(new SystemTrigger(SystemTriggerType.InternetAvailable, false));
            //task.SetTrigger(new TimeTrigger(15, false));
            task.Register();
        }
    }
}
