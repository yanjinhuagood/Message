using System.Windows;
using System.Windows.Controls;

namespace MessageSample
{
    public class MessageItem : ListBoxItem
    {
        public MessageBoxImage MessageType
        {
            get { return (MessageBoxImage)GetValue(MessageTypeProperty); }
            set { SetValue(MessageTypeProperty, value); }
        }
        public static readonly DependencyProperty MessageTypeProperty =
            DependencyProperty.Register("MessageType", typeof(MessageBoxImage), typeof(MessageItem), new PropertyMetadata(MessageBoxImage.Information));


    }
}
