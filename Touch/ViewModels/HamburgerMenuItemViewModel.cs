using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Touch.ViewModels
{
    public class HamburgerMenuItemViewModel : DependencyObject
    {
        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.RegisterAttached("Glyph", typeof(string), typeof(HamburgerMenuItemViewModel), null);

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(HamburgerMenuItemViewModel), null);

        public static readonly DependencyProperty PageProperty =
            DependencyProperty.RegisterAttached("Page", typeof(Page), typeof(HamburgerMenuItemViewModel), null);

        public string Glyph
        {
            get => (string) GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Type Page
        {
            get => (Type) GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }
    }
}