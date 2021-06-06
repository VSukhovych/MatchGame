using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int _tenthOfSecondsElapsed;
        private int _matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _tenthOfSecondsElapsed++;
            timeTextBlock.Text = (_tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if (_matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐶","🐶",
                "🐺","🐺",
                "🐱","🐱",
                "🐯","🐯",
                "🦊","🦊",
                "🦓","🦓",
                "🐴","🐴",
                "🐗","🐗"
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            _tenthOfSecondsElapsed = 0;
            _matchesFound = 0;
        }

        private TextBlock _lasTextBlockClicked;
        private bool _findingMatch;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (_findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                _lasTextBlockClicked = textBlock;
                _findingMatch = true;
            }
            else if (textBlock.Text == _lasTextBlockClicked.Text)
            {
                _matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                _lasTextBlockClicked.Visibility = Visibility.Visible;
                _findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
