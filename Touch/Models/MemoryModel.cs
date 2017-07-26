using System.Collections.Generic;

namespace Touch.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     回忆
    /// </summary>
    public class MemoryModel
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        ///     回忆编号
        /// </summary>
        public int KeyNo { get; set; }

        /// <summary>
        ///     回忆名字
        /// </summary>
        public string MemoryName { get; set; }

        /// <summary>
        ///     回忆里的图片
        /// </summary>
        public List<ImageModel> ImageModels { get; set; }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as MemoryModel;
            return o != null && o.KeyNo == KeyNo;
        }
    }
}