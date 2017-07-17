using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Touch.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Touch.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //打开文件选择器
            String folderStr = "photo";
            FolderPicker pick = new FolderPicker();
            pick.FileTypeFilter.Add(".png");
            pick.FileTypeFilter.Add(".jpg");
            pick.FileTypeFilter.Add(".bmp");
            IAsyncOperation<StorageFolder> folderTask = pick.PickSingleFolderAsync();

            StorageFolder folder = await folderTask;

            //var folder = await pick.PickSingleFolderAsync();
            StorageFolder Folder = null;
            string Address;
            string Token = "";
            if (folder != null)
            {
                Folder = folder;
                Address = folder.Path;
                Token = StorageApplicationPermissions.FutureAccessList.Add(folder);
            }
            //StorageApplicationPermissions.FutureAccessList.GetFolderAsync(Token);
            Debug.WriteLine(Token);
            //获取本地文件夹
            StorageFolder folderLocal = ApplicationData.Current.LocalFolder;

            //创建一个文件夹account
            try
            {
                folderLocal = await folderLocal.GetFolderAsync(folderStr);
            }
            catch (FileNotFoundException)
            {
                folderLocal = await folderLocal.CreateFolderAsync(folderStr);
            }

            StorageFile file = await folderLocal.CreateFileAsync(folderStr + ".json", CreationCollisionOption.ReplaceExisting);

            //保存选择的文件夹Token
            var json = JsonSerializer.Create();
            Debug.WriteLine(file.Name + file.Path);
            ImagePath imagePath = new ImagePath { Id = DateTime.Now.ToString("yyMMddHHmmss"), Path = Token };
            var resault = JsonConvert.SerializeObject(imagePath);
            //string imageJson = resault.Stringify();
            if (file != null)
            {
                try
                {
                    using (StorageStreamTransaction transaction = await file.OpenTransactedWriteAsync())
                    {
                        using (DataWriter dataWriter = new DataWriter(transaction.Stream))
                        {
                            dataWriter.WriteInt32(Encoding.UTF8.GetByteCount(resault));
                            dataWriter.WriteString(resault);
                            transaction.Stream.Size = await dataWriter.StoreAsync();
                            await transaction.CommitAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*string folderStr = "photo";
            StorageFile fileLocal = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata:///local/" + folderStr + ".json"));
            if (fileLocal != null)
            {
                try
                {
                    //读取本地文件内容，并且反序列化
                    using (IRandomAccessStream readStream = await fileLocal.OpenAsync(FileAccessMode.Read))
                    {
                        using (DataReader dataReader = new DataReader(readStream))
                        {
                            UInt64 size = readStream.Size;
                            if (size <= UInt32.MaxValue)
                            {
                                await dataReader.LoadAsync(sizeof(Int32));
                                Int32 stringSize = dataReader.ReadInt32();
                                await dataReader.LoadAsync((UInt32)stringSize);
                                string fileContent = dataReader.ReadString((uint)stringSize);
                                ImagePath imagePath = new ImagePath(fileContent);
                                StorageFolder folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(imagePath.Path);
                                //筛选图片
                                var queryOptions = new Windows.Storage.Search.QueryOptions();
                                queryOptions.FileTypeFilter.Add(".png");
                                queryOptions.FileTypeFilter.Add(".jpg");
                                queryOptions.FileTypeFilter.Add(".bmp");
                                var query = folder.CreateFileQueryWithOptions(queryOptions);
                                var files = await query.GetFilesAsync();

                                ImagePath img;
                                ObservableCollection<ImagePath> imgList = new ObservableCollection<ImagePath>();
                                foreach (var item in files)
                                {
                                    IRandomAccessStream irandom = await item.OpenAsync(FileAccessMode.Read);

                                    //对图像源使用流源
                                    BitmapImage bitmapImage = new BitmapImage();
                                    bitmapImage.DecodePixelWidth = 160;
                                    bitmapImage.DecodePixelHeight = 100;
                                    await bitmapImage.SetSourceAsync(irandom);

                                    img = new ImagePath();
                                    img.Path = item.Path;
                                    img.File = bitmapImage;
                                    img.Storage = item;
                                    imgList.Add(img);
                                }
                                imageView.ItemsSource = imgList;
                            }

                        }
                    }
                }
                catch (Exception exce)
                {
                    await new MessageDialog(exce.ToString()).ShowAsync();
                    throw exce;
                }
            }*/
        }
    }
}
