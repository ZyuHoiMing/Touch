using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Touch.ViewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Touch.Views.UserControls
{
    public sealed partial class FolderSourceControl : UserControl
    {
        private FolderListViewModel _folderListViewModel;

        public FolderSourceControl()
        {
            InitializeComponent();
        }

        private async void FolderSourceControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            _folderListViewModel = await FolderListViewModel.GetInstanceAsync();
            SourceList.ItemsSource = _folderListViewModel.FolderViewModels;
            SourceList.DataContext = _folderListViewModel;
        }
    }
}