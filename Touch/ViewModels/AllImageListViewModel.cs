using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Touch.Models;

namespace Touch.ViewModels
{
    /// <summary>
    ///     所有文件夹里的图片的View Model，并且按照月份分好组了
    /// </summary>
    public class AllImageListViewModel : NotificationBase
    {
        private AllImageList _allImageList;

        public IEnumerable<ImageMonthGroup> ImageMonthGroups { get; private set; }

        /// <summary>
        ///     异步刷新list内容
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAsync()
        {
            _allImageList = await AllImageList.GetInstanceAsync();

            //ImageMonthGroups = _allImageList.List.GroupBy(img => new MyImageViewModel(img).MonthYearDateTaken,
            //    (key, list) => new ImageMonthGroup(key, list));
        }
    }
}