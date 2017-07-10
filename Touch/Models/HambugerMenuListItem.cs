using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Touch.Models
{
    class HambugerMenuListItem
    {
        public Symbol ItemSymbol { get; set; }
        public string ItemName { get; set; }
        public Type ItemPage { get; set; }
    }
}
