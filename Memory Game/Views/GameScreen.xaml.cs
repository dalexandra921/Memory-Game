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
using Memory_Game.Controls;
using System.Windows.Threading;

namespace Memory_Game.Views
{
    public partial class GameScreen : UserControl
    {
        private List<CardControl> selectedCards = new();
        private bool canPlay = true;
        private int moves = 0;
        private int score = 0;
        private DispatcherTimer timer = new();
        private int seconds = 0;
        private bool timerStarted = false;
        private int rows;
        private int columns;

        public GameScreen(int rows, int columns)
        {
            InitializeComponent();
           
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            this.rows = rows;
            this.columns = columns;
            CreateCards();
        }

        private void CreateCards()
        {
            List<string> allCards = new()
            {
                "/Assets/Cards/card1.png",
                "/Assets/Cards/card2.png",
                "/Assets/Cards/card3.png",
                "/Assets/Cards/card4.png",
                "/Assets/Cards/card5.png",
                "/Assets/Cards/card6.png",
                "/Assets/Cards/card7.png",
                "/Assets/Cards/card8.png",
                "/Assets/Cards/card9.png",
                "/Assets/Cards/card10.png",

                "/Assets/Cards/card11.png",
                "/Assets/Cards/card12.png",
                "/Assets/Cards/card13.png",
                "/Assets/Cards/card14.png",
                "/Assets/Cards/card15.png",
                "/Assets/Cards/card16.png",
                "/Assets/Cards/card17.png",
                "/Assets/Cards/card18.png",
                "/Assets/Cards/card19.png",
                "/Assets/Cards/card20.png",
            };

            Random random = new();
            GameBoard.Rows = rows;
            GameBoard.Columns = columns;
            int totalCards = rows * columns;
            int pairsNeeded = totalCards / 2;

            List<string> selectedCards = allCards
                .OrderBy(x => random.Next())
                .Take(pairsNeeded)
                .ToList();

            List<string> gameCards = selectedCards
                .Concat(selectedCards)
                .OrderBy(x => random.Next())
                .ToList();

            foreach (string image in gameCards)
            {
                CardControl card = new();

                card.FrontImage = image;
                card.CardButton.Click += Card_Click;
                GameBoard.Children.Add(card);
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            seconds++;
            int mins = seconds / 60;
            int secs = seconds % 60;
            TimerText.Text = $"Time: {mins:00}:{secs:00}";
        }

        private async void Card_Click(object sender, RoutedEventArgs e)
        {
            if (!canPlay) return;

            if (!timerStarted)
            {
                timer.Start();
                timerStarted = true;
            }

            Button clickedButton = (Button)sender;

            CardControl clickedCard = (CardControl)clickedButton.Parent;

            if (clickedCard.IsFlipped() || clickedCard.IsMatched)
                return;
            clickedCard.Flip();

            selectedCards.Add(clickedCard);

            if (selectedCards.Count == 2)
            {
                moves++;
                MovesText.Text = $"Moves: {moves}";
                canPlay = false;
                await CheckMatch();
            }
        }
        private async Task CheckMatch() //asteapta sa se termine verificarea
        {
            CardControl first = selectedCards[0];
            CardControl second = selectedCards[1];

            if (first.FrontImage == second.FrontImage)
            {
                first.IsMatched = true;
                second.IsMatched = true;

                score += 10;
                ScoreText.Text = $"Score: {score}";
                CheckWin();
            }
            else
            {
                score -= 2;
                ScoreText.Text = $"Score: {score}";

                await Task.Delay(1500);
                first.Hide();
                second.Hide();
            }

            selectedCards.Clear();
            canPlay = true;
        }

        private void CheckWin()
        {
            bool allMatched = true;

            foreach (CardControl card in GameBoard.Children)
            {
                if (!card.IsMatched)
                {
                    allMatched = false;
                    break;
                }
            }

            if (allMatched)
            {
                timer.Stop();
                MessageBox.Show(
                    $"You won!\n\nScore: {score}\nMoves: {moves}\n" +
                    $"{TimerText.Text}","Victory"
                );
            }
        }

        private void ResetGame()
        {
            GameBoard.Children.Clear();
            selectedCards.Clear();
            canPlay = true;

            score = 0;
            moves = 0;
            seconds = 0;

            timer.Stop();
            timerStarted = false;

            ScoreText.Text = "Score: 0";
            MovesText.Text = "Moves: 0";
            TimerText.Text = "Time: 00:00";

            CreateCards();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.ShowIntroScreen();
        }

    }
}