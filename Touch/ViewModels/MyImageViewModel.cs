using System;
using Windows.UI.Xaml.Media.Imaging;
using Touch.Models;

namespace Touch.ViewModels
{
    public class MyImageViewModel : NotificationBase<MyImage>
    {
        public MyImageViewModel(MyImage myImage = null) : base(myImage)
        {
        }

        public string ImagePath
        {
            get => This.ImagePath;
            set { SetProperty(This.ImagePath, value, () => This.ImagePath = value); }
        }

        public BitmapImage Bitmap { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public DateTime DateTaken { get; set; }
    }
}