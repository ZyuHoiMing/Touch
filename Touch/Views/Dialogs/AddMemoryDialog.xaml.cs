using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Dialogs
{
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class AddMemoryDialog : ContentDialog
    {
        // 图片个数
        private int _imageNum;

        // 图片list
        private IReadOnlyList<StorageFile> _images;

        public AddMemoryDialog()
        {
            InitializeComponent();

            // 选择图片
            SelectImageButton.Click += async (sender, args) =>
            {
                // 如果是添加新文件夹的按钮
                var picker = new FileOpenPicker();
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".png");
                _images = await picker.PickMultipleFilesAsync();
                _imageNum = _images.Count;
                Debug.WriteLine(_imageNum);
            };
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}