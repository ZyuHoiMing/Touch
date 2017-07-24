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
    public sealed partial class AlbumPage : Page
    {
        /// <summary>
        ///     回忆list VM
        /// </summary>
        private readonly MemoryListViewModel _memoryListVm;

        public AlbumPage()
        {
            InitializeComponent();

            _memoryListVm = new MemoryListViewModel();
        }

        // TODO 考虑下怎么复用
        /// <summary>
        ///     根据窗口大小动态调整 item 长宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = (ItemsWrapGrid) sender;
            if (VisualStateGroup.CurrentState == NarrowVisualState)
                grid.ItemWidth = e.NewSize.Width / 2;
            else if (VisualStateGroup.CurrentState == NormalVisualState)
                grid.ItemWidth = e.NewSize.Width / 3;
            else if (VisualStateGroup.CurrentState == WideVisualState)
                grid.ItemWidth = e.NewSize.Width / 4;
            grid.ItemHeight = grid.ItemWidth * 3 / 4;
        }
    }
}