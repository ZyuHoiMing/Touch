using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Touch.Models
{
    class ImagePath
    {
        public BitmapImage File { get; internal set; }
        public string Id { get; set; }
        public string Path { get; set; }
        public StorageFile Storage { get; internal set; }
    }
}
