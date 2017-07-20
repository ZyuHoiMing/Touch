using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;
using Touch.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            // 显示title bar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = false;

            HambugerMenuButton.Click += (sender, args) =>
            {
                MainPageSplitView.IsPaneOpen = !MainPageSplitView.IsPaneOpen;
            };

            HambugerMenuPrimaryList.ItemClick += HambugerMenuList_ItemClick;
            HambugerMenuSecondaryList.ItemClick += HambugerMenuList_ItemClick;

            MainPageFrame.Navigate(typeof(GalleryPage));
        }

        private void HambugerMenuList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedPage = (e.ClickedItem as HamburgerMenuItemViewModel)?.Page;
            if (MainPageFrame.SourcePageType != clickedPage)
                MainPageFrame.Navigate(clickedPage);
        }
    }
}