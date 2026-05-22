using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Memory_Game.Controls
{
    public partial class CardControl : UserControl
    {
        private bool isFlipped = false;
        public bool IsMatched { get; set; }
        public string FrontImage { get; set; }

        public Button CardButtonControl { get { return CardButton; } }
            
        public CardControl()
        {
            InitializeComponent();
        }

        //private void Card_Click(object sender, RoutedEventArgs e)
        //{
        //    Flip();
        //}

        public void Flip()
        {
            if (isFlipped)
                return;

            CardImage.Source = new BitmapImage(new Uri(FrontImage, UriKind.Relative));
            isFlipped = true;
        }

        public void Hide()
        {
            CardImage.Source = new BitmapImage(new Uri("/Assets/Cards/card-back.png", UriKind.Relative));
            isFlipped = false;
        }

        public bool IsFlipped()
        {
            return isFlipped;
        }
    }
}