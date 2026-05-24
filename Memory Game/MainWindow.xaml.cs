using Memory_Game.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string CurrentPlayer { get; set; } = "Player";
        public MainWindow()
        {
            InitializeComponent();
            MainContainer.Children.Add(new IntroScreen(this));
        }

        public void ShowGameScreen(int rows, int columns)
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add( new GameScreen(rows, columns));
        }

        public void ShowIntroScreen()
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add(new IntroScreen(this));
        }

        public void ShowLeaderboardScreen()
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add(new LeaderboardScreen());
        }

    }
}