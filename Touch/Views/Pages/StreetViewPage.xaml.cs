using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Windows.Foundation;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Touch.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class StreetViewPage : Page
    {
        private readonly List<Point> _pathPoint = new List<Point>();
        //
        public StreetViewPage()
        {
            InitializeComponent();
            var localFolder = ApplicationData.Current.LocalFolder;
            Debug.WriteLine(localFolder.Path);
            var existingFile = localFolder.TryGetItemAsync("Test.html");
            if (existingFile == null)
            {
                Debug.WriteLine("null");
            }
            else
            {
                var uri = new Uri("ms-appx-web:///Web/Test.html");
                Webview1.Navigate(uri);
            }
        }

        private async void InvokeJsStart(string x, string y)
        {
            /*string[] script = { "panorama=new google.maps.StreetViewPanorama("+
            "document.getElementById('street-view'),"+
            "{position: { lat: "+x+", lng:"+y+"},"+
          "pov: { heading: 90, pitch: 0},"+
            "});" };*/
            string[] script =
            {
                "panorama.setPosition({lat:"
                + x + ",lng:" + y
                + "});" + "panorama.setVisible(true);"
            };
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await Webview1.InvokeScriptAsync("eval", new[] {"setIsGetPath()"});
                var result = await Webview1.InvokeScriptAsync("eval", script);
                //Debug.WriteLine(result);
            });
        }

        //嵌入移动
        private async void InvokeJsMove(string x, string y, string heading)
        {
            string[] script =
            {
                "panorama.setPosition({lat:"
                + x + ",lng:" + y
                + "});panorama.setPov({" +
                "heading:" + heading + ",pitch:0})"
            };
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var result = await Webview1.InvokeScriptAsync("eval", script);
                //Debug.WriteLine(result);
            });
        }

        //嵌入朝向
        private void InvokeJsHeading(int tmpNodeNum)
        {
            Debug.WriteLine("run");
            var delay = TimeSpan.FromSeconds(2);
            var delayTimer = ThreadPoolTimer.CreateTimer
            (async source =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    string[] insertMessage =
                    {
                        "insertMark(" +
                        _pathPoint[tmpNodeNum].X +
                        "," + _pathPoint[tmpNodeNum].Y +
                        "," + tmpNodeNum + ")"
                    };
                    var result = await Webview1.InvokeScriptAsync("eval", insertMessage);
                    /*string[] args = {"setMarkHeading()"};
                    result = await Webview1.InvokeScriptAsync("eval", args); //镜头转换，待改善
                    Debug.WriteLine("result" + result);*/
                });
            }, delay);
        }

        //得到路径
        private async void InvokeJsGetPath()
        {
            string[] script =
            {
                "getPath(40.75682475,-73.9883746666667, 40.7583754444444,-73.9851607777778)"
            };
            var result = await Webview1.InvokeScriptAsync("eval", script);
        }

        //测试得到路径
        private void testGetPath()
        {
            var completed = false;
            var delay = TimeSpan.FromSeconds(0.5);
            var delayTimer = ThreadPoolTimer.CreateTimer
                // ReSharper disable once ImplicitlyCapturedClosure
                (source => { completed = true; }, delay, async source =>
                {
                    await Dispatcher.RunAsync(
                        CoreDispatcherPriority.High,
                        async () =>
                        {
                            if (!completed) return;
                            string[] args = {"testIsGetPath()"};
                            var result = await Webview1.InvokeScriptAsync("eval", args);
                            if (result == "Y")
                            {
                                await Webview1.InvokeScriptAsync("eval", new[] {"setIsGetPath()"});
                                var tmp = await Webview1.InvokeScriptAsync("eval", new[] {"getPathPoint()"});
                                var pathArray = tmp.Split('\n');
                                for (var i = 0; i < pathArray.Length; ++i)
                                {
                                    Debug.WriteLine(pathArray[i]);
                                    if (pathArray[i].Length >= 3)
                                    {
                                        var pointArray = pathArray[i].Split(',');
                                        var lat = Convert.ToDouble(pointArray[0]);
                                        var lng = Convert.ToDouble(pointArray[1]);
                                        _pathPoint.Add(new Point(lat, lng));
                                    }
                                }
                                startWalk();
                            }
                            else
                            {
                                testGetPath();
                            }
                            // Timer completed.
                        });
                });
        }

        //测试点击label
        private void TestClick()
        {
            var completed = false;
            var delay = TimeSpan.FromSeconds(0.5);
            var delayTimer = ThreadPoolTimer.CreateTimer
                // ReSharper disable once ImplicitlyCapturedClosure
                (source => { completed = true; }, delay, async source =>
                {
                    await Dispatcher.RunAsync(
                        CoreDispatcherPriority.High,
                        async () =>
                        {
                            if (!completed) return;
                            string[] args = {"getClick()"};
                            var result = await Webview1.InvokeScriptAsync("eval", args);
                            if (result == "click")
                                ShowPath(1);
                            else
                                TestClick();
                            // Timer completed.
                        });
                });
        }

        //显示路径
        private void ShowPath(int nodeNum)
        {
            var completed = false;
            var delay = TimeSpan.FromSeconds(2);
            var delayTimer = ThreadPoolTimer.CreateTimer
            (source =>
            {
                //
                // TODO: Work
                //
                var x = _pathPoint.ElementAt(nodeNum).X.ToString(CultureInfo.CurrentCulture);
                var y = _pathPoint.ElementAt(nodeNum).Y.ToString(CultureInfo.CurrentCulture);
                Debug.WriteLine(x);
                Debug.WriteLine(y);
                var pathpov = new PathPov(_pathPoint.ElementAt(nodeNum - 1), _pathPoint.ElementAt(nodeNum));
                var tmpheading = pathpov.GetHeading().ToString();
                InvokeJsMove(x, y, tmpheading);
                //
                // Update the UI thread by using the UI core dispatcher.
                //
                completed = true;
            }, delay, async source =>
            {
                //
                // Update the UI thread by using the UI core dispatcher.
                //
                await Dispatcher.RunAsync(
                    CoreDispatcherPriority.High,
                    () =>
                    {
                        Debug.WriteLine(nodeNum);
                        if (!completed) return;
                        if (nodeNum == _pathPoint.Count - 1)
                        {
                            Debug.WriteLine("finish");
                            InvokeJsHeading(nodeNum);
                        }
                        else
                        {
                            ShowPath(nodeNum+1);
                        }
                    });
            });
        }

        //开始行走
        private void startWalk()
        {
            if (_pathPoint.Count == 0)
            {
                Debug.WriteLine("no path");
            }
            else
            {
                var x = _pathPoint.ElementAt(0).X.ToString(CultureInfo.CurrentCulture);
                var y = _pathPoint.ElementAt(0).Y.ToString(CultureInfo.CurrentCulture);
                InvokeJsStart(x, y);
                Debug.WriteLine("test run");
                InvokeJsHeading(0);
                var delay = TimeSpan.FromSeconds(2);
                var delayTimer = ThreadPoolTimer.CreateTimer
                (source =>
                {
                    if (_pathPoint.Count > 1)
                    {
                        TestClick();
                    }
                    else
                    {
                        Debug.WriteLine("can't move");
                    }
                }, delay);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testGetPath();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InvokeJsGetPath();
        }
    }
}