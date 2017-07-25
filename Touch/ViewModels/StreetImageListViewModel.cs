using System.Collections.Generic;
using System.Collections.ObjectModel;
using Touch.Models;

namespace Touch.ViewModels
{
    public class StreetImageListViewModel : NotificationBase
    {
        // TODO 数据应该来自data
        //private readonly List<ImageModel> _myImages = new List<ImageModel>
        //{
        //    new ImageModel {ImagePath = "ms-appx:///Assets/pic1.jpg"},
        //    new ImageModel {ImagePath = "ms-appx:///Assets/pic2.jpg"},
        //    new ImageModel {ImagePath = "ms-appx:///Assets/pic3.jpg"},
        //    new ImageModel {ImagePath = "ms-appx:///Assets/pic4.jpg"},
        //    new ImageModel {ImagePath = "ms-appx:///Assets/pic5.jpg"},
        //    new ImageModel {ImagePath = "ms-appx:///Assets/pic6.jpg"}
        //};

        private ObservableCollection<MyImageViewModel> _myImageVms;

        private int _selectedIndex;

        public StreetImageListViewModel()
        {
            //_myImageVms = new ObservableCollection<MyImageViewModel>();
            //// 从数据库中加载数据，加到与list交互的VM中
            //foreach (var myImage in _myImages)
            //{
            //    var myImageVm = new MyImageViewModel(myImage);
            //    _myImageVms.Add(myImageVm);
            //}
            //// 默认选中中间的照片
            //SelectedIndex = _myImageVms.Count / 2;
        }

        /// <summary>
        ///     与view交互的list
        /// </summary>
        public ObservableCollection<MyImageViewModel> MyImageVms
        {
            get { return _myImageVms; }
            set { SetProperty(ref _myImageVms, value); }
        }

        /// <summary>
        ///     当前选中的index
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value < 0 || value >= _myImageVms.Count)
                    return;
                if (SetProperty(ref _selectedIndex, value))
                    RaisePropertyChanged(nameof(SelectedPerson));
            }
        }

        /// <summary>
        ///     选中的imageVM
        /// </summary>
        public MyImageViewModel SelectedPerson
        {
            get { return _selectedIndex >= 0 ? _myImageVms[_selectedIndex] : null; }
        }
    }
}