using System.Collections.Generic;

namespace Touch.Models
{
    /// <summary>
    ///     回忆
    /// </summary>
    public class MyMemory
    {
        /// <summary>
        ///     回忆编号
        /// </summary>
        public int KeyNo { get; set; }

        /// <summary>
        ///     回忆里的名字
        /// </summary>
        public string MemoryName { get; set; }

        /// <summary>
        ///     回忆里包含的图片
        /// </summary>
        public List<MyImage> Images { get; set; }
    }
}