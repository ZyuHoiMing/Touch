using System.Collections.Generic;

namespace Touch.Models
{
    /// <summary>
    ///     所有文件夹图片的list
    /// </summary>
    public class AllImageList
    {
        /// <summary>
        ///     一个文件夹内的图片list
        /// </summary>
        public List<MyImage> List;

        public AllImageList(IEnumerable<FolderImageList> folderImageLists)
        {
            List = new List<MyImage>();
            foreach (var folderImageList in folderImageLists)
            foreach (var image in folderImageList.List)
                List.Add(image);
        }
    }
}