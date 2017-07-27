using Touch.ViewModels;
using System;
using Touch.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Touch.Views.UserControls
{
    public sealed partial class StreetGalleryControl : NavigableUserControl
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

            GalleryBackButtonControl.OnBackButtonClicked += () =>
            {
                Shown = false;
                OnBackButtonClicked?.Invoke();
            };
        }
    }
}