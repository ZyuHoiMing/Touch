using System;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
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
        private FolderListViewModel _folderListVm;

        public SettingPage()
        {
            InitializeComponent();

            _folderListVm = new FolderListViewModel();
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
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add("*");
            var folder = await folderPicker.PickSingleFolderAsync();
            if (folder == null)
                return;
            _folderListVm.Add(new MyFolderViewModel {FolderPath = folder.Path});
        }
    }
}