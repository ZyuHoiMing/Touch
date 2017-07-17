using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Touch.Models
{
    internal class ImagePath
    {
        public BitmapImage File { get; internal set; }
        public string Id { get; set; }
        public string Path { get; set; }
        public StorageFile Storage { get; internal set; }
    }
}