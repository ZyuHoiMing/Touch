using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
namespace Touch.Views
{
    /// <summary>
    /// 设置路径的视角
    /// </summary>
    public class pathPov
    {
        private Point from;
        private Point to;
        public pathPov(Point from,Point to)
        {
            this.from = from;
            this.to = to;
        }
         public int getHeading()
        {
            double tmpY = to.X - from.X;//纬度
            double tmpX = to.Y - from.Y;//经度
            if (System.Math.Abs(tmpY)>System.Math.Abs(tmpX))
            {
                if (tmpY > 0) return 0;
                else return 180;
            }
            else
            {
                if (tmpX > 0) return 90;
                else return 270;
            }
        }
    }
}
