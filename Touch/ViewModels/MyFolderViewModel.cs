using Windows.UI.Xaml;
using Touch.Models;

namespace Touch.ViewModels
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class MyFolderViewModel : NotificationBase<MyFolder>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public MyFolderViewModel(MyFolder myFolder = null) : base(myFolder)
        {
        }

        public string FolderPath
        {
            get => This.FolderPath;
            set { SetProperty(This.FolderPath, value, () => This.FolderPath = value); }
        }

        public string AccessToken
        {
            get => This.AccessToken;
            set { SetProperty(This.AccessToken, value, () => This.AccessToken = value); }
        }

        public string ItemSymbol { get; set; } = Application.Current.Resources["Folder"] as string;

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