using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Touch.Models;

namespace Touch.ViewModels
{
    public class AllImageListViewModel : NotificationBase
    {
        private AllImageList _allImageList;

        private ObservableCollection<MyImageViewModel> _myImageVms;

        private AllImageListViewModel()
        {
            _myImageVms = new ObservableCollection<MyImageViewModel>();
        }

        public ObservableCollection<MyImageViewModel> MyImageVms
        {
            get => _myImageVms;
            set => SetProperty(ref _myImageVms, value);
        }

        public static async Task<AllImageListViewModel> GetInstanceAsync()
        {
            var allImageListViewModel =
                new AllImageListViewModel {_allImageList = await AllImageList.GetInstanceAsync()};
            foreach (var myImage in allImageListViewModel._allImageList.List)
            {
                var myImageVm = new MyImageViewModel(myImage)
                {
                    ImagePath = myImage.ImagePath,
                    Bitmap = myImage.Bitmap,
                    Latitude = myImage.Latitude,
                    Longitude = myImage.Longitude,
                    DateTaken = myImage.DateTaken
                };
                allImageListViewModel._myImageVms.Add(myImageVm);
            }
            return allImageListViewModel;
        }
    }
}