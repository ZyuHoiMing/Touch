using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Touch.ViewModels;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Dialogs
{
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class AddMemoryDialog : ContentDialog
    {
        // 图片list
        private IReadOnlyList<StorageFile> _images;
        // 图片个数
        private int _imageNum;

        public AddMemoryDialog()
        {
            this.InitializeComponent();

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
