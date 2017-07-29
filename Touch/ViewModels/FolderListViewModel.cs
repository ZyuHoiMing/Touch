using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
    /// <summary>
    ///     文件夹列表ViewModel
    /// </summary>
    public class FolderListViewModel : NotificationBase
    {
        /// <summary>
        ///     与data交互的model，文件夹路径list
        /// </summary>
        private FolderList _folderList;

        /// <summary>
        ///     与view交互的list
        /// </summary>
        public ObservableCollection<FolderViewModel> FolderViewModels;

        private FolderListViewModel()
        {
            FolderViewModels = new ObservableCollection<FolderViewModel>();
        }

        /// <summary>
        ///     删除点击操作
        /// </summary>
        public ICommand DeleteCommand
        {
            get { return new CommandHandler(folderViewModel => Delete(folderViewModel as FolderViewModel)); }
        }

        /// <summary>
        ///     异步获取实例
        /// </summary>
        /// <returns></returns>
        public static async Task<FolderListViewModel> GetInstanceAsync()
        {
            var folderListViewModel = new FolderListViewModel
            {
                _folderList = await FolderList.GetInstanceAsync()
            };
            // 从数据库中加载数据，加到与list交互的VM中
            foreach (var folder in folderListViewModel._folderList.FolderModels)
            {
                var folderViewModel = new FolderViewModel(folder);
                folderListViewModel.FolderViewModels.Add(folderViewModel);
            }
            // 最后一个添加文件夹选项
            folderListViewModel.FolderViewModels.Add(new FolderViewModel
            {
                FolderPath = new ResourceLoader().GetString("AddFolder"),
                ItemSymbol = Application.Current.Resources["Add"] as string,
                IsDeleteVisibility = Visibility.Collapsed
            });
            return folderListViewModel;
        }

        /// <summary>
        ///     增加一个文件夹路径记录
        /// </summary>
        public void Add(FolderViewModel folderViewModel)
        {
            if (FolderViewModels.Contains(folderViewModel))
                return;
            _folderList.Add(folderViewModel);
            FolderViewModels.Insert(FolderViewModels.Count - 1, folderViewModel);
        }

        /// <summary>
        ///     删除一个文件夹路径记录
        /// </summary>
        public void Delete(FolderViewModel folderViewModel)
        {
            if (!FolderViewModels.Contains(folderViewModel))
                return;
            _folderList.Delete(folderViewModel);
            FolderViewModels.Remove(folderViewModel);
        }

        /// <summary>
        ///     item点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as FolderViewModel;
            if (item == null)
                return;
            if (item.IsDeleteVisibility == Visibility.Collapsed)
            {
                // 如果是添加新文件夹的按钮
                var folderPicker = new FolderPicker();
                folderPicker.FileTypeFilter.Add("*");
                folderPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                var folder = await folderPicker.PickSingleFolderAsync();
                if (folder == null)
                    return;
                var accessToken = StorageApplicationPermissions.FutureAccessList.Add(folder);
                item = new FolderViewModel
                {
                    FolderPath = folder.Path,
                    AccessToken = accessToken
                };
                Add(item);
            }
            else
            {
                var folder = await item.GetFolder();
                await Launcher.LaunchFolderAsync(folder);
            }
        }
    }
}