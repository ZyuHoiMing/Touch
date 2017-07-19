namespace Touch.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     文件夹路径
    /// </summary>
    public class MyFolder
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        ///     文件夹路径
        /// </summary>
        public string FolderPath { get; set; }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as MyFolder;
            return o != null && o.FolderPath == FolderPath;
        }
    }
}