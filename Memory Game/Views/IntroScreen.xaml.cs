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

namespace Memory_Game.Views
{
    /// <summary>
    /// Interaction logic for IntroScreen.xaml
    /// </summary>
    public partial class IntroScreen : UserControl
    {
        private MainWindow mainWindow;
        private int selectedRows = 4;
        private int selectedColumns = 5;

        public IntroScreen(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            HighlightSelectedButton(EasyButton);
        }

        private void StartButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            mainWindow.ShowGameScreen( selectedRows, selectedColumns);
        }

        private void HighlightSelectedButton(Button selectedButton)
        {
            EasyButton.Opacity = 0.5;
            MediumButton.Opacity = 0.5;
            HardButton.Opacity = 0.5;
            selectedButton.Opacity = 1;
        }

        private void EasyButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRows = 5;
            selectedColumns = 4;
            HighlightSelectedButton(EasyButton);
        }

        private void MediumButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRows = 5;
            selectedColumns = 6;
            HighlightSelectedButton(MediumButton);
        }

        private void HardButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRows = 5;
            selectedColumns = 8;
            HighlightSelectedButton(HardButton);
        }
    }
}
