using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Touch.Models;

namespace Touch.ViewModels
{
    /// <summary>
    ///     图库图片list
    /// </summary>
    public class GalleryImageListViewModel : NotificationBase
    {
        /// <summary>
        ///     所有的文件夹里的图片
        /// </summary>
        private readonly List<ImageFolderList> _imageFolderLists;

        /// <summary>
        ///     所有文件夹
        /// </summary>
        private FolderList _folderList;

        /// <summary>
        ///     按月份分好类的图片list
        /// </summary>
        public IOrderedEnumerable<ImageMonthGroup> ImageMonthGroups;

        private GalleryImageListViewModel()
        {
            _imageFolderLists = new List<ImageFolderList>();
        }

        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<GalleryImageListViewModel> GetInstanceAsync()
        {
            var galleryImageListViewModel = new GalleryImageListViewModel();
            await galleryImageListViewModel.RefreshFolderListAsync();
            await galleryImageListViewModel.GroupImageAsync();
            return galleryImageListViewModel;
        }

        /// <summary>
        ///     刷新文件夹list，但不刷新图片list
        /// </summary>
        /// <returns></returns>
        public async Task RefreshFolderListAsync()
        {
            _imageFolderLists.Clear();
            _folderList = await FolderList.GetInstanceAsync();
            foreach (var folderModel in _folderList.FolderModels)
            {
                var imageFolderList = await ImageFolderList.GetInstanceAsync(folderModel);
                _imageFolderLists.Add(imageFolderList);
            }
        }

        /// <summary>
        ///     刷新当前文件夹list里的所有图片（不刷新文件夹list）
        /// </summary>
        /// <returns></returns>
        public async Task RefreshImageListAsync()
        {
            foreach (var imageFolderList in _imageFolderLists)
                await imageFolderList.RefreshAsync();
            await GroupImageAsync();
        }

        /// <summary>
        ///     按月份分类图片
        /// </summary>
        private async Task GroupImageAsync()
        {
            // 先初始化所有图片成一个list
            var imageViewModels = new List<ImageViewModel>();
            foreach (var imageFolderList in _imageFolderLists)
            foreach (var imageModel in imageFolderList.ImageModels)
            {
                var imageViewModel = await ImageViewModel.GetInstanceAsync(imageModel);
                imageViewModels.Add(imageViewModel);
            }
            ImageMonthGroups = imageViewModels
                .GroupBy(m => m.MonthYearDate, (key, list) => new ImageMonthGroup(key, list))
                .OrderByDescending(m => m.Key.WholeDateTime.Year).ThenByDescending(m => m.Key.WholeDateTime.Month);
        }
    }
}