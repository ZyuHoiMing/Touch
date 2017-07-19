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

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as MyFolderViewModel;
            return o != null && o.FolderPath == FolderPath;
        }
    }
}