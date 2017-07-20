using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Touch.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        private readonly FolderListViewModel _folderListVm;

        public SettingPage()
        {
            InitializeComponent();

            _folderListVm = new FolderListViewModel();

            var package = Package.Current;
            var name = package.DisplayName;
            var version = package.Id.Version;
            AppInfoText.Text = name + " " + $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var folderPicker = new FolderPicker {SuggestedStartLocation = PickerLocationId.Desktop};

        //    folderPicker.FileTypeFilter.Add(".png");
        //    folderPicker.FileTypeFilter.Add(".jpg");
        //    var folder = await folderPicker.PickSingleFolderAsync();
        //    var files = await folder.GetFilesAsync();
        //    foreach (var file in files)
        //    {
        //        var bitmap = new BitmapImage();
        //        using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
        //        {
        //            bitmap.SetSource(stream);
        //        }
        //        var img = new Image {Source = bitmap};
        //        ImgView.Items?.Add(img);
        //    }
        //}
    }
}