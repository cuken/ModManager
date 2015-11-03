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

namespace ModManager
{
    /// <summary>
    /// Interaction logic for ManageGame.xaml
    /// </summary>
    public partial class ManageGame : Window
    {
        public ManageGame(Game game)
        {
            InitializeComponent();
            this.Title = game.Name;
            lbl_GameDir.Content = game.IniPath;
        }

    }
}
