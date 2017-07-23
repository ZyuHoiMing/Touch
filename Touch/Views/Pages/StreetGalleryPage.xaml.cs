using Windows.UI.Xaml.Controls;
using Touch.ViewModels;

// ReSharper disable InconsistentNaming

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class StreetGalleryPage : Page
    {
        private StreetImageListViewModel _streetImageListVm;

        public StreetGalleryPage()
        {
            InitializeComponent();

            _streetImageListVm = new StreetImageListViewModel();
            // 左右button的点击事件
            LeftButton.Click += (sender, args) => { _streetImageListVm.SelectedIndex--; };
            RightButton.Click += (sender, args) => { _streetImageListVm.SelectedIndex++; };
        }
    }
}