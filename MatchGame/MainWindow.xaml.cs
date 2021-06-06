using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
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
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }

        private TextBlock _lasTextBlockClicked;
        private bool _findingMatch;

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                textBlock.Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                _lasTextBlockClicked.Visibility = Visibility.Visible;
                _findingMatch = false;
            }
        }
    }
}
