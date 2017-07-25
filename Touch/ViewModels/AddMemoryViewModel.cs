using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Touch.Models;

namespace Touch.ViewModels
{
    /// <summary>
    ///     添加回忆的VM
    /// </summary>
    public class AddMemoryViewModel : NotificationBase
    {
        /// <summary>
        ///     图片个数
        /// </summary>
        private int _imageNum;

        /// <summary>
        ///     图片list
        /// </summary>
        private IReadOnlyList<StorageFile> _images;

        /// <summary>
        ///     回忆列表
        /// </summary>
        private readonly MemoryListViewModel _memoryListVm;

        public AddMemoryViewModel(MemoryListViewModel memoryListVm)
        {
            _memoryListVm = memoryListVm;
        }

        /// <summary>
        ///     图片个数
        /// </summary>
        public int ImageNum
        {
            get { return _imageNum; }
            set { SetProperty(ref _imageNum, value); }
        }

        /// <summary>
        ///     选择图片button点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SelectImage_OnClick(object sender, RoutedEventArgs e)
        {
            // 如果是添加新文件夹的按钮
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            _images = await picker.PickMultipleFilesAsync();
            ImageNum = _images.Count;
        }

        /// <summary>
        ///     确定按钮点击
        /// </summary>
        public async void PrimaryButtonClick(string memoryName)
        {
            //var imageList = await ImageList.GetInstanceAsync(_images);
            //var myMemoryVm = new MyMemoryViewModel
            //{
            //    KeyNo = -1,
            //    MemoryName = memoryName,
            //    Images = imageList.List
            //};
            //_memoryListVm.Add(myMemoryVm);
        }
    }
}