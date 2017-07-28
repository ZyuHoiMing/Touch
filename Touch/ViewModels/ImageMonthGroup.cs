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
    public class ImageMonthGroup : IGrouping<MonthYearDateTime, ImageViewModel>
    {
        private readonly ObservableCollection<ImageViewModel> _imageViewModels;

        public ImageMonthGroup(MonthYearDateTime key, IEnumerable<ImageViewModel> items)
        {
            Key = key;
            _imageViewModels = new ObservableCollection<ImageViewModel>(items);
        }

        public MonthYearDateTime Key { get; }

        public IEnumerator<ImageViewModel> GetEnumerator()
        {
            return _imageViewModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _imageViewModels.GetEnumerator();
        }
    }
}