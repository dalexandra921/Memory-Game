using System;
using Memory_Game.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Memory_Game.Views
{
    public partial class LeaderboardScreen : UserControl
    {
        private MainWindow mainWindow;
        public LeaderboardScreen(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            LoadLeaderboard();
        }

        private void LoadLeaderboard()
        {
            string path = "players.json";
            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);

            List<Player> players =
                JsonSerializer.Deserialize<List<Player>>(json)
                ?? new List<Player>();

            var sortedPlayers = players
                .OrderByDescending(p => p.RankedScore)
                .ThenBy(p => p.BestTime)    
                .ToList();

            int rank = 1;

            foreach (Player player in sortedPlayers)
            {
                Border card = new()
                {
                    CornerRadius = new CornerRadius(20),
                    Margin = new Thickness(0, 0, 0, 20),
                    Padding = new Thickness(20),
                    Width = 700
                };

                if (rank == 1)
                    card.Background =
                        new System.Windows.Media.SolidColorBrush(
                            System.Windows.Media.Color.FromRgb(212, 175, 55));

                else if (rank == 2)
                    card.Background =
                        new System.Windows.Media.SolidColorBrush(
                            System.Windows.Media.Color.FromRgb(192, 192, 192));

                else if (rank == 3)
                    card.Background =
                        new System.Windows.Media.SolidColorBrush(
                            System.Windows.Media.Color.FromRgb(205, 127, 50));

                else
                    card.Background =
                        new System.Windows.Media.SolidColorBrush(
                            System.Windows.Media.Color.FromRgb(45, 45, 45));

                StackPanel content = new();

                TextBlock title = new()
                {
                    Text = $"#{rank}   {player.Username}   -   {player.RankedScore} pts ({player.Difficulty})",
                    FontSize = 28,
                    FontWeight = FontWeights.Bold,
                    Foreground = System.Windows.Media.Brushes.White
                };

                TextBlock details = new()
                {
                    Text =
                        $"Wins: {player.Wins}   |   " +
                        $"Games: {player.GamesPlayed}   |   " +
                        $"Best Time: {player.BestTime}",

                    FontSize = 18,
                    Margin = new Thickness(0, 10, 0, 0),
                    Foreground = System.Windows.Media.Brushes.White
                };

                content.Children.Add(title);
                content.Children.Add(details);
                card.Child = content;
                LeaderboardPanel.Children.Add(card);
                rank++;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ShowIntroScreen();
        }
    }
}
