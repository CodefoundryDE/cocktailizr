using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace CocktailizrClient.View.Components
{
    /// <summary>
    /// Interaktionslogik für ExpandableMenuButton.xaml
    /// </summary>
    public partial class ExpandableMenuButton : UserControl
    {
        #region Properties

        public int MenuIsOpen { get; set; }

        #region DependencyProperties

        #region ItemsSource

        /// <summary>
        /// The <see cref="Itemssource" /> dependency property's name.
        /// </summary>
        public const string ItemssourcePropertyName = "Itemssource";

        /// <summary>
        /// Gets or sets the value of the <see cref="Itemssource" />
        /// property. This is a dependency property.
        /// </summary>
        public IEnumerable<KeyValuePair<object,object>> Itemssource
        {
            get
            {
                return (IEnumerable<KeyValuePair<object, object>>)GetValue(ItemssourceProperty);
            }
            set
            {
                SetValue(ItemssourceProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Itemssource" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemssourceProperty = DependencyProperty.Register(
            ItemssourcePropertyName,
            typeof(IEnumerable<KeyValuePair<object, object>>),
            typeof(ExpandableMenuButton),
            null);

        #endregion

        #endregion

        #endregion

        #region Variables

        #endregion

        #region Constructor

        public ExpandableMenuButton()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void MenuButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OpenMenu();
        }

        private void MenuButton_MouseLeave(object sender, MouseEventArgs e)
        {
           CloseMenu();
        }

        private void MenuListBox_MouseEnter(object sender, MouseEventArgs e)
        {
            OpenMenu();
        }

        private void MenuListBox_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseMenu();
        }
        #endregion

        #region Methods

        private void OpenMenu()
        {
            MenuListBox.Visibility = Visibility.Visible;
            MenuListBox.IsTabStop = true;

            MenuIsOpen++;
        }

        private async void CloseMenu()
        {
            MenuIsOpen--;
            await Task.Delay(50);
            if (MenuIsOpen == 0)
            {
                MenuListBox.IsTabStop = false;
                await Task.Delay(300);
                MenuListBox.Visibility = Visibility.Collapsed;
            }
        }

        #endregion




    }
}
