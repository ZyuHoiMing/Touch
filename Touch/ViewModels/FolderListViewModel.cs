using System.Collections.ObjectModel;
using Touch.Models;

namespace Touch.ViewModels
{
    public class FolderListViewModel : NotificationBase
    {
        /// <summary>
        ///     与data交互的model，文件夹路径list
        /// </summary>
        private readonly FolderList _folderList;

        /// <summary>
        ///     与view交互的list
        /// </summary>
        private ObservableCollection<MyFolderViewModel> _myFolderVms = new ObservableCollection<MyFolderViewModel>();

        private int _selectedIndex;

        public FolderListViewModel()
        {
            _folderList = new FolderList();
            _selectedIndex = -1;
            // 从数据库中加载数据，加到与list交互的VM中
            foreach (var folder in _folderList.List)
            {
                var myFolderVm = new MyFolderViewModel(folder);
                _myFolderVms.Add(myFolderVm);
            }
        }

        public ObservableCollection<MyFolderViewModel> MyFolderVms
        {
            get => _myFolderVms;
            set => SetProperty(ref _myFolderVms, value);
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (SetProperty(ref _selectedIndex, value))
                    RaisePropertyChanged(nameof(SelectedMyFolderVm));
            }
        }

        public MyFolderViewModel SelectedMyFolderVm => _selectedIndex >= 0 ? _myFolderVms[_selectedIndex] : null;

        /// <summary>
        ///     增加一个文件夹路径记录
        /// </summary>
        public void Add(MyFolderViewModel myFolderVm)
        {
            if (_myFolderVms.Contains(myFolderVm)) return;
            _myFolderVms.Add(myFolderVm);
            _folderList.Add(myFolderVm);
        }

        /// <summary>
        ///     删除一个文件夹路径记录
        /// </summary>
        public void Delete()
        {
            if (SelectedIndex == -1) return;
            var myFolderVm = _myFolderVms[SelectedIndex];
            _myFolderVms.RemoveAt(SelectedIndex);
            _folderList.Delete(myFolderVm);
            SelectedIndex = -1;
        }
    }
}