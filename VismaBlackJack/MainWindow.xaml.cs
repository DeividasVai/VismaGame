using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VismaBlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Name;

        public MainWindow()
        {
            InitializeComponent();
            if (NameTextBox.Text == "Insert name here")
            {
                NameTextBox.Focus();
                NameTextBox.SelectAll();
            }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void StartGameBtn_Click(object sender, RoutedEventArgs e)
        {
            Name = NameTextBox.Text;
            GameWindow wind = new GameWindow();
            wind.Show();
            this.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                StartGameBtn_Click(new object(), new RoutedEventArgs());
            }
        }
    }
}
