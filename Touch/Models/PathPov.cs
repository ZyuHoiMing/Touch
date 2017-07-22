using System;
using Windows.Foundation;

namespace Touch.Models
{
    /// <summary>
    ///     设置路径的视角
    /// </summary>
    public class PathPov
    {
        private Point from;
        private Point to;

        public PathPov(Point from, Point to)
        {
            this.from = from;
            this.to = to;
        }

        public int getHeading()
        {
            var tmpY = to.X - from.X; //纬度
            var tmpX = to.Y - from.Y; //经度
            if (Math.Abs(tmpY) > Math.Abs(tmpX))
                if (tmpY > 0) return 0;
                else return 180;
            if (tmpX > 0) return 90;
            return 270;
        }
    }
}