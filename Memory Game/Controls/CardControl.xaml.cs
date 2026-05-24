using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using Memory_Game.Models;

namespace Memory_Game.Controls
{
    public partial class CardControl : UserControl
    {
        public Carte CardData { get; set; }
        public Button CardButtonControl{get{return CardButton;}}

        public CardControl()
        {
            InitializeComponent();
        }

        public void Flip()
        {
            if (CardData.esteIntoarsa) return;
            DoubleAnimation hideAnimation = new()
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(120)
            };

            hideAnimation.Completed += (s, e) =>
            {
                CardImage.Source = new BitmapImage(new Uri(CardData.simbol, UriKind.Relative));

                DoubleAnimation showAnimation = new()
                {
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(120)
                };

                CardScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty,showAnimation);
            };

            CardScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty,hideAnimation);
            CardData.esteIntoarsa = true;
        }

        public void Hide()
        {
            DoubleAnimation hideAnimation = new()
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(120)
            };

            hideAnimation.Completed += (s, e) =>
            {
                CardImage.Source = new BitmapImage(new Uri("/Assets/Cards/card-back.png", UriKind.Relative));

                DoubleAnimation showAnimation = new()
                {
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(120)
                };

                CardScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty,showAnimation);
            };

            CardScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty,hideAnimation);
            CardData.esteIntoarsa = false;
        }

        public bool IsFlipped()
        {
            return CardData.esteIntoarsa;
        }
    }
}