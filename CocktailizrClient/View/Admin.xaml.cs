using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Helpers;
using UserControl = System.Windows.Controls.UserControl;

namespace CocktailizrClient.View
{
    /// <summary>
    /// Interaktionslogik für Admin.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string path;

            if (OpenFile("Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png", out path))
            {
                ImagePathTextBox.Text = path;

            }
        }


        public static bool OpenFile(string filter, out string filePath)
        {
            filePath = string.Empty;
            using (var dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.Filter = filter;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    return true;
                }
            }
            return false;

        }
    }
}
