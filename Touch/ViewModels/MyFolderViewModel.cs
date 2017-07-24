using Windows.UI.Xaml;
using Touch.Models;

namespace Touch.ViewModels
{
    /// <summary>
    ///     文件夹ViewModel
    /// </summary>
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode() public class MyFolderViewModel : NotificationBase<MyFolder>
    public class MyFolderViewModel : NotificationBase<MyFolder>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public MyFolderViewModel(MyFolder myFolder = null) : base(myFolder)
        {
        }

        /// <summary>
        ///     文件夹路径
        /// </summary>
        public string FolderPath
        {
            get { return This.FolderPath; }
            set { SetProperty(This.FolderPath, value, () => This.FolderPath = value); }
        }

        /// <summary>
        ///     访问权限
        /// </summary>
        public string AccessToken
        {
            get { return This.AccessToken; }
            set { SetProperty(This.AccessToken, value, () => This.AccessToken = value); }
        }

        /// <summary>
        ///     图标
        /// </summary>
        public string ItemSymbol { get; set; } = Application.Current.Resources["Folder"] as string;

        /// <summary>
        ///     删除按钮是否可见
        /// </summary>
        public Visibility IsDeleteVisibility { get; set; } = Visibility.Visible;
#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as MyFolderViewModel;
            return o != null && o.FolderPath == FolderPath;
        }
    }
}