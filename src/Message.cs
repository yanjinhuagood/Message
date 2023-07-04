using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace MessageSample
{
    public static class Message
    {
        private static MessageAdorner messageAdorner;
        public static void PushMessage( string message, MessageBoxImage type = MessageBoxImage.Information)
        {
            if (messageAdorner != null)
            {
                messageAdorner.PushMessage(message, type);
                return;
            }
            Window win = null;
            if (Application.Current.Windows.Count > 0)
            {
                win = Application.Current.Windows.OfType<Window>().FirstOrDefault(o => o.IsActive);
                if (win == null)
                    win = Application.Current.Windows.OfType<Window>().First(o => o.IsActive);
            }

            var layer = GetAdornerLayer(win);
            if (layer == null)
                throw new Exception("not AdornerLayer is null");
            messageAdorner = new MessageAdorner(layer);
            layer.Add(messageAdorner);
            messageAdorner.PushMessage(message, type);
        }
        static AdornerLayer GetAdornerLayer(Visual visual)
        {
            var decorator = visual as AdornerDecorator;
            if (decorator != null)
                return decorator.AdornerLayer;
            var presenter = visual as ScrollContentPresenter;
            if (presenter != null)
                return presenter.AdornerLayer;
            var visualContent = (visual as Window)?.Content as Visual;
            return AdornerLayer.GetAdornerLayer(visualContent ?? visual);
        }
    }
    public class MessageAdorner : Adorner
    {
        private ListBox listBox;
        private UIElement _child;
        private FrameworkElement adornedElement;
        public MessageAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.adornedElement = adornedElement as FrameworkElement;
        }
      
        public void PushMessage(string message, MessageBoxImage type = MessageBoxImage.Information)
        {
            if (listBox == null)
            {
                listBox = new ListBox() { Style = null ,BorderThickness = new Thickness(0) ,Background = Brushes.Transparent};
                Child = listBox;
            }
            var item = new MessageItem{ Content = message , MessageType = type};
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += (sender, e) =>
            {
                listBox.Items.Remove(item);
                timer.Stop();
            };
            listBox.Items.Insert(0,item);
            timer.Start();
        }

        public UIElement Child
        {
            get => _child;
            set
            {
                if (value == null)
                {
                    RemoveVisualChild(_child);
                    _child = value;
                    return;
                }
                AddVisualChild(value);
                _child = value;
            }
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return _child != null ? 1 : 0;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = (adornedElement.ActualWidth - _child.DesiredSize.Width) / 2;
            _child.Arrange(new Rect(new Point(x, 0), _child.DesiredSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index == 0 && _child != null) return _child;
            return base.GetVisualChild(index);
        }
    }
}
