using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Symbion
{
    [ContentProperty("Icon")]
    public class MetroButton : Control
    {
        public static readonly DependencyProperty IconProperty;
        public static readonly DependencyProperty StrokeThicknessProperty;

        public static readonly DependencyProperty IsPressedProperty;


        public bool IsPressed
        {
            get
            {
                return (bool)GetValue(IsPressedProperty);
            }
            set
            {
                SetValue(IsPressedProperty, value);
            }
        }


        public Geometry Icon
        {
            get
            {
                return (Geometry)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }

        public double StrokeThickness
        {
            get
            {
                return (double)GetValue(StrokeThicknessProperty);
            }
            set
            {
                SetValue(StrokeThicknessProperty, value);
            }
        }


        public static readonly RoutedEvent ClickEvent;

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        static MetroButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(MetroButton),
                new FrameworkPropertyMetadata(typeof(MetroButton)));

            ClickEvent = EventManager.RegisterRoutedEvent(
                "Click", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(MetroButton));

            IconProperty = DependencyProperty.Register(
                "Icon", typeof(Geometry), typeof(MetroButton));

            StrokeThicknessProperty = DependencyProperty.Register(
                "StrokeThickness", typeof(double), typeof(MetroButton));

            IsPressedProperty = DependencyProperty.Register(
                "IsPressed", typeof(bool), typeof(MetroButton));

        }

        private UIElement _border;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _border = Template.FindName("PART_BORDER", this) as UIElement;
            if (_border == null) _border = new Ellipse();
            _border.MouseLeftButtonDown += _border_MouseLeftButtonDown;
            _border.MouseLeftButtonUp += _border_MouseLeftButtonUp;
        }

        private void _border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsPressed = true;
        }

        private void _border_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent, this));
            IsPressed = false;
            e.Handled = true;   // stop routing (optional)
        } 
    }
}
