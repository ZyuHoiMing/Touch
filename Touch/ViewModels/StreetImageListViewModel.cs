using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Touch.ViewModels
{
    /// <summary>
    ///     街景图片的VM
    /// </summary>
    public class StreetImageListViewModel : NotificationBase
    {
        private int _selectedIndex;

        /// <summary>
        ///     与view交互的list
        /// </summary>
        public ObservableCollection<ImageViewModel> ImageViewModels;

        public StreetImageListViewModel()
        {
            ImageViewModels = new ObservableCollection<ImageViewModel>
            {
                null
            };
            _selectedIndex = 0;
        }

        /// <summary>
        ///     当前选中的index
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value < 0 || value >= ImageViewModels.Count)
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
            get { return _selectedIndex >= 0 ? ImageViewModels[_selectedIndex] : null; }
        }

        /// <summary>
        ///     添加街景图片
        /// </summary>
        /// <param name="imageViewModels"></param>
        public void AddImages(List<ImageViewModel> imageViewModels)
        {
            ImageViewModels.Clear();
            foreach (var imageViewModel in imageViewModels)
                ImageViewModels.Add(imageViewModel);
            SelectedIndex = 0;
        }
    }
}