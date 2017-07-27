using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Touch.Controls
{
    public interface INavigableUserControl
    {
        bool Shown { get; set; }

        void OnShow();

        void OnHide();

        void ToggleAnimation();
    }
}
