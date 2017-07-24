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

        public IOrderedEnumerable<ImageMonthGroup> ImageMonthGroups { get; set; }

        /// <summary>
        ///     异步刷新list内容
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAsync()
        {
            _allImageList = await AllImageList.GetInstanceAsync();
            // 读取图片信息列表中的全部信息
            var imageVms = _allImageList.List.Select(img => new MyImageViewModel(img)).ToList();
            // 第二个函数是选择
            ImageMonthGroups = imageVms.GroupBy(m => m.MonthYearDate, (key, list) => new ImageMonthGroup(key, list))
                .OrderByDescending(m => m.Key);
        }
    }
}