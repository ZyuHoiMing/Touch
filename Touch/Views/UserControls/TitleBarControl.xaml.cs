using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Touch.Views.UserControls
{
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class TitleBarControl : UserControl
    {
        public TitleBarControl()
        {
            InitializeComponent();

            TitleText.Text = Package.Current.DisplayName;
        }
    }
}