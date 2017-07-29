using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Touch.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class MemoryDetailPage : Page
    {
        private MemoryListViewModel _memoryListViewModel;
        private MemoryViewModel _memoryViewModel;

        public MemoryDetailPage()
        {
            InitializeComponent();
            InitVisual();
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

        private void InitVisual()
        {
            // TODO 14393 15063
            //var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            //// 动画结束后要盖一层图片，不然会闪
            //var coverImageCoverOpacityAnimation = compositor.CreateScalarKeyFrameAnimation();
            //coverImageCoverOpacityAnimation.DelayTime = TimeSpan.FromSeconds(0.5);
            //coverImageCoverOpacityAnimation.Duration = TimeSpan.FromMilliseconds(1);
            //coverImageCoverOpacityAnimation.Target = "Opacity";
            //coverImageCoverOpacityAnimation.InsertKeyFrame(0, 0);
            //coverImageCoverOpacityAnimation.InsertKeyFrame(1, 1);
            //ElementCompositionPreview.SetIsTranslationEnabled(CoverImageCover, true);
            //ElementCompositionPreview.GetElementVisual(CoverImageCover);
            //ElementCompositionPreview.SetImplicitShowAnimation(CoverImageCover, coverImageCoverOpacityAnimation);

            //// Add a translation animation that will play when this element is shown
            //var topBorderOpacityAnimation = compositor.CreateScalarKeyFrameAnimation();
            //topBorderOpacityAnimation.DelayTime = TimeSpan.FromSeconds(0.5);
            //topBorderOpacityAnimation.Duration = TimeSpan.FromSeconds(1);
            //topBorderOpacityAnimation.Target = "Opacity";
            //topBorderOpacityAnimation.InsertKeyFrame(0, 0);
            //topBorderOpacityAnimation.InsertKeyFrame(1, 1);
            //ElementCompositionPreview.SetIsTranslationEnabled(TopBorder, true);
            //ElementCompositionPreview.GetElementVisual(TopBorder);
            //ElementCompositionPreview.SetImplicitShowAnimation(TopBorder, topBorderOpacityAnimation);

            //// Add an opacity and translation animation that will play when this element is shown
            //var mainContentTranslationAnimation = compositor.CreateScalarKeyFrameAnimation();
            //mainContentTranslationAnimation.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
            //mainContentTranslationAnimation.DelayTime = TimeSpan.FromSeconds(0.2);
            //mainContentTranslationAnimation.Duration = TimeSpan.FromSeconds(0.45);
            //mainContentTranslationAnimation.Target = "Translation.Y";
            //mainContentTranslationAnimation.InsertKeyFrame(0, 50.0f);
            //mainContentTranslationAnimation.InsertKeyFrame(1, 0);

            //var mainContentOpacityAnimation = compositor.CreateScalarKeyFrameAnimation();
            //mainContentOpacityAnimation.Duration = TimeSpan.FromSeconds(0.4);
            //mainContentOpacityAnimation.Target = "Opacity";
            //mainContentOpacityAnimation.InsertKeyFrame(0, 0);
            //mainContentOpacityAnimation.InsertKeyFrame(0.25f, 0);
            //mainContentOpacityAnimation.InsertKeyFrame(1, 1);

            //var mainContentShowAnimations = compositor.CreateAnimationGroup();
            //mainContentShowAnimations.Add(mainContentTranslationAnimation);
            //mainContentShowAnimations.Add(mainContentOpacityAnimation);

            //ElementCompositionPreview.SetIsTranslationEnabled(MainContent, true);
            //ElementCompositionPreview.GetElementVisual(MainContent);
            //ElementCompositionPreview.SetImplicitShowAnimation(MainContent, mainContentShowAnimations);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // connected animation
            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("CoverImage");
            animation?.TryStart(CoverImage);

            var memoryDetailParameters = e.Parameter as MemoryDetailParameters;
            _memoryViewModel = memoryDetailParameters?.MemoryViewModel;
            _memoryListViewModel = memoryDetailParameters?.MemoryListViewModel;
            PhotoGridView.MemoryViewModel = _memoryViewModel;
        }
    }
}