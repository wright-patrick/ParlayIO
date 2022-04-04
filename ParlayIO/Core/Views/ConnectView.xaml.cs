using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ParlayIO.Client.Core.Views
{
    /// <summary>
    /// Interaction logic for ConnectView.xaml
    /// </summary>
    public partial class ConnectView : UserControl
    {
        private delegate void SetupDelegate();
        public ConnectView()
        {
            InitializeComponent();

            _firstNameField.GotFocus += _firstNameField_GotFocus;
            _firstNameField.LostFocus += _firstNameField_LostFocus;
            _lastNameField.GotFocus += _lastNameField_GotFocus;
            _lastNameField.LostFocus += _lastNameField_LostFocus;
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new SetupDelegate(Setup));
        }

        private void Setup()
        {
            _lastNameField.Foreground = Brushes.LightGray;
            _lastNameField.Text = "Enter Last Name";
            _firstNameField.Foreground = Brushes.LightGray;
            _firstNameField.Text = "Enter First Name";
        }

        private void _lastNameField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_lastNameField.Text))
            {
                _lastNameField.Foreground = Brushes.LightGray;
                _lastNameField.Text = "Enter Last Name";
            }
        }

        private void _lastNameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(_lastNameField.Text == "Enter Last Name")
            {
                _lastNameField.Foreground = Brushes.Black;
                _lastNameField.Text = "";
            }
        }

        private void _firstNameField_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_firstNameField.Text))
            {
                _firstNameField.Foreground = Brushes.LightGray;
                _firstNameField.Text = "Enter First Name";
            }
        }

        private void _firstNameField_GotFocus(object sender, RoutedEventArgs e)
        {
            if(_firstNameField.Text == "Enter First Name")
            {
                _firstNameField.Foreground = Brushes.Black;
                _firstNameField.Text = "";
            }
        }
    }
}
