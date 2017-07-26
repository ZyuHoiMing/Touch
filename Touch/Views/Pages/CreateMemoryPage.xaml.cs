using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class CreateMemoryPage : Page
    {
        public CreateMemoryPage()
        {
            InitializeComponent();
            TitleBarControl.SetBackButtonVisibility(Visibility.Visible);
            // 设置为多选
            GalleryGridViewControl.SetGridViewMultipleSelection();
            // 标题box
            TitleBox.Text = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            // 完成button
            DoneButton.Click += (sender, args) => { };
            // 取消button
            CancelButton.Click += (sender, args) =>
            {
                var rootFrame = Window.Current.Content as Frame;
                if (rootFrame == null)
                    return;
                rootFrame.GoBack();
            };
        }
    }
}