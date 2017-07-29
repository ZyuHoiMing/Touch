using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Touch.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class MemoryDetailPage : Page
    {
        private MemoryViewModel _memoryViewModel;
        private MemoryListViewModel _memoryListViewModel;

        public MemoryDetailPage()
        {
            InitializeComponent();
            TitleBarControl.SetBackButtonVisibility(Visibility.Visible);
            ShowButton.Click += (sender, args) =>
            {
                // 进入街景界面
                var rootFrame = Window.Current.Content as Frame;
                rootFrame?.Navigate(typeof(StreetViewPage), _memoryViewModel.ImageViewModels);
                Window.Current.Content = rootFrame;
                Debug.WriteLine("进入街景界面");
            };
            DeleteButton.Click += (sender, args) =>
            {
                var rootFrame = Window.Current.Content as Frame;
                _memoryListViewModel.Delete(_memoryViewModel);
                rootFrame?.GoBack();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var memoryDetailParameters = e.Parameter as MemoryDetailParameters;
            _memoryViewModel = memoryDetailParameters?.MemoryViewModel;
            _memoryListViewModel = memoryDetailParameters?.MemoryListViewModel;
            PhotoGridView.MemoryViewModel = _memoryViewModel;
        }
    }
}
