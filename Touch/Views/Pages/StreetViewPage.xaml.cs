using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Touch.Models;
using Touch.ViewModels;

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

        private List<ImageViewModel> _test;

        private List<Point> _wayPoint = new List<Point>();

        //
        public StreetViewPage()
        {
            InitializeComponent();
            /*var localFolder = ApplicationData.Current.LocalFolder;
            Debug.WriteLine(localFolder.Path);
            var existingFile = localFolder.TryGetItemAsync("Test.html");
            if (existingFile == null)
            {
                Debug.WriteLine("null");
            }*/
            var uri = new Uri("ms-appx-web:///Web/Test.html");
            Webview1.Navigate(uri);
            //var test = new List<ImageViewModel>();
            //ImageViewModel tmp = new ImageViewModel();
            //tmp.DateTaken = new DateTime(2016, 09, 01, 15, 45, 03);
            //tmp.Latitude = 40.8074377777778;
            //tmp.Longitude =-73.9615143611111;
            //test.Add(tmp);


            //ImageViewModel tmp3 = new ImageViewModel();
            //tmp3.DateTaken = new DateTime(2016, 09, 02, 11, 40, 47);
            //tmp3.Latitude = 40.8074971666667;
            //tmp3.Longitude = -73.9622030555556;
            //test.Add(tmp3);

            //ImageViewModel tmp2 = new ImageViewModel();
            //tmp2.DateTaken = new DateTime(2016, 09, 01, 19, 19, 46);
            //tmp2.Latitude = 40.8074971666667;
            //tmp2.Longitude = -73.9622030555556;
            //test.Add(tmp2);

            //ImageViewModel tmp4 = new ImageViewModel();
            //tmp4.DateTaken = new DateTime(2016, 09, 02, 12, 47, 18);
            //tmp4.Latitude = 40.7583754444444 ;
            //tmp4.Longitude =- 73.9851607777778;
            //test.Add(tmp4);

            //ImageViewModel tmp5 = new ImageViewModel();
            //tmp5.DateTaken = new DateTime(2016, 09, 02, 12, 59, 39);
            //tmp5.Latitude = 40.75682475 ;
            //tmp5.Longitude =- 73.9883746666667;
            //test.Add(tmp5);

            //ImageViewModel tmp6 = new ImageViewModel();
            //tmp6.DateTaken = new DateTime(2016, 09, 02, 15, 18, 57);
            //tmp6.Latitude = 40.7566056666667 ;
            //tmp6.Longitude =- 73.9884400555556;
            //test.Add(tmp6);

            //ImageViewModel tmp7 = new ImageViewModel();
            //tmp7.DateTaken = new DateTime(2016, 09, 02, 15, 49, 23);
            //tmp7.Latitude = 40.7487146666667;
            //tmp7.Longitude = - 73.9845638055556;
            //test.Add(tmp7);

            //ImageViewModel tmp8 = new ImageViewModel();
            //tmp8.DateTaken = new DateTime(2016, 09, 02, 15, 49, 31);
            //tmp8.Latitude = 40.7430319722222 ;
            //tmp8.Longitude =- 73.9880073611111;
            //test.Add(tmp8);

            //ImageViewModel tmp9 = new ImageViewModel();
            //tmp9.DateTaken = new DateTime(2016, 09, 02, 16, 58, 31);
            //tmp9.Latitude = 40.7430855277778 ;
            //tmp9.Longitude =- 73.9879168333333;
            //test.Add(tmp9);

            //ImageViewModel tmp10 = new ImageViewModel();
            //tmp10.DateTaken = new DateTime(2016, 09, 02, 17, 32, 18);
            //tmp10.Latitude = 40.7484751944444;
            //tmp10.Longitude = - 73.9847475555556;
            //test.Add(tmp10);

            //ImageViewModel tmp11 = new ImageViewModel();
            //tmp11.DateTaken = new DateTime(2016, 09, 04, 12, 11, 01);
            //tmp11.Latitude = 40.7482575555556 ;
            //tmp11.Longitude =- 73.9857225555556;
            //test.Add(tmp11);

            //ImageViewModel tmp12 = new ImageViewModel();
            //tmp12.DateTaken = new DateTime(2016, 09, 05, 13, 35, 15);
            //tmp12.Latitude = 40.7406280833333;
            //tmp12.Longitude = - 73.9931034722222;
            //test.Add(tmp12);

            //ImageViewModel tmp13 = new ImageViewModel();
            //tmp13.DateTaken = new DateTime(2016, 09, 05, 16, 34, 32);
            //tmp13.Latitude = 40.7823715555556 ;
            //tmp13.Longitude =- 73.9740036944444;
            //test.Add(tmp13);

            //ImageViewModel tmp14 = new ImageViewModel();
            //tmp14.DateTaken = new DateTime(2016, 09, 08, 11, 07, 55);
            //tmp14.Latitude = 40.7815460555556 ;
            //tmp14.Longitude =- 73.9749867777778;
            //test.Add(tmp14);

            //var photoClustering = new PhotoClustering(test);
            //_wayPoint = photoClustering.getPhotoClustering();
            //Debug.WriteLine("run");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _test = e.Parameter as List<ImageViewModel>;
            var photoClustering = new PhotoClustering(_test);
            _wayPoint = photoClustering.GetPhotoClustering();
            Debug.WriteLine("run");
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

        //得到路径插入中途点
        private async void InvokeJsGetPath()
        {
            var script = new string [1];
            for (var i = 1; i < _wayPoint.Count - 1; ++i)
                script[0] += "addWayPoint(" + _wayPoint.ElementAt(i).X + ", " + _wayPoint.ElementAt(i).Y + ");";
            script[0] += "getPath(" + _wayPoint.ElementAt(0).X + "," + _wayPoint.ElementAt(0).Y + ","
                         + _wayPoint.ElementAt(_wayPoint.Count - 1).X + "," +
                         _wayPoint.ElementAt(_wayPoint.Count - 1).Y + ");";
            //var result = await Webview1.InvokeScriptAsync("eval", script);
            //var result=await Webview1.InvokeScriptAsync("eval", new string[] { "addWayPoint(40.7548751831055, -73.9842300415039);" });
            var result = await Webview1.InvokeScriptAsync("eval", script);
            //Debug.WriteLine("rerutn"+result);
        }

        //在路径中加入
        private void InsertWayPoint()
        {
            for (var i = 1; i < _wayPoint.Count; ++i)
            {
                var x = _wayPoint[i].X * 1000;
                var y = _wayPoint[i].Y * 1000;
                var tmpx = _pathPoint[0].X * 1000;
                var tmpy = _pathPoint[0].Y * 1000;
                var tmp = (tmpx - x) * (tmpx - x) + (tmpy - y) * (tmpy - y);
                //Debug.WriteLine(tmp);
                for (var j = 1; j < _pathPoint.Count; ++j)
                {
                    tmpx = _pathPoint[j].X * 1000;
                    tmpy = _pathPoint[j].Y * 1000;
                    var newtmp = (tmpx - x) * (tmpx - x) + (tmpy - y) * (tmpy - y);
                    if (newtmp < tmp)
                    {
                        tmp = newtmp;
                    }
                    else
                    {
                        _pathPoint.Insert(j - 1, _wayPoint[i]);
                        break;
                    }
                    //Debug.WriteLine(newtmp);
                }
            }
            //Debug.WriteLine("finish insert");
        }

        //测试得到路径
        private void TestGetPath()
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
                                if (pathArray.Length > 100)
                                {
                                }
                                foreach (var t in pathArray)
                                    if (t.Length >= 3)
                                    {
                                        var pointArray = t.Split(',');
                                        var lat = Convert.ToDouble(pointArray[0]);
                                        var lng = Convert.ToDouble(pointArray[1]);
                                        _pathPoint.Add(new Point(lat, lng));
                                    }
                                //嵌入_waypoint点
                                InsertWayPoint();
                                StartWalk();
                            }
                            else
                            {
                                TestGetPath();
                            }
                            // Timer completed.
                        });
                });
        }

        //测试点击label
        private void TestClick(int nodeNum, int wayNum)
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
                                ShowPath(nodeNum, wayNum);
                            else
                                TestClick(nodeNum, wayNum);
                            // Timer completed.
                        });
                });
        }

        //显示路径
        private void ShowPath(int nodeNum, int wayNum)
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
                //Debug.WriteLine("get point"+tmp);
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
                            //Debug.WriteLine("finish");
                            InvokeJsHeading(nodeNum);
                        }
                        else if (wayNum < _wayPoint.Count - 1 && _wayPoint[wayNum] == _pathPoint[nodeNum])
                        {
                            InvokeJsHeading(nodeNum);
                            TestClick(nodeNum + 1, wayNum + 1);
                        }
                        else
                        {
                            ShowPath(nodeNum + 1, wayNum);
                        }
                    });
            });
        }

        //开始行走
        private void StartWalk()
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
                        TestClick(1, 1);
                    else
                        Debug.WriteLine("can't move");
                }, delay);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestGetPath();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*_wayPoint.Add(new Point(40.75682475, -73.9883746666667));
            _wayPoint.Add(new Point(40.754874, -73.984228));
            _wayPoint.Add(new Point(40.7583754444444,- 73.9851607777778));*/
            InvokeJsGetPath();
        }
    }
}