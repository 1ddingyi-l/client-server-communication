using System.Windows;

namespace Lib
{
    public class MessageNotifyer
    {
        public static void ShowError(string content, string caption = "")
            => MessageBox.Show(content, caption == "" ? "Error" : caption, MessageBoxButton.OK, MessageBoxImage.Error);

        public static void ShowWarning(string content, string caption = "")
            => MessageBox.Show(content, caption == "" ? "Warning" : caption, MessageBoxButton.OK, MessageBoxImage.Warning);

        public static void ShowConfirmWindow(string content, string caption = "")
            => MessageBox.Show(content, caption == "" ? "Please confirm" : caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

        public static void ShowSuccess(string content, string caption = "")
            => MessageBox.Show(content, caption == "" ? "Success" : caption, MessageBoxButton.OK, MessageBoxImage.Asterisk);

        public static void ShowTest(string test)
            => MessageBox.Show(test, "Testing statement", MessageBoxButton.OK, MessageBoxImage.Hand);
    }
}
