using System.Collections.Generic;

namespace Touch.ViewModels
{
    /// <summary>
    ///     街景图片的VM
    /// </summary>
    public class StreetImageListViewModel : NotificationBase
    {
        private List<ImageViewModel> _imageViewModels;

        private int _selectedIndex;

        public StreetImageListViewModel(List<ImageViewModel> imageViewModels)
        {
            _imageViewModels = imageViewModels;
        }

        /// <summary>
        ///     与view交互的list
        /// </summary>
        public List<ImageViewModel> ImageViewModels
        {
            get { return _imageViewModels; }
            set { SetProperty(ref _imageViewModels, value); }
        }

        /// <summary>
        ///     当前选中的index
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value < 0 || value >= _imageViewModels.Count)
                    return;
                if (SetProperty(ref _selectedIndex, value))
                    RaisePropertyChanged(nameof(SelectedImage));
            }
        }

        /// <summary>
        ///     选中的imageVM
        /// </summary>
        public ImageViewModel SelectedImage
        {
            get { return _selectedIndex >= 0 ? _imageViewModels[_selectedIndex] : null; }
        }
    }
}