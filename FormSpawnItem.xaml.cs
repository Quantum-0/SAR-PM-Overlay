using SAR_Overlay.Enums;
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

namespace SAR_Overlay
{
    /// <summary>
    /// Логика взаимодействия для FormSpawnItem.xaml
    /// </summary>
    public partial class FormSpawnItem : Window
    {
        public string Command;

        public FormSpawnItem()
        {
            InitializeComponent();
        }

        private void CategorySelected(object sender, RoutedEventArgs e)
        {
            var category = (string)((ListBoxItem)sender).Content;

            ListBoxSelectItem.Items.Clear();
            switch (category)
            {
                case "Weapon":
                    RaretySelector.Visibility = Visibility.Visible;
                    AmountSelector.Visibility = Visibility.Collapsed;
                    foreach (var weapon in Enum.GetNames<SARWeapon>())
                        ListBoxSelectItem.Items.Add(new ListBoxItem() { Content = weapon });
                    break;
                case "Ammo":
                    RaretySelector.Visibility = Visibility.Collapsed;
                    AmountSelector.Visibility = Visibility.Visible;
                    foreach (var ammo in Enum.GetNames<SARAmmo>())
                        ListBoxSelectItem.Items.Add(new ListBoxItem() { Content = ammo });
                    break;
                default:
                    break;
            }
        }

        private void ButtonSpawn_Click(object sender, RoutedEventArgs e)
        {
            // Spawn
            if (ListBoxCategory.SelectedItem == null)
                return;

            var category = (string)((ListBoxItem)ListBoxCategory.SelectedItem).Content;
            var command = "/";
            if (category == "Weapon")
                command += "gun";

            if (ListBoxSelectItem.SelectedIndex == -1)
                return;

            command += ListBoxSelectItem.SelectedIndex.ToString();

            if (ListBoxRarety.SelectedIndex == -1)
                return;

            command += " " + ListBoxRarety.SelectedIndex.ToString();
            Command = command;
            Close();
        }
    }
}
