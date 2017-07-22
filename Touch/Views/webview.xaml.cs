using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.UI.Core;
using Windows.System.Threading;
using Windows.Foundation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Touch.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class webview : Page
    {
        private List<Point> pathPoint=new List<Point>();
        public webview()
        {
            this.InitializeComponent();
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Debug.WriteLine(localFolder.Path);
            var existingFile = localFolder.TryGetItemAsync("test.html");
            if (existingFile == null)
            {
                Debug.WriteLine("null");
            }
            else {
                Uri uri = new Uri("ms-appx-web:///web/test.html");
                webview1.Navigate(uri);
            }
            //insert
            pathPoint.Add(new Point(22.277782, 114.170241));
            pathPoint.Add(new Point(22.277753, 114.169953));
            pathPoint.Add(new Point(22.277759, 114.169793));
            pathPoint.Add(new Point(22.277759, 114.169415));
            pathPoint.Add(new Point(22.277759, 114.169179));
            pathPoint.Add(new Point(22.277841, 114.168825));
            pathPoint.Add(new Point(22.277968, 114.168482));
            pathPoint.Add(new Point(22.278107, 114.168546));
        }
        public async void invokeJsStart(string  x,string y)
        {
            /*string[] script = { "panorama=new google.maps.StreetViewPanorama("+
            "document.getElementById('street-view'),"+
            "{position: { lat: "+x+", lng:"+y+"},"+
          "pov: { heading: 90, pitch: 0},"+
            "});" };*/
            string[] script = { "panorama.setPosition({lat:"
            +x+",lng:"+y
            +"});" };
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
             {
                 string result = await webview1.InvokeScriptAsync("eval", script);
                //Debug.WriteLine(result);
            });
        }
        public async void invokeJsClick()
        {
            string[] args = { "getClick()" };
            var result = await webview1.InvokeScriptAsync("eval", args);
            Debug.WriteLine("result" + result.ToString());
        }
        public async void invokeJsMove(string x, string y, string heading)
        {
            string[] script = { "panorama.setPosition({lat:"
            +x+",lng:"+y
            +"});panorama.setPov({"+
            "heading:"+heading+",pitch:0})" };
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                string result = await webview1.InvokeScriptAsync("eval", script);
                //Debug.WriteLine(result);
            });
        }
        public void invokeJsHeading()
        {
            TimeSpan delay = TimeSpan.FromSeconds(2);
            ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer
            (async (source) =>
           {
               await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
               {
                   string[] args = { "setMarkHeading()" };
                   string result = await webview1.InvokeScriptAsync("eval", args);//镜头转换，待改善
                   Debug.WriteLine("result" + result);
               });
               testClick();
           }, delay);
        }
        public void testClick()
        {
            bool completed = false;
            TimeSpan delay = TimeSpan.FromSeconds(0.5);
            ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer
            ( (source) =>
            {
                completed = true;
            }, delay, async (source) =>
            {
                await Dispatcher.RunAsync(
                    CoreDispatcherPriority.High,
                    async () =>
                    {
                        //
                        // UI components can be accessed within this scope.
                        //

                        if (completed)
                        {
                            string result; 
                            string[] args = { "getClick()" };
                            result = await webview1.InvokeScriptAsync("eval", args);
                            if (result == "click")
                            {
                                Debug.WriteLine("click");
                                //testClick();
                            }
                            else
                            {
                                Debug.WriteLine("not click now");
                                testClick();
                            }
                            // Timer completed.
                        }
                        else
                        {
                            // Timer cancelled.
                        }
                    });
            });
        }
        public void show_path(int nodeNum)
        {
            bool completed = false;
            TimeSpan delay = TimeSpan.FromSeconds(2);
            ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer
            ((source) =>
            {
                //
                // TODO: Work
                //
                string x=pathPoint.ElementAt(nodeNum).X.ToString();
                string y = pathPoint.ElementAt(nodeNum).Y.ToString();
                Debug.WriteLine(x);
                Debug.WriteLine(y);
                pathPov pathpov = new pathPov(pathPoint.ElementAt(nodeNum-1),pathPoint.ElementAt(nodeNum));
                string tmpheading=pathpov.getHeading().ToString();
                invokeJsMove(x,y,tmpheading);
                //
                // Update the UI thread by using the UI core dispatcher.
                //
                completed = true;
            }, delay, async (source) =>
            {
                //
                // Update the UI thread by using the UI core dispatcher.
                //
                await Dispatcher.RunAsync(
                    CoreDispatcherPriority.High,
                    () =>
                    {
                        if (completed)
                        {
                            if (nodeNum == pathPoint.Count - 1)
                            {
                                Debug.WriteLine("finish");
                                invokeJsHeading();
                            }
                            else
                            {
                                show_path(nodeNum + 1);
                            }
                            // Timer completed.
                        }
                        else
                        {
                            // Timer cancelled.
                        }
                    });
            });
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pathPoint.Count == 0)
            {
                Debug.WriteLine("no path");
            }
            else
            {
                string x = pathPoint.ElementAt(0).X.ToString();
                string y = pathPoint.ElementAt(0).Y.ToString();
                invokeJsStart(x, y);
                //testClick();
                TimeSpan delay = TimeSpan.FromSeconds(2);
                ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer
                ((source) =>
                {
                    if (pathPoint.Count > 1)
                    {
                        show_path(1);
                    }
                    else
                    {
                        Debug.WriteLine("can't move");
                    }
                }, delay);
            }
        }
    }
}
