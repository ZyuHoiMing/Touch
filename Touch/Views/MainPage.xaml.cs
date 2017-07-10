using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Touch.Models;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Touch.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<HambugerMenuListItem> hambugerMenuPrimaryListItems;
        private ObservableCollection<HambugerMenuListItem> hambugerMenuSecondaryListItems;

        public MainPage()
        {
            this.InitializeComponent();

            hambugerMenuPrimaryListItems = new ObservableCollection<HambugerMenuListItem>();
            hambugerMenuSecondaryListItems = new ObservableCollection<HambugerMenuListItem>();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HambugerMenuListItem item = new HambugerMenuListItem()
            {
                ItemName = Application.Current.Resources["gallery"] as string,
                ItemSymbol = Symbol.Home,
                ItemPage = typeof(GalleryPage)
            };
            hambugerMenuPrimaryListItems.Add(item);
            item = new HambugerMenuListItem()
            {
                ItemName = Application.Current.Resources["3d_gallery"] as string,
                ItemSymbol = Symbol.Home,
                ItemPage = typeof(_3DGalleryPage)
            };
            hambugerMenuPrimaryListItems.Add(item);

            HambugerMenuListItem item2 = new HambugerMenuListItem()
            {
                ItemName = Application.Current.Resources["setting"] as string,
                ItemSymbol = Symbol.Setting,
                ItemPage = typeof(_3DGalleryPage)
            };
            hambugerMenuSecondaryListItems.Add(item2);
        }

        private void HambugerMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageSplitView.IsPaneOpen = !MainPageSplitView.IsPaneOpen;
        }

        private void HambugerMenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPage = ((sender as ListView).SelectedItem as HambugerMenuListItem).ItemPage;
            if (MainPageFrame.SourcePageType != selectedPage)
            {
                MainPageFrame.Navigate(selectedPage);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HambugerMenuPrimaryList.SelectedIndex = 0;
        }
    }
}
