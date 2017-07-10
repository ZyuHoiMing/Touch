using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Touch.Models;
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
        private ObservableCollection<HambugerMenuListItem> hambugerMenuPrimaryItems;
        private ObservableCollection<HambugerMenuListItem> hambugerMenuSecondaryItems;

        public MainPage()
        {
            this.InitializeComponent();

            hambugerMenuPrimaryItems = new ObservableCollection<HambugerMenuListItem>();
            hambugerMenuSecondaryItems = new ObservableCollection<HambugerMenuListItem>();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HambugerMenuListItem item = new HambugerMenuListItem()
            {
                ItemName = "首页",
                ItemSymbol = Symbol.Home
            };
            hambugerMenuPrimaryItems.Add(item);

            HambugerMenuListItem item2 = new HambugerMenuListItem()
            {
                ItemName = "设置",
                ItemSymbol = Symbol.Setting
            };
            hambugerMenuSecondaryItems.Add(item2);
        }

        private void HambugerMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageSplitView.IsPaneOpen = !MainPageSplitView.IsPaneOpen;
        }

        private void HambugerMenuPrimaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ListView a = (ListView)sender;
            //a.SelectedItem;
            MainPageFrame.Navigate(typeof(_3DGalleryPage));
        }
    }
}
