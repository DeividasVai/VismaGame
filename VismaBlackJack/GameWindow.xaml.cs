using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VismaBlackJack
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public double PlayerOwnedMoney = 1000;
        Random rnd = new Random();
        public int playerPoints = 0, PCPoints = 0;
        public int tempCalc;
        public int WinCount = 0, LooseCount = 0;
        public double yourStake = new double();
        public string[] CardValue = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        public string[] CardType = new string[] { "♥", "♦", "♣", "♠" };
        public int playerTurn = 1, compTurn = 1;

        Dictionary<string, int> UsedCards = new Dictionary<string, int>();

        public GameWindow()
        {
            InitializeComponent();
            PlayerMoney.Content = PlayerOwnedMoney.ToString();
            Playername.Content = MainWindow.Name;

            
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (playerTurn == 1)
            {
                int errorCounter = Regex.Matches(PlayerStake.Text, @"[a-zA-Z]").Count;
                if ((errorCounter == 0) && (PlayerStake.Text != ""))
                {
                    yourStake = Convert.ToDouble(PlayerStake.Text);
                    if (PlayerOwnedMoney >= yourStake)
                    {
                        PlayerOwnedMoney = PlayerOwnedMoney - yourStake;
                        PlayerMoney.Content = PlayerOwnedMoney.ToString();
                        PlayerStake.Text = "";
                        FirstTurnPlayer();
                        FirstTurnPC();
                    }
                    else MessageBox.Show("You don't have enough money", "Error", MessageBoxButton.OK);
                    PlayerStake.Clear();
                }
                else MessageBox.Show("Only numbers can be typed in to the box on the left\nAlso, it can not be blank", "Error", MessageBoxButton.OK);
                PlayerStake.Clear();
            }
            else MessageBox.Show("You haven't finished your turn\nPress the Stop button to end", "Error", MessageBoxButton.OK);
            PlayerStake.Clear();
        }

        public void FirstTurnPlayer()
        {
            bool haventUsed;
            while (playerTurn < 3)
            {
                if (playerTurn < 3) haventUsed = false;
                else haventUsed = true;

                while (haventUsed == false)
                {
                    string builder = "";
                    int iteration = 0;
                    iteration = rnd.Next(0, 3);
                    builder += CardType[iteration];
                    iteration = rnd.Next(0, 12);
                    tempCalc = PointCalculator(iteration);
                    builder += CardValue[iteration];
                    if (!UsedCards.ContainsKey(builder))
                    {
                        UsedCards.Add(builder, playerTurn);
                        if (playerTurn == 1)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) PlayerCard1.Foreground = Brushes.Red;
                            PlayerCard1.Content = builder;
                        }
                        if (playerTurn == 2)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) PlayerCard2.Foreground = Brushes.Red;
                            PlayerCard2.Content = builder;
                        }
                        playerPoints += tempCalc;
                        YourScore.Content = playerPoints;
                        playerTurn++;
                        haventUsed = true;
                    }
                }
            }
        }

        public void FirstTurnPC()
        {
            bool haventUsed;
            while (compTurn < 3)
            {
                if (compTurn < 3) haventUsed = false;
                else haventUsed = true;

                while (haventUsed == false)
                {
                    string builder = "";
                    int iteration = 0;
                    iteration = rnd.Next(0, 3);
                    builder += CardType[iteration];
                    iteration = rnd.Next(0, 12);
                    tempCalc = PointCalculator(iteration);
                    builder += CardValue[iteration];
                    if (!UsedCards.ContainsKey(builder))
                    {
                        UsedCards.Add(builder, playerTurn);
                        if (compTurn == 1)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) CompCard1.Foreground = Brushes.Red;
                            CompCard1.Content = builder;
                        }
                        if (compTurn == 2)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) CompCard2.Foreground = Brushes.Red;
                            CompCard2.Content = builder;
                        }
                        PCPoints += tempCalc;
                        PCScore.Content = PCPoints;
                        compTurn++;
                        haventUsed = true;
                    }
                }
            }
        }

        public int PointCalculator(int x)
        {
            int Points = 0;

            // tasku sistema https://www.beatblackjack.org/rules/

            switch (CardValue[x])
            {
                case "2":
                    Points = 2;
                    break;
                case "3":
                    Points = 3;
                    break;
                case "4":
                    Points = 4;
                    break;
                case "5":
                    Points = 5;
                    break;
                case "6":
                    Points = 6;
                    break;
                case "7":
                    Points = 7;
                    break;
                case "8":
                    Points = 8;
                    break;
                case "9":
                    Points = 9;
                    break;
                case "10":
                    Points = 10;
                    break;
                case "J":
                    Points = 10;
                    break;
                case "Q":
                    Points = 10;
                    break;
                case "K":
                    Points = 10;
                    break;
                case "A":
                    if (playerPoints <= 10)
                    {
                        Points = 11;
                    }
                    else Points = 1;
                    break;

            }
            return Points;
        }

        private void GetCardBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (playerTurn)
            {
                case 3:
                    getCardOneByOne("Player");
                    break;
                case 4:
                    getCardOneByOne("Player");
                    break;
                case 5:
                    getCardOneByOne("Player");
                    break;
            }
        }

        public void getCardOneByOne(string who)
        {
            bool haventUsed = false;

            while (haventUsed == false)
            {
                string builder = "";
                int iteration = 0;
                iteration = rnd.Next(0, 3);
                builder += CardType[iteration];
                iteration = rnd.Next(0, 12);
                tempCalc = PointCalculator(iteration);
                builder += CardValue[iteration];

                if (!UsedCards.ContainsKey(builder))
                {
                    UsedCards.Add(builder, playerTurn);

                    if (who == "Player")
                    {
                        if (playerTurn == 3)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) PlayerCard3.Foreground = Brushes.Red;
                            PlayerCard3.Content = builder;
                        }
                        if (playerTurn == 4)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) PlayerCard4.Foreground = Brushes.Red;
                            PlayerCard4.Content = builder;
                        }
                        if (playerTurn == 5)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) PlayerCard5.Foreground = Brushes.Red;
                            PlayerCard5.Content = builder;
                        }
                        playerPoints += tempCalc;
                        YourScore.Content = playerPoints;
                        playerTurn++;

                        if (playerPoints > 21) StopBtn_Click(new object(), new RoutedEventArgs());

                    }

                    else
                    {
                        if (compTurn == 3)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) CompCard3.Foreground = Brushes.Red;
                            CompCard3.Content = builder;
                        }
                        if (compTurn == 4)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) CompCard4.Foreground = Brushes.Red;
                            CompCard4.Content = builder;
                        }
                        if (compTurn == 5)
                        {
                            if ((builder.Contains("♥")) || (builder.Contains("♦"))) CompCard5.Foreground = Brushes.Red;
                            CompCard5.Content = builder;
                        }

                        PCPoints += tempCalc;
                        PCScore.Content = PCPoints;
                        compTurn++;
                    }
                    haventUsed = true;
                }
            }

        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            if (playerPoints <= 21)
            {
                while ((PCPoints < playerPoints) && (PCPoints < 18))
                {
                    switch (compTurn)
                    {
                        case 3:
                            getCardOneByOne("PC");
                            break;
                        case 4:
                            getCardOneByOne("PC");
                            break;
                        case 5:
                            getCardOneByOne("PC");
                            break;
                    }
                }
            }
            if(playerPoints > 21)
            {
                MessageBox.Show("Lost", "End of round", MessageBoxButton.OK);
                LooseCount++;
                gamesLostLb.Content = LooseCount;
            }
            else if (PCPoints > 21)
            {
                if (playerPoints == 21)
                {
                    // 400% player win
                    yourStake = yourStake * 8;
                    PlayerOwnedMoney += yourStake;
                    PlayerMoney.Content = PlayerOwnedMoney;
                }
                else
                {
                    yourStake = yourStake * 2;
                    PlayerOwnedMoney += yourStake;
                    PlayerMoney.Content = PlayerOwnedMoney;
                }
                MessageBox.Show("You have won", "End of round", MessageBoxButton.OK);
                WinCount++;
                GamesWonLb.Content = WinCount;
            }
            else if (PCPoints > playerPoints)
            {
                // PC wins
                MessageBox.Show("Lost", "End of round", MessageBoxButton.OK);
                LooseCount++;
                gamesLostLb.Content = LooseCount;
            }
            else if (PCPoints < playerPoints)
            {
                // Player wins
                if(playerPoints == 21)
                {
                    MessageBox.Show("You have won with 21", "End of round", MessageBoxButton.OK);
                    yourStake = yourStake * 8;
                    PlayerOwnedMoney += yourStake;
                    PlayerMoney.Content = PlayerOwnedMoney;
                    // 800% player win
                }
                else MessageBox.Show("You have won", "End of round", MessageBoxButton.OK);
                WinCount++;
                GamesWonLb.Content = WinCount;
            }
            else if (PCPoints == playerPoints)
            {
                // PC = Player
                if(PCPoints == 21)
                {
                    // 400% player win
                    yourStake = yourStake * 4;
                    PlayerOwnedMoney += yourStake;
                    PlayerMoney.Content = PlayerOwnedMoney;
                    MessageBox.Show("Draw", "End of round", MessageBoxButton.OK);
                }
                else
                {
                    PlayerOwnedMoney += yourStake;
                    PlayerMoney.Content = PlayerOwnedMoney;
                    MessageBox.Show("Draw","End of round", MessageBoxButton.OK);
                }
            }

            refreshGame();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.Key >= Key.D0) && (e.Key <= Key.D9)) || (e.Key == Key.Decimal) || ((e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9)))
            {
                PlayerStake.Focus();
            }
            if(e.Key == Key.Enter)
            {
                StartBtn_Click(new object(), new RoutedEventArgs());
            }
            if (e.Key == Key.Tab)
            {
                GetCardBtn_Click(new object(), new RoutedEventArgs());
            }
            if (e.Key == Key.Escape)
            {
                StopBtn_Click(new object(), new RoutedEventArgs());
            }
        }

        public void refreshGame()
        {

            PlayerCard1.Content = "";
            PlayerCard1.Foreground = Brushes.Black;
            PlayerCard2.Content = "";
            PlayerCard2.Foreground = Brushes.Black;
            PlayerCard3.Content = "";
            PlayerCard3.Foreground = Brushes.Black;
            PlayerCard4.Content = "";
            PlayerCard4.Foreground = Brushes.Black;
            PlayerCard5.Content = "";
            PlayerCard5.Foreground = Brushes.Black;

            CompCard1.Content = "";
            CompCard1.Foreground = Brushes.Black;
            CompCard2.Content = "";
            CompCard2.Foreground = Brushes.Black;
            CompCard3.Content = "";
            CompCard3.Foreground = Brushes.Black;
            CompCard4.Content = "";
            CompCard4.Foreground = Brushes.Black;
            CompCard5.Content = "";
            CompCard5.Foreground = Brushes.Black;

            playerTurn = 1;
            compTurn = 1;
            UsedCards.Clear();
            YourScore.Content = "";
            PCScore.Content = "";
            playerPoints = 0;
            PCPoints = 0;

            if(PlayerOwnedMoney == 0)
            {
                MessageBox.Show("Oh dear!\nYou are out of cash!", "Lost completely", MessageBoxButton.OK);
                menuBtn_Click(new object(), new RoutedEventArgs());
            }
        }

        private void menuBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wind = new MainWindow();
            wind.Show();
            this.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
