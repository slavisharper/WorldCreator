namespace WorldCreator.AttachedProperties
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class CanvasExtensions
    {
        public static double GetBottom(DependencyObject obj)
        {
            return (double)obj.GetValue(BottomProperty);
        }

        public static void SetBottom(DependencyObject obj, double value)
        {
            obj.SetValue(BottomProperty, value);
        }

        // Using a DependencyProperty as the backing store for Bottom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomProperty =
            DependencyProperty.RegisterAttached("Bottom", 
            typeof(double),
            typeof(FrameworkElement), 
            new PropertyMetadata(0, OnBottomChanged));

        private static void OnBottomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement el = d as FrameworkElement;
            if (el != null)
            {
                var canvas = el.Parent as Canvas;
                if (canvas != null)
                {
                    var top = canvas.ActualHeight - double.Parse(e.NewValue.ToString());
                    Canvas.SetTop(el, top);
                }
            }
        }
    }
}
