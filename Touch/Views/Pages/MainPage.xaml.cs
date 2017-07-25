using Windows.ApplicationModel.Core;
using Windows.UI.Core;
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
            
            //// 汉堡菜单按钮的点击事件
            //HambugerMenuButton.Click += (sender, args) =>
            //{
            //    MainPageSplitView.IsPaneOpen = !MainPageSplitView.IsPaneOpen;
            //};
            //// item list点击事件
            //HambugerMenuPrimaryList.ItemClick += HambugerMenuList_ItemClick;
            //HambugerMenuSecondaryList.ItemClick += HambugerMenuList_ItemClick;
            //// 左上角返回按钮的点击事件
            //SystemNavigationManager.GetForCurrentView().BackRequested += (sender, e) =>
            //{
            //    if (!MainPageFrame.CanGoBack) return;
            //    MainPageFrame.GoBack();
            //};
            //// Frame nav后做的事
            //MainPageFrame.Navigated += (sender, e) =>
            //{
            //    // 左上角返回按钮
            //    // Each time a navigation event occurs, update the Back button's visibility
            //    // Show UI in title bar if opted-in and in-app backstack is not empty.
            //    // Remove the UI from the title bar if in-app back stack is empty.
            //    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = MainPageFrame.CanGoBack
            //        ? AppViewBackButtonVisibility.Visible
            //        : AppViewBackButtonVisibility.Collapsed;
            //};
            //// 默认跳转到主页
            //MainPageFrame.Navigate(typeof(GalleryPage));
        }

        /// <summary>
        ///     item list点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HambugerMenuList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var clickedPage = (e.ClickedItem as HamburgerMenuItemViewModel)?.Page;
            //if (MainPageFrame.SourcePageType != clickedPage)
            //    MainPageFrame.Navigate(clickedPage);
        }
    }
}