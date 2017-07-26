using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using System.Diagnostics;

namespace Touch.Models
{
    /// <summary>
    ///     实现图片聚类
    /// </summary>
    class PhotoClustering
    {
        private List<MyImage> _photoPath = new List<MyImage>();
        private List<List<int>> _clusteringResult = new List<List<int>>();
        private bool[] _vis=new bool[1000];
        public PhotoClustering(List<MyImage> photoPath)
        {
            _photoPath = photoPath;
            _photoPath.Sort();
            for(int i=0;i<= _photoPath.Count;++i)
            {
                _vis[i] = false;
            }
            /*for (int i = 0; i < _photoPath.Count; ++i)
                Debug.WriteLine(_photoPath.ElementAt(i).DateTaken);*/
        }
        private void dfs(int nowId,List<int> group)
        {
            _vis[nowId] = true;
            double nowX = _photoPath.ElementAt(nowId).Latitude.Value;
            double nowY = _photoPath.ElementAt(nowId).Longitude.Value;
            for (int i = 0; i < _photoPath.Count; ++i)
            {
                if (!_vis[i])
                {
                    if (Math.Abs(nowX - _photoPath.ElementAt(i).Latitude.Value)< 0.001 && Math.Abs(nowY - _photoPath.ElementAt(i).Longitude.Value )< 0.001)
                    {
                        group.Add(i);
                        dfs(i, group);
                    }
                }
            }
        }
        public List<Point> getPhotoClustering()
        {
            List<Point> path = new List<Point>();
            for (int i = 0; i < _photoPath.Count; ++i)  //去掉没有经纬度的图片
            {
                if (!(_photoPath.ElementAt(i).Latitude.HasValue && _photoPath.ElementAt(i).Longitude.HasValue))
                {
                    _photoPath.RemoveAt(i);
                }
               // Debug.WriteLine(_photoPath.ElementAt(i).Latitude.ToString() + _photoPath.ElementAt(i).Longitude.ToString());
            }
            for (int i = 0; i < _photoPath.Count; ++i)
            {
                if (!_vis[i])
                {
                    List<int> group = new List<int>();
                    group.Add(i);
                    dfs(i,group);
                    _clusteringResult.Add(group);
                }
            }
            Debug.Write("end:"+_clusteringResult.Count);
            for(int i=0;i<_clusteringResult.Count;++i)
            {
                List<int> tmp = _clusteringResult.ElementAt(i);
                //Debug.Write("group:");
                path.Add( new Point(_photoPath.ElementAt(tmp[0]).Latitude.Value,
                _photoPath.ElementAt(tmp[0]).Longitude.Value));
            }
            return path;
        }
    }
}
