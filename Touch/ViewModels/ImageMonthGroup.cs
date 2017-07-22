using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Touch.Models;

namespace Touch.ViewModels
{
    /// <summary>
    ///     图片按月份分组
    /// </summary>
    public class ImageMonthGroup : IGrouping<MonthYearDateTime, MyImageViewModel>
    {
        private readonly ObservableCollection<MyImageViewModel> _myImageVms;

        public ImageMonthGroup(MonthYearDateTime key, IEnumerable<MyImageViewModel> items)
        {
            Key = key;
            _myImageVms = new ObservableCollection<MyImageViewModel>(items);
        }

        public MonthYearDateTime Key { get; }

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