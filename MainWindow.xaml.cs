using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace togizkumalakUI;
public partial class MainWindow : Window
{
    public string namePlayer1 { get; set; }
    public string namePlayer2 { get; set; }
    public int firstplayer { get; set; }
    public class Field
    {
        public static int[] _fieldPlayer2 = new int[9] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
        public static int kazanPlayer1 = 80;
        public static int kazanPlayer2 = 80;
        public static int[] _fieldPlayer1 = new int[9] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
    }
    public MainWindow(string InputnamePlayer1, string InputnamePlayer2, int motionPlayer)
    {
        InitializeComponent();
        namePlayer2 = InputnamePlayer2;
        namePlayer1 = InputnamePlayer1;
        firstplayer = motionPlayer;
        InitializeGameField(namePlayer1, namePlayer2, firstplayer);
    }
    private void InitializeGameField(string namePlayer1, string namePlayer2,int firstplayer)
    {
        namePlayer2TextBlock.Text = namePlayer2;
        Button[] player2Buttons = new Button[]
        {
            Button8, Button7, Button6, Button5, Button4, Button3, Button2, Button1, Button0
        };
        for (int i = 0; i < Field._fieldPlayer2.Length; i++)
        {
            if (Field._fieldPlayer2[i] == -2 || Field._fieldPlayer2[i] == -1)
            {
                player2Buttons[i].Content = "ТУЗДЫК";
                player2Buttons[i].Background = Brushes.Red;
                player2Buttons[i].IsEnabled = false;
            }
            else
                player2Buttons[i].Content = Field._fieldPlayer2[i];
        }
        for (int i = 8; i >= 0; i--)
            player2Buttons[i].Tag = i;
        NamePL1.Text = namePlayer1;
        NamePL2.Text = namePlayer2;
        CountKazanPL1.Text = Convert.ToString(Field.kazanPlayer1);
        CountKazanPL2.Text = Convert.ToString(Field.kazanPlayer2);
        namePlayer1TextBlock.Text = namePlayer1;
        Button[] player1Buttons = new Button[]
        {
            Button9, Button10, Button11, Button12, Button13, Button14, Button15, Button16, Button17
        };
        for (int i = 0; i < player1Buttons.Length; i++)
        {
            if (firstplayer == 1)
            {
                player1Buttons[i].IsEnabled = true;
                player2Buttons[i].IsEnabled = false;
            }
            else
            {
                player1Buttons[i].IsEnabled = false;
                player2Buttons[i].IsEnabled = true;
            }
            if (Field._fieldPlayer1[i] == -2 || Field._fieldPlayer1[i] == -1)
            {
                player1Buttons[i].Content = "ТУЗДЫК";
                player1Buttons[i].Background = Brushes.Red;
                Field._fieldPlayer1[i] = -2;
                player1Buttons[i].IsEnabled = false;
            }
            else
            {
                player1Buttons[i].Content = Field._fieldPlayer1[i];
                player1Buttons[i].Tag = i;
            }
        }

        if (firstplayer == 1)
        {
            ActivePlayer2TextBlock.Text = "В ожидании";
            ActivePlayer2Border.Background = Brushes.LightCoral;
            ActivePlayer1TextBlock.Text = "Делает ход";
            ActivePlayer1Border.Background = Brushes.LightGreen;
            ActivePlayer2BorderField.Background = Brushes.LightCoral;
            ActivePlayer1BorderField.Background = Brushes.LightGreen;
        }
        else
        {
            ActivePlayer2TextBlock.Text = "Делает ход";
            ActivePlayer2Border.Background = Brushes.LightGreen;
            ActivePlayer1TextBlock.Text = "В ожидании";
            ActivePlayer1Border.Background = Brushes.LightCoral;
            ActivePlayer2BorderField.Background = Brushes.LightGreen;
            ActivePlayer1BorderField.Background = Brushes.LightCoral;
        }
        int totalcountPL1 = 0;
        for (int i = 0; i < Field._fieldPlayer1.Length; i++)
            totalcountPL1 += Field._fieldPlayer1[i];
        player1TotalCount.Text = "Общее: " + (Field.kazanPlayer1 + totalcountPL1);
        int totalcountPL2 = 0;
        for (int i = 0; i < Field._fieldPlayer2.Length; i++)
            totalcountPL2 += Field._fieldPlayer2[i];
        player2TotalCount.Text = "Общее: " + (Field.kazanPlayer2 + totalcountPL2);
    }
    static Random rnd = new Random();
    public static int countField = 0; // счет ходов (просто по приколу)
    public static bool tuzdikPlayer1 = false; // Туздык для игрока 1
    public static bool tuzdikPlayer2 = false; // Туздык для игрока 2
    
    public void Click_Button(object sender, EventArgs e)
    {
        Button button = sender as Button;
        int countball = (int)button.Content;
        int indexField = (int)button.Tag;
        bool perehod = false;
        MessageBox.Show("Вы выбрали поле: " + indexField + "\n" + "Шариков в руке: " + button.Content + "\n" + button.Name, namePlayer1, MessageBoxButton.OK, MessageBoxImage.Information);
        if (firstplayer == 1)
        {
            if (Field._fieldPlayer1[indexField] == 1)
            {
                Field._fieldPlayer1[indexField] = 0;
                if (indexField == 8) // если на последнем поле
                {
                    Field._fieldPlayer2[0]++;
                    if (Field._fieldPlayer2[0] % 2 == 0)
                    {
                        Field.kazanPlayer1 += Field._fieldPlayer2[0];
                        Field._fieldPlayer2[0] = 0;
                    }
                }
                else
                {
                    indexField++;
                    Field._fieldPlayer1[indexField]++;
                }
                if (tuzdikPlayer1 && (Field._fieldPlayer2[indexField] == -2 || Field._fieldPlayer2[indexField] == -1) && perehod)
                {
                    Field._fieldPlayer2[indexField] = -2;
                    Field.kazanPlayer1++;
                }
                if (tuzdikPlayer2 && (Field._fieldPlayer1[indexField] == -2 || Field._fieldPlayer1[indexField] == -1) && !perehod)
                {
                    Field._fieldPlayer1[indexField] = -2;
                    Field.kazanPlayer2++;
                }
            }
            else
            {
                Field._fieldPlayer1[indexField] = 0;
                while (true) // пока не потратим все шарики
                {
                    countball--;
                    if (!perehod) // идем по своему полю, и НЕ переходим на другое
                        Field._fieldPlayer1[indexField]++;
                    else
                        Field._fieldPlayer2[indexField]++;
                    if (indexField == 8 && countball > 0)
                    {
                        perehod = !perehod;
                        indexField = 0;
                    }
                    else
                        indexField++;
                    if (countball == 0)
                        break;
                    if (tuzdikPlayer1 && (Field._fieldPlayer2[indexField] == -2 || Field._fieldPlayer2[indexField] == -1) && perehod)
                    {
                        Field._fieldPlayer2[indexField] = -2;
                        Field.kazanPlayer1++;
                    }
                    if (tuzdikPlayer2 && (Field._fieldPlayer1[indexField] == -2 || Field._fieldPlayer1[indexField] == -1) && !perehod)
                    {
                        Field._fieldPlayer1[indexField] = -2;
                        Field.kazanPlayer2++;
                    }
                }
                indexField--;
                if (Field._fieldPlayer2[indexField] % 2 == 0 && perehod) // если остановились на чет клетке 
                {
                    Field.kazanPlayer1 += Field._fieldPlayer2[indexField];
                    Field._fieldPlayer2[indexField] = 0;
                }
                // tuzdik
                if (Field._fieldPlayer2[indexField] == 3 && perehod && !tuzdikPlayer1 && indexField != 9)
                {
                    tuzdikPlayer1 = true;
                    Field._fieldPlayer2[indexField] = -2;
                    MessageBox.Show("На поле " + indexField + " игрока " + namePlayer2 + " появился туздык!");
                }
            }
        }
        if (firstplayer == 2) // PLAYER 2
        {
            if (Field._fieldPlayer2[indexField] == 1)
            {
                Field._fieldPlayer2[indexField] = 0;
                if(indexField == 8) // если на последнем поле
                {
                    Field._fieldPlayer1[0]++;
                    if(Field._fieldPlayer1[0] % 2 == 0)
                    {
                        Field.kazanPlayer2 += Field._fieldPlayer1[0];
                        Field._fieldPlayer1[0] = 0;
                    }
                }
                else
                {
                    indexField++;
                    Field._fieldPlayer2[indexField]++;
                }
                if (tuzdikPlayer2 && (Field._fieldPlayer1[indexField] == -1 || Field._fieldPlayer1[indexField] == -2) && perehod)
                {
                    Field._fieldPlayer1[indexField] = -2;
                    Field.kazanPlayer2++;
                }
                if (tuzdikPlayer1 && (Field._fieldPlayer2[indexField] == -1 || Field._fieldPlayer2[indexField] == -2) && !perehod)
                {
                    Field._fieldPlayer2[indexField] = -2;
                    Field.kazanPlayer1++;
                }
            }
            else
            {
                Field._fieldPlayer2[indexField] = 0;
                while (true) // пока не потратим все шарики
                {
                    countball--;
                    if (!perehod) // идем по своему полю, и НЕ переходим на другое
                    {
                        Field._fieldPlayer2[indexField]++;
                    }
                    else
                    {
                        Field._fieldPlayer1[indexField]++;
                    }
                    if (indexField == 8 && countball > 0)
                    {
                        perehod = !perehod;
                        indexField = 0;
                    }
                    else
                        indexField++;
                    if (countball == 0)
                        break;
                    if (tuzdikPlayer2 && (Field._fieldPlayer1[indexField] == -1 || Field._fieldPlayer1[indexField] == -2)  && perehod)
                    {
                        Field._fieldPlayer1[indexField] = -2;
                        Field.kazanPlayer2++;
                    }
                    if (tuzdikPlayer1 && (Field._fieldPlayer2[indexField] == -1 || Field._fieldPlayer2[indexField] == -2) && !perehod)
                    {
                        Field._fieldPlayer2[indexField] = -2;
                        Field.kazanPlayer1++;
                    }
                }
                indexField--; // ???? HZ NYJEN ILI NET (UPD DYMAU DA NYJEN)
                if (Field._fieldPlayer1[indexField] % 2 == 0 && perehod) // если остановились на чет клетке 
                {
                    Field.kazanPlayer2 += Field._fieldPlayer1[indexField];
                    Field._fieldPlayer1[indexField] = 0;
                }
                // tuzdik
                if (Field._fieldPlayer1[indexField] == 3 && perehod && !tuzdikPlayer2 && indexField != 9)
                {
                    tuzdikPlayer2 = true;
                    Field._fieldPlayer1[indexField] = -2;
                    MessageBox.Show("На поле " + indexField + " игрока " + namePlayer1 + " появился туздык!");
                }
            }
        }
        firstplayer = (firstplayer == 1) ? 2 : 1;
        InitializeGameField(namePlayer1, namePlayer2, firstplayer);
        if (Field.kazanPlayer1 > 81 || Field.kazanPlayer2 > 81)
        {
            WindowEnd.Visibility = Visibility.Visible;
            CheckWin();
        }
    }
    public void CheckWin() 
    {
        string message = null;
        if (Field.kazanPlayer1 > 81)
            message = "Поздравляем игрок(1) - " + namePlayer1 + " победил!";
        if (Field.kazanPlayer2 > 81)
            message = "Поздравляем игрок(2) - " + namePlayer2 + " победил!";

        TextWinner.Text = message;
        ButtonsPL1.IsEnabled = false;
        ButtonsPL2.IsEnabled = false;
        CloseGameInMainWindow.IsEnabled = false;
    }
    public void RestartGame_ButtonClick(object sender, EventArgs e)
    {
        RestartGameFunc();
    }
    public void RestartGameFunc()
    {
        tuzdikPlayer1 = false;
        tuzdikPlayer2 = false;
        Field.kazanPlayer1 = 0;
        Field.kazanPlayer2 = 0;
        Random rnd = new Random();
        firstplayer = rnd.Next(1, 3);
        for (int i = 0; i < Field._fieldPlayer1.Length; i++)
        {
            Field._fieldPlayer1[i] = 9;
            Field._fieldPlayer2[i] = 9;
        }
        InitializeGameField(namePlayer1, namePlayer2, firstplayer);
        WindowEnd.Visibility = Visibility.Collapsed;
        ButtonsPL1.IsEnabled = true;
        ButtonsPL2.IsEnabled = true;
        CloseGameInMainWindow.IsEnabled = true;
    }
    public void GameClose_ButtonClick(object sender, EventArgs e)
    {
        Application.Current.Shutdown(); 
    }
    public void OpenMenu_ButtonClick(object sender, EventArgs e)
    {
        RestartGameFunc();
        StartMenu window = new StartMenu();
        window.Show();
        this.Close();
    }
}