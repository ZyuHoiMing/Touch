using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Touch.Models;

namespace Touch.ViewModels
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     回忆ViewModel
    /// </summary>
    public class MyMemoryViewModel : NotificationBase<MyMemory>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public MyMemoryViewModel(MyMemory myMemory = null) : base(myMemory)
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
        ///     回忆里的名字
        /// </summary>
        public string MemoryName
        {
            get { return This.MemoryName; }
            set { SetProperty(This.MemoryName, value, () => This.MemoryName = value); }
        }

        /// <summary>
        ///     回忆里包含的图片
        /// </summary>
        public List<ImageModel> Images
        {
            get { return This.Images; }
            set { SetProperty(This.Images, value, () => This.Images = value); }
        }

        /// <summary>
        ///     回忆图片里的第一个图片作为封面
        /// </summary>
        public BitmapImage Bitmap
        {
            get { return Images != null && Images.Count > 0 ? Images[0].Bitmap : null; }
        }

        /// <summary>
        ///     添加新回忆按钮是否可见
        /// </summary>
        public Visibility IsAddVisibility { get; set; } = Visibility.Collapsed;
#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as MyMemoryViewModel;
            return o != null && o.KeyNo == KeyNo;
        }
    }
}