using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        public FolderListViewModel()
        {
            _folderList = new FolderList();
            // 从数据库中加载数据，加到与list交互的VM中
            foreach (var folder in _folderList.List)
            {
                var myFolderVm = new MyFolderViewModel(folder)
                {
                    FolderPath = folder.FolderPath,
                    AccessToken = folder.AccessToken
                };
                _myFolderVms.Add(myFolderVm);
            }
            _myFolderVms.Add(new MyFolderViewModel
            {
                FolderPath = new ResourceLoader().GetString("AddFolder"),
                ItemSymbol = Application.Current.Resources["Add"] as string,
                IsDeleteVisibility = Visibility.Collapsed
            });
        }

        public ObservableCollection<MyFolderViewModel> MyFolderVms
        {
            get => _myFolderVms;
            set => SetProperty(ref _myFolderVms, value);
        }

        /// <summary>
        ///     删除点击操作
        /// </summary>
        public ICommand DeleteCommand
        {
            get { return new CommandHandler(myFolderVm => Delete(myFolderVm as MyFolderViewModel)); }
        }

        /// <summary>
        ///     增加一个文件夹路径记录
        /// </summary>
        public void Add(MyFolderViewModel myFolderVm)
        {
            if (MyFolderVms.Contains(myFolderVm)) return;
            MyFolderVms.Insert(MyFolderVms.Count - 1, myFolderVm);
            _folderList.Add(myFolderVm);
        }

        /// <summary>
        ///     删除一个文件夹路径记录
        /// </summary>
        public void Delete(MyFolderViewModel myFolderVm)
        {
            if (!MyFolderVms.Contains(myFolderVm))
                return;
            MyFolderVms.Remove(myFolderVm);
            _folderList.Delete(myFolderVm);
        }

        public async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as MyFolderViewModel;
            if (item == null)
                return;
            if (item.IsDeleteVisibility == Visibility.Collapsed)
            {
                // 如果是添加新文件夹的按钮
                var folderPicker = new FolderPicker();
                folderPicker.FileTypeFilter.Add("*");
                var folder = await folderPicker.PickSingleFolderAsync();
                if (folder == null)
                    return;
                var accessToken = StorageApplicationPermissions.FutureAccessList.Add(folder);
                item = new MyFolderViewModel
                {
                    FolderPath = folder.Path,
                    AccessToken = accessToken
                };
                Add(item);
            }
            else
            {
                var folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(item.AccessToken);
                await Launcher.LaunchFolderAsync(folder);
            }
        }
    }
}