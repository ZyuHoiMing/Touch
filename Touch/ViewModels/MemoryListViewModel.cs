using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Touch.Models;

namespace Touch.ViewModels
{
    /// <summary>
    ///     回忆列表ViewModel
    /// </summary>
    public class MemoryListViewModel : NotificationBase
    {
        /// <summary>
        ///     与data交互的model
        /// </summary>
        private readonly MemoryList _memoryList;

        /// <summary>
        ///     与view交互的list
        /// </summary>
        private ObservableCollection<MyMemoryViewModel> _myMemoryVms;

        public MemoryListViewModel()
        {
            _memoryList = new MemoryList();
            _myMemoryVms = new ObservableCollection<MyMemoryViewModel>();
            // 从数据库中加载数据，加到与list交互的VM中
            foreach (var myMemory in _memoryList.List)
            {
                var myMemoryVm = new MyMemoryViewModel(myMemory);
                _myMemoryVms.Add(myMemoryVm);
            }
            // 最后一个添加选项
            _myMemoryVms.Add(new MyMemoryViewModel
            {
                IsAddVisibility = Visibility.Collapsed
            });
        }

        /// <summary>
        ///     与view交互的list
        /// </summary>
        public ObservableCollection<MyMemoryViewModel> MyMemoryVms
        {
            get { return _myMemoryVms; }
            set { SetProperty(ref _myMemoryVms, value); }
        }

        /// <summary>
        ///     增加一条回忆
        /// </summary>
        public void Add(MyMemoryViewModel myMemoryVm)
        {
            if (MyMemoryVms.Contains(myMemoryVm))
                return;
            MyMemoryVms.Insert(MyMemoryVms.Count - 1, myMemoryVm);
            _memoryList.Add(myMemoryVm);
        }

        /// <summary>
        ///     删除一个文件夹路径记录
        /// </summary>
        public void Delete(MyMemoryViewModel myMemoryVm)
        {
            if (!MyMemoryVms.Contains(myMemoryVm))
                return;
            MyMemoryVms.Remove(myMemoryVm);
            _memoryList.Delete(myMemoryVm);
        }
    }
}