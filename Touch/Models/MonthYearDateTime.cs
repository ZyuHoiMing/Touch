using System;

namespace Touch.Models
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    ///     只有月和日的DateTime
    /// </summary>
    public class MonthYearDateTime
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public MonthYearDateTime(DateTime dateTime)
        {
            Month = dateTime.Month;
            Year = dateTime.Year;
        }

        /// <summary>
        ///     月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        ///     年
        /// </summary>
        public int Year { get; set; }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var o = obj as MonthYearDateTime;
            return o != null && o.Month == Month && o.Year == Year;
        }

        public override string ToString()
        {
            return Year+" - "+Month;
        }
    }
}