using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Touch.Models;
using Touch.Views.Pages;

namespace Touch.ViewModels
{
    /// <summary>
    ///     回忆列表ViewModel
    /// </summary>
    public class MemoryListViewModel : NotificationBase
    {
        private static MemoryListViewModel _uniqueInstance;
        private static readonly object Locker = new object();

        private MemoryList _memoryList;

        /// <summary>
        ///     回忆列表
        /// </summary>
        public ObservableCollection<MemoryViewModel> MemoryViewModels;

        private MemoryListViewModel()
        {
            MemoryViewModels = new ObservableCollection<MemoryViewModel>();
        }

        /// <summary>
        ///     最新key号
        /// </summary>
        public int LastKeyNo => _memoryList.LastKeyNo;

        /// <summary>
        ///     删除点击操作
        /// </summary>
        public ICommand DeleteCommand
        {
            get { return new CommandHandler(memoryViewModel => Delete(memoryViewModel as MemoryViewModel)); }
        }

        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<MemoryListViewModel> GetInstanceAsync()
        {
            if (_uniqueInstance != null)
                return _uniqueInstance;
            lock (Locker)
            {
                // 如果类的实例不存在则创建，否则直接返回
                if (_uniqueInstance == null)
                    // ReSharper disable once PossibleMultipleWriteAccessInDoubleCheckLocking
                    _uniqueInstance = new MemoryListViewModel();
            }
            _uniqueInstance._memoryList = await MemoryList.GetInstanceAsync();
            foreach (var memoryModel in _uniqueInstance._memoryList.MemoryModels)
            {
                var memoryViewModel = await MemoryViewModel.GetInstanceAsync(memoryModel);
                _uniqueInstance.MemoryViewModels.Add(memoryViewModel);
            }
            return _uniqueInstance;
        }

        /// <summary>
        ///     增加一条回忆
        /// </summary>
        public void Add(MemoryViewModel memoryViewModel)
        {
            if (MemoryViewModels.Contains(memoryViewModel))
                return;
            _memoryList.Add(memoryViewModel);
            MemoryViewModels.Add(memoryViewModel);
        }

        /// <summary>
        ///     删除一个回忆
        /// </summary>
        public void Delete(MemoryViewModel memoryViewModel)
        {
            if (!MemoryViewModels.Contains(memoryViewModel))
                return;
            _memoryList.Delete(memoryViewModel);
            MemoryViewModels.Remove(memoryViewModel);
        }

        /// <summary>
        ///     点击回忆列表的item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as MemoryViewModel;
            if (item == null)
                return;
            // 进入街景界面
            var rootFrame = Window.Current.Content as Frame;
            rootFrame?.Navigate(typeof(StreetViewPage), item.ImageViewModels);
            Window.Current.Content = rootFrame;
            Debug.WriteLine("进入街景界面");
        }
    }
}