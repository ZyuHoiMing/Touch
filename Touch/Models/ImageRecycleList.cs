using System.Collections.Generic;

namespace Touch.Models
{
    internal class ImageRecycleList
    {
        private readonly int _count;
        private readonly List<string> _pathList;

        public ImageRecycleList(List<string> pathList)
        {
            _pathList = pathList;
            _count = pathList.Count;
        }

        public string GetItem(int index)
        {
            if (index % _count < 0)
                return _pathList[index % _count + _count];
            return _pathList[index % _count];
        }
    }
}