using Windows.UI.Xaml.Controls;
using Touch.ViewModels;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Touch.Views.Dialogs
{
    // ReSharper disable once RedundantExtendsListEntry
    public sealed partial class AddMemoryDialog : ContentDialog
    {
        private readonly AddMemoryViewModel _addMemoryVm;

        public AddMemoryDialog(MemoryListViewModel memoryListVm)
        {
            InitializeComponent();

            _addMemoryVm = new AddMemoryViewModel(memoryListVm);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            _addMemoryVm.PrimaryButtonClick(MemoryNameBox.Text);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}