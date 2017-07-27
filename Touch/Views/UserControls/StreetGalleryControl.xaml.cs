using Windows.UI.Xaml.Controls;
using Touch.ViewModels;
using System;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Touch.Views.UserControls
{
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class StreetGalleryControl : UserControl
    {
        public StreetImageListViewModel StreetImageListViewModel;
        public event Action OnBackButtonClicked;

        public StreetGalleryControl()
        {
            InitializeComponent();
            StreetImageListViewModel = new StreetImageListViewModel();
            // 左右button的点击事件
            LeftButton.Click += (sender, args) => { StreetImageListViewModel.SelectedIndex--; };
            RightButton.Click += (sender, args) => { StreetImageListViewModel.SelectedIndex++; };

            BackButton.Click += (sender, args) =>
            {
                OnBackButtonClicked?.Invoke();
            };
        }
    }
}