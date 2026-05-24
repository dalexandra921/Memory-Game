using System;
using System.Windows;
using System.Windows.Controls;
using Memory_Game.Controls;
using System.Windows.Threading;
using Memory_Game.Models;
using System.IO;
using System.Text.Json;
using Memory_Game.Models;

namespace Memory_Game.Views
{
    public partial class GameScreen : UserControl
    {
        private List<CardControl> selectedCards = new();
        private bool canPlay = true;
        private DispatcherTimer timer = new();
        private int seconds = 0;
        private bool timerStarted = false;
        private int rows;
        private int columns;

        private Joc jocCurent = new();

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
                CardControl cardControl = new(); //componenta vizuala a cartii

                Carte carte = new() //datele cartii efective
                {
                    id = jocCurent.listaCarti.Count + 1,
                    simbol = image,
                    esteGasita = false,
                    esteIntoarsa = false
                };

                cardControl.CardData = carte;

                jocCurent.listaCarti.Add(carte);
                cardControl.CardButton.Tag = cardControl;
                cardControl.CardButton.Click += Card_Click;
                GameBoard.Children.Add(cardControl);
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
            CardControl clickedCard =
                (CardControl)clickedButton.Tag;

            if (clickedCard.IsFlipped() || clickedCard.CardData.esteGasita) return;

            clickedCard.Flip();
            selectedCards.Add(clickedCard);

            if (selectedCards.Count == 2)
            {
                jocCurent.mutari++;
                MovesText.Text = $"Moves: {jocCurent.mutari}";
                canPlay = false;
                await CheckMatch();
            }
        }
        private async Task CheckMatch() //asteapta sa se termine verificarea
        {
            CardControl firstCard = selectedCards[0];
            CardControl secondCard = selectedCards[1];

            if (firstCard.CardData.simbol == secondCard.CardData.simbol)
            {
                firstCard.CardData.esteGasita = true;
                secondCard.CardData.esteGasita = true;

                jocCurent.scor += 10;
                ScoreText.Text = $"Score: {jocCurent.scor}";
                CheckWin();
            }
            else
            {
                jocCurent.scor -= 2;
                ScoreText.Text = $"Score: {jocCurent.scor}";

                await Task.Delay(1500);
                firstCard.Hide();
                secondCard.Hide();
            }

            selectedCards.Clear();
            canPlay = true;
        }

        private void CheckWin()
        {
            bool allMatched = true;

            foreach (CardControl card in GameBoard.Children)
            {
                if (!card.CardData.esteGasita)
                {
                    allMatched = false;
                    break;
                }
            }

            if (allMatched)
            {
                timer.Stop();
                SavePlayerStats();
                MainWindow mainWindow =(MainWindow)Window.GetWindow(this);

                MessageBox.Show(
                    $"Congratulations, {mainWindow.CurrentPlayer}!" +
                    $"\n\nScore: {jocCurent.scor}" +
                    $"\nMoves: {jocCurent.mutari}" +
                    $"\nTime: {TimerText.Text}",
                    "Victory"
                );
            }
        }

        private void ResetGame()
        {
            GameBoard.Children.Clear();
            selectedCards.Clear();
            canPlay = true;

            jocCurent.scor = 0;
            jocCurent.mutari = 0;
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

        private void SavePlayerStats()
        {
            MainWindow mainWindow =
                (MainWindow)Window.GetWindow(this);

            string username = mainWindow.CurrentPlayer;
            string path = "players.json";

            List<Player> players = new();

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    players = JsonSerializer.Deserialize<List<Player>>(json)
                                ?? new List<Player>();
                }
            }

            int multiplier = 1;
            string difficulty = "Easy";

            if (columns == 5)
            {
                multiplier = 2;
                difficulty = "Medium";
            }

            if (columns == 8)
            {
                multiplier = 3;
                difficulty = "Hard";
            }

            int rankedScore = jocCurent.scor * multiplier;

            Player existingPlayer =
                players.FirstOrDefault(p => p.Username == username);

            if (existingPlayer == null)
            {
                existingPlayer = new Player
                {
                    Username = username,
                    BestScore = jocCurent.scor,
                    RankedScore = rankedScore,
                    Difficulty = difficulty,
                    GamesPlayed = 1,
                    Wins = 1,
                    BestTime = TimerText.Text
                };

                players.Add(existingPlayer);
            }
            else
            {
                existingPlayer.GamesPlayed++;
                existingPlayer.Wins++;
                
                if (rankedScore > existingPlayer.RankedScore)
                {
                    existingPlayer.BestScore = jocCurent.scor;
                    existingPlayer.RankedScore = rankedScore;
                    existingPlayer.Difficulty = difficulty;
                }

                existingPlayer.BestTime = TimerText.Text;
            }

            string updatedJson =
                JsonSerializer.Serialize(players,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(path, updatedJson);
        }

    }
}