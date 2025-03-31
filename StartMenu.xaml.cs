using System.Windows;
namespace togizkumalakUI;
public partial class StartMenu : Window
{
    public StartMenu()
    {
        InitializeComponent();
    }
    private void Button_Click_Start(object sender, RoutedEventArgs e)
    {
        Random rnd = new Random();
        string InputnamePlayer2 = Player2NameTextBox.Text;
        string InputnamePlayer1 = Player1NameTextBox.Text;
        if (InputnamePlayer1.Length < 2 || InputnamePlayer2.Length < 2)
            MessageBox.Show("У игрока " + ((InputnamePlayer1.Length < 2) ? "1" : "2") + " некорректное имя");
        else
        {
            int motionPlayer = rnd.Next(1, 3);
            MainWindow Window = new MainWindow(InputnamePlayer1, InputnamePlayer2, motionPlayer);
            Window.Show();
            this.Close();
        }
    }
    private void Button_Click_Rules_Open(object sender, RoutedEventArgs e)
    {
        WindowRules.Visibility = Visibility.Visible;
    }    
    private void Button_Click_Rules_Close(object sender, RoutedEventArgs e)
    {
        WindowRules.Visibility = Visibility.Collapsed;
    }
    public void GameClose_ButtonClick(object sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }
}

