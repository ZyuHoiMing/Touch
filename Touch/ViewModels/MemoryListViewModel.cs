using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Touch.Models;

namespace Touch.ViewModels
{
    /// <summary>
    ///     回忆列表ViewModel
    /// </summary>
    public class MemoryListViewModel : NotificationBase
    {
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
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<MemoryListViewModel> GetInstanceAsync()
        {
            var memoryListViewModel = new MemoryListViewModel
            {
                _memoryList = await MemoryList.GetInstanceAsync()
            };
            foreach (var memoryModel in memoryListViewModel._memoryList.MemoryModels)
            {
                var memoryViewModel = await MemoryViewModel.GetInstanceAsync(memoryModel);
                memoryListViewModel.MemoryViewModels.Add(memoryViewModel);
            }
            return memoryListViewModel;
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
            Debug.WriteLine("进入街景界面");
        }
    }
}