using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Touch.ViewModels
{
    /// <summary>
    ///     图片按月份分组
    /// </summary>
    public class ImageMonthGroup : IGrouping<string, MyImageViewModel>
    {
        private readonly ObservableCollection<MyImageViewModel> _myImageVms;

        public ImageMonthGroup(string key, IEnumerable<MyImageViewModel> items)
        {
            Key = key;
            _myImageVms = new ObservableCollection<MyImageViewModel>(items);
        }

        public string Key { get; }

        public IEnumerator<MyImageViewModel> GetEnumerator()
        {
            return _myImageVms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _myImageVms.GetEnumerator();
        }
    }
}