using Touch.Data;

namespace Touch.ViewModels
{
    public class MyFolderViewModel : NotificationBase<MyFolder>
    {
        public MyFolderViewModel(MyFolder myFolder = null) : base(myFolder)
        {
        }

        public string FolderPath
        {
            get => This.FolderPath;
            set { SetProperty(This.FolderPath, value, () => This.FolderPath = value); }
        }
    }
}