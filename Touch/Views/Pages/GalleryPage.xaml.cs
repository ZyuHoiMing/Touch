using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Touch.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class GalleryPage : Page
    {
        //private readonly AllImageListViewModel _allImageListVm;

        public GalleryPage()
        {
            InitializeComponent();

            //LoadingControl.IsLoading = true;
            //_allImageListVm = new AllImageListViewModel();
            //RefreshButton.Click += async (sender, args) => await RefreshAsync();
        }

        //protected override async void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    await RefreshAsync();
        //}

        ///// <summary>
        /////     刷新图库图片
        ///// </summary>
        ///// <returns></returns>
        //private async Task RefreshAsync()
        //{
        //    await _allImageListVm.RefreshAsync();
        //    Cvs.Source = _allImageListVm.ImageMonthGroups;
        //    LoadingControl.IsLoading = false;
        //}
    }
}