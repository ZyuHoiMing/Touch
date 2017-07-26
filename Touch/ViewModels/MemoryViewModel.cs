using System.Collections.Generic;
using System.Threading.Tasks;
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
        public ImageViewModel CoverImage { get; private set; }

        /// <summary>
        ///     回忆里包含的图片
        /// </summary>
        public List<ImageViewModel> ImageViewModels { get; private set; }

        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<MemoryViewModel> GetInstanceAsync(MemoryModel memoryModel = null)
        {
            var memoryViewModel = new MemoryViewModel(memoryModel)
            {
                ImageViewModels = new List<ImageViewModel>()
            };
            if (memoryModel?.ImageModels == null)
                return memoryViewModel;
            foreach (var imageModel in memoryModel.ImageModels)
            {
                var imageViewModel = await ImageViewModel.GetInstanceAsync(imageModel);
                memoryViewModel.ImageViewModels.Add(imageViewModel);
            }
            memoryViewModel.CoverImage = memoryViewModel.ImageViewModels[0];
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