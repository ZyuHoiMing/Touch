using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Touch.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ObservableCollection<HambugerMenuListItem> _hambugerMenuPrimaryListItems;
        private readonly ObservableCollection<HambugerMenuListItem> _hambugerMenuSecondaryListItems;

        public MainPage()
        {
            InitializeComponent();

            _hambugerMenuPrimaryListItems = new ObservableCollection<HambugerMenuListItem>();
            _hambugerMenuSecondaryListItems = new ObservableCollection<HambugerMenuListItem>();
            // 显示title bar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var item = new HambugerMenuListItem
            {
                ItemName = new ResourceLoader().GetString("Gallery"),
                ItemSymbol = Symbol.Home,
                ItemPage = typeof(GalleryPage)
            };
            _hambugerMenuPrimaryListItems.Add(item);
            item = new HambugerMenuListItem
            {
                ItemName = "测试",
                ItemSymbol = Symbol.Home,
                ItemPage = typeof(StreetGalleryPage)
            };
            _hambugerMenuPrimaryListItems.Add(item);
            item = new HambugerMenuListItem
            {
                ItemName = new ResourceLoader().GetString("Album"),
                ItemSymbol = Symbol.Home,
                ItemPage = typeof(AlbumPage)
            };
            _hambugerMenuPrimaryListItems.Add(item);

            var item2 = new HambugerMenuListItem
            {
                ItemName = new ResourceLoader().GetString("Setting"),
                ItemSymbol = Symbol.Setting,
                ItemPage = typeof(SettingPage)
            };
            _hambugerMenuSecondaryListItems.Add(item2);
        }

        private void HambugerMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageSplitView.IsPaneOpen = !MainPageSplitView.IsPaneOpen;
        }

        private void HambugeMenurPrimaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPage = ((sender as ListView)?.SelectedItem as HambugerMenuListItem)?.ItemPage;
            if (MainPageFrame.SourcePageType != selectedPage)
                MainPageFrame.Navigate(selectedPage);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HambugerMenuPrimaryList.SelectedIndex = 0;
        }

        private void HambugerMenuSecondaryList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedPage = (e.ClickedItem as HambugerMenuListItem)?.ItemPage;
            MainPageFrame.Navigate(clickedPage);
        }
    }
}