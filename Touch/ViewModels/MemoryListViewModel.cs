using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        ///     删除一个回忆
        /// </summary>
        public void Delete(MyMemoryViewModel myMemoryVm)
        {
            if (!MyMemoryVms.Contains(myMemoryVm))
                return;
            MyMemoryVms.Remove(myMemoryVm);
            _memoryList.Delete(myMemoryVm);
        }

        public async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as MyMemoryViewModel;
            if (item == null)
                return;
            if (item.IsAddVisibility == Visibility.Visible)
            {
                // 如果是添加新回忆的按钮
                //var dialog = new AddMemoryDialog(this);
                //await dialog.ShowAsync();
            }
            else
            {
                // 进入街景界面
                Debug.WriteLine("进入街景界面");
            }
        }
    }
}