using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Touch.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class SettingPage : Page
    {
        private readonly FolderListViewModel _folderListVm;

        public SettingPage()
        {
            InitializeComponent();
            _folderListVm = new FolderListViewModel();
            TitleBarControl.SetBackButtonVisibility(Visibility.Visible);
            var package = Package.Current;
            var name = package.DisplayName;
            var version = package.Id.Version;
            AppInfoText.Text = name + " " + $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}