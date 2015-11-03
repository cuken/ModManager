using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ModManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameCollection Games = new GameCollection();

        //Use this for Release
        //string AppDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        //string MMGameDir = System.AppDomain.CurrentDomian.BaseDirectory + "\\ModManagerGames";

        //DEBUG
        string AppDirectory = @"D:\Testing\ModManager";
        string MMGameDir = @"D:\Testing\ModManager\ModManagerGames";

        public MainWindow()
        {
            InitializeComponent();

            //Make sure Games Directory Exists...
            if (!Directory.Exists(MMGameDir))
            {
                try
                {
                    Directory.CreateDirectory(MMGameDir);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("ModManager needs to create a \"Games\" Directory where the exe lives!, please move the exe into a directory you have write permissions", "Unable to create directory!",MessageBoxButton.OK);
                    Application.Current.Shutdown();
                }
            }

            foreach (String f in Directory.GetFiles(MMGameDir))
            {
                Game g = new Game(f.Substring(f.LastIndexOf('\\') + 1));
                Games.AddGameToColleciton(g);
                lb_Games.Items.Add(g.Name);
            }
        }

        private void MenuItemOpen_Click(object sender, EventArgs e)
        {

        }

        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ManageGame manageGame = new ManageGame(Games.ReturnGameByName(lb_Games.SelectedValue.ToString()));
            manageGame.Show();
        }

        private void btn_Game_Click(object sender, RoutedEventArgs e)
        {
            string gameName = string.Empty;
            string gameDir = string.Empty;            
            AddGame addGame = new AddGame();
            addGame.GameDir += value => gameDir = value;
            addGame.GameName += value => gameName = value;
            addGame.ShowDialog();

            if(gameName == "")
            {
                //User canceled the Add Game Dialog
            }
            else
            {
                //Else add the Game
                AddGame(gameName, gameDir);
            }

        }

        public void AddGame(string gameName, string gameDir)
        {
            if(Games.ReturnGameByName(gameName) == null)
            {
                Game game = new Game(gameName);
                Games.AddGameToColleciton(game);
                lb_Games.Items.Add(gameName);
                IniFile gameINI = new IniFile();
                gameINI.Section("General").Set("Name", gameName);
                gameINI.Section("General").Set("BaseDirectory", gameDir);
                gameINI.Save(MMGameDir + "\\" + gameName);
            }       
        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    Games.RemoveGameFromCollection(Games.ReturnGameByName(lb_Games.SelectedValue.ToString()));
                    File.Delete(MMGameDir + "\\" + lb_Games.SelectedValue.ToString());
                    lb_Games.Items.Remove(lb_Games.SelectedItem);
                }
                catch (Exception ex)
                {
                }
            }            
        }
    }
}
