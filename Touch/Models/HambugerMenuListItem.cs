using System;
using Windows.UI.Xaml.Controls;

namespace Touch.Models
{
    internal class HambugerMenuListItem
    {
        public Symbol ItemSymbol { get; set; }
        public string ItemName { get; set; }
        public Type ItemPage { get; set; }
    }
}