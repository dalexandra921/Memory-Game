using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace Memory_Game.Controls
{
    public partial class CardControl : UserControl
    {
        private bool isFlipped = false;

        public bool IsMatched { get; set; }

        public string FrontImage { get; set; }

        public Button CardButtonControl
        {
            get
            {
                return CardButton;
            }
        }

        public CardControl()
        {
            InitializeComponent();
        }

        public void Flip()
        {
            if (isFlipped) return;

            DoubleAnimation hideAnimation = new()
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(120)
            };

            hideAnimation.Completed += (s, e) =>
            {
                CardImage.Source = new BitmapImage(
                    new Uri(FrontImage, UriKind.Relative)
                );

                DoubleAnimation showAnimation = new()
                {
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(120)
                };

                CardScale.BeginAnimation(
                    System.Windows.Media.ScaleTransform.ScaleXProperty,
                    showAnimation
                );
            };

            CardScale.BeginAnimation(
                System.Windows.Media.ScaleTransform.ScaleXProperty,
                hideAnimation
            );

            isFlipped = true;
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
                CardImage.Source = new BitmapImage(
                    new Uri("/Assets/Cards/card-back.png", UriKind.Relative)
                );

                DoubleAnimation showAnimation = new()
                {
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(120)
                };

                CardScale.BeginAnimation(
                    System.Windows.Media.ScaleTransform.ScaleXProperty,
                    showAnimation
                );
            };

            CardScale.BeginAnimation(
                System.Windows.Media.ScaleTransform.ScaleXProperty,
                hideAnimation
            );

            isFlipped = false;
        }

        public bool IsFlipped()
        {
            return isFlipped;
        }
    }
}