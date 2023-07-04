using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPFDevelopers.Helpers;

namespace MessageSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LightDark_Checked(object sender, RoutedEventArgs e)
        {
            var lightDark = sender as ToggleButton;
            if (lightDark == null) return;
            ControlsHelper.ToggleLightAndDark(lightDark.IsChecked == true);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            switch (btn.Tag)
            {
                case "Info":
                    Message.PushMessage("这是一条成功消息", MessageBoxImage.Information);
                    break;
                case "Error":
                    Message.PushMessage("这是一条错误消息", MessageBoxImage.Error);
                    break;
                case "Warning":
                    Message.PushMessage("这是一条警告消息", MessageBoxImage.Warning);
                    break;
                case "Question":
                    Message.PushMessage("这是一条询问消息", MessageBoxImage.Question);
                    break;
                default:
                    Message.PushMessage("这是一条很长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长长消息", MessageBoxImage.Information);
                    break;
            }
        }
        
    }
}
