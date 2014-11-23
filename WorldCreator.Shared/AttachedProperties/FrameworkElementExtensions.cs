using System;
using System.Collections.Generic;
using System.Text;

namespace WorldCreator.AttachedProperties
{
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using WorldCreator.Views;

    public class FrameworkElementExtensions
    {
        public static bool GetSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectedProperty);
        }

        public static void SetSelected(DependencyObject obj, double value)
        {
            obj.SetValue(SelectedProperty, value);
        }

        // Using a DependencyProperty as the backing store for Bottom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.RegisterAttached("Selected",
            typeof(bool),
            typeof(FrameworkElement),
            new PropertyMetadata(false, SelectedChanged));

        private static void SelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var el = d as FrameworkElement;
            if (el != null)
            {
                bool value = bool.Parse(e.NewValue.ToString());
                if (value)
                {
                    el.Width = el.Width / 2;
                    el.Height = el.Height / 2;
                }
                else
                {
                    el.Width = el.Width * 2;
                    el.Height = el.Height * 2;
                }
            }
        }
        
    }
}
