using System.Collections.Generic;

namespace Touch.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     回忆
    /// </summary>
    public class MyMemory
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        ///     回忆编号
        /// </summary>
        public int KeyNo { get; set; }

        /// <summary>
        ///     回忆里的名字
        /// </summary>
        public string MemoryName { get; set; } = "";

        /// <summary>
        ///     回忆里包含的图片
        /// </summary>
        public List<ImageModel> Images { get; set; }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as MyMemory;
            return o != null && o.KeyNo == KeyNo;
        }
    }
}