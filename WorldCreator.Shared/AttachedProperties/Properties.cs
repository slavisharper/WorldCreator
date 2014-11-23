using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WorldCreator.AttachedProperties
{
    public class Properties
    {
        public static bool GetSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectedProperty);
        }

        public static void SetSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectedProperty, value);
        }

        // Using a DependencyProperty as the backing store for Selected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.RegisterAttached("Selected", 
                typeof(bool), 
                typeof(Control),
                new PropertyMetadata(false));

        private static void OnSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var el = d as Control;
            if (el != null)
            {
                bool value = bool.Parse(e.NewValue.ToString());
                if (value)
                {
                    el.Width = el.ActualWidth / 2;
                    el.Height = el.ActualHeight / 2;
                }
                else
                {
                    el.Width = el.ActualWidth * 2;
                    el.Height = el.ActualHeight * 2;
                }
            }
        }
    }
}
