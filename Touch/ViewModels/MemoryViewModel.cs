using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Touch.Models;

namespace Touch.ViewModels
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     回忆ViewModel
    /// </summary>
    public class MemoryViewModel : NotificationBase<MemoryModel>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        ///     回忆里包含的图片
        /// </summary>
        private List<ImageViewModel> _imageViewModels;

        private MemoryViewModel(MemoryModel memoryModel = null) : base(memoryModel)
        {
        }

        /// <summary>
        ///     回忆编号
        /// </summary>
        public int KeyNo
        {
            get { return This.KeyNo; }
            set { SetProperty(This.KeyNo, value, () => This.KeyNo = value); }
        }

        /// <summary>
        ///     回忆标题名称
        /// </summary>
        public string MemoryName
        {
            get { return This.MemoryName; }
            set { SetProperty(This.MemoryName, value, () => This.MemoryName = value); }
        }

        /// <summary>
        ///     回忆图片里的第一个图片作为封面
        /// </summary>
        public BitmapImage CoverImage { get; set; }

        /// <summary>
        ///     回忆里包含的图片
        /// </summary>
        public List<ImageViewModel> ImageViewModels
        {
            get { return _imageViewModels; }
            set
            {
                _imageViewModels = value;
                var imageModels = value.Select(imageViewModel => imageViewModel.ImageModel).ToList();
                This.ImageModels = imageModels;
            }
        }

        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<MemoryViewModel> GetInstanceAsync(MemoryModel memoryModel = null)
        {
            var memoryViewModel = new MemoryViewModel(memoryModel)
            {
                _imageViewModels = new List<ImageViewModel>()
            };
            if (memoryModel?.ImageModels == null)
                return memoryViewModel;
            foreach (var imageModel in memoryModel.ImageModels)
            {
                var imageViewModel = await ImageViewModel.GetInstanceAsync(imageModel);
                memoryViewModel._imageViewModels.Add(imageViewModel);
            }
            if (memoryViewModel._imageViewModels != null && memoryViewModel._imageViewModels.Count > 0)
                memoryViewModel.CoverImage = memoryViewModel._imageViewModels[0].ThumbnailImage;
            else
                memoryViewModel.CoverImage = new BitmapImage(new Uri("ms-appx:///Assets/Gray.png"));
            return memoryViewModel;
        }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as MemoryViewModel;
            return o != null && o.KeyNo == KeyNo;
        }
    }
}