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
using System.Windows.Shapes;
using System.IO;

namespace ModManager
{
    /// <summary>
    /// Interaction logic for AddGame.xaml
    /// </summary>
    public partial class AddGame : Window
    {
        public event Action<string> GameName;
        public event Action<string> GameDir;

        public AddGame()
        {
            InitializeComponent();
            tb_GameName.Focus();
            
        }

        private void btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                tb_GameDir.Text = dialog.SelectedPath;
            }            
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(tb_GameDir.Text))
            {
                lbl_Info.Content = "Invalid directory provided!";
            }
            else
            {
                if (tb_GameName.Text != "" && tb_GameDir.Text != "")
                {
                    GameName(tb_GameName.Text);
                    GameDir(tb_GameDir.Text);
                    this.Close();
                }
                else
                {
                    lbl_Info.Content = "You must provide a value for both fields!";
                }
            }            
        }

        private void tb_GameName_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_Info.Content = "Provide a name for the game.";
        }

        private void tb_GameDir_GotFocus(object sender, RoutedEventArgs e)
        {
            lbl_Info.Content = "The parent directory for the game.";
        }
    }
}
