using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Touch.Models;

namespace Touch.ViewModels
{
    /// <summary>
    ///     所有文件夹里的图片的View Model
    /// </summary>
    public class AllImageListViewModel : NotificationBase
    {
        private AllImageList _allImageList;

        private ObservableCollection<MyImageViewModel> _myImageVms;

        /// <summary>
        ///     所有文件夹里的图片的View Model
        /// </summary>
        public AllImageListViewModel()
        {
            _myImageVms = new ObservableCollection<MyImageViewModel>();
        }

        /// <summary>
        ///     与view交互的list
        /// </summary>
        public ObservableCollection<MyImageViewModel> MyImageVms
        {
            get => _myImageVms;
            set => SetProperty(ref _myImageVms, value);
        }

        /// <summary>
        ///     异步刷新list内容
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAsync()
        {
            _allImageList = await AllImageList.GetInstanceAsync();
            foreach (var myImage in _allImageList.List)
            {
                var myImageVm = new MyImageViewModel(myImage);
                _myImageVms.Add(myImageVm);
            }
        }
    }
}