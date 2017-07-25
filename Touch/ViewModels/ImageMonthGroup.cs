using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Touch.ViewModels
{
    /// <summary>
    ///     图片按月份分组
    /// </summary>
    public class ImageMonthGroup : IGrouping<string, ImageViewModel>
    {
        private readonly ObservableCollection<ImageViewModel> _myImageVms;

        public ImageMonthGroup(string key, IEnumerable<ImageViewModel> items)
        {
            Key = key;
            _myImageVms = new ObservableCollection<ImageViewModel>(items);
        }

        public string Key { get; }

        public IEnumerator<ImageViewModel> GetEnumerator()
        {
            return _myImageVms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _myImageVms.GetEnumerator();
        }
    }
}