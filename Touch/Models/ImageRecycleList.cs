using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Touch.Models
{
    class ImageRecycleList
    {
        private List<string> pathList;
        private int count;

        public ImageRecycleList(List<string> pathList)
        {
            this.pathList = pathList;
            count = pathList.Count;
        }

        public string GetItem(int index)
        {
            return pathList[index % count];
        }
    }
}
