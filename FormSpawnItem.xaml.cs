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

        private void OtherItemSelected(object sender, RoutedEventArgs e)
        {
            var itemType = (string)((ListBoxItem)sender).Content;

            SliderAmount.Value = 0;
            AmountSelector.Visibility = Visibility.Visible;

            switch (itemType)
            {
                case "Juice":
                    SliderAmount.Maximum = 200;
                    break;
                case "Tape":
                    SliderAmount.Maximum = 5;
                    break;
                case "Hamball":
                    AmountSelector.Visibility = Visibility.Collapsed;
                    break;
                case "Banana":
                    SliderAmount.Maximum = 10;
                    break;
                default:
                    break;
            }
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
                    SliderAmount.Maximum = 50;
                    foreach (var ammo in Enum.GetNames<SARAmmo>())
                        ListBoxSelectItem.Items.Add(new ListBoxItem() { Content = ammo });
                    break;
                case "Utility":
                    RaretySelector.Visibility = Visibility.Collapsed;
                    AmountSelector.Visibility = Visibility.Collapsed;
                    foreach (var util in Enum.GetNames<SARUtil>())
                        ListBoxSelectItem.Items.Add(new ListBoxItem() { Content = util });
                    break;
                case "Armor":
                    RaretySelector.Visibility = Visibility.Collapsed;
                    AmountSelector.Visibility = Visibility.Collapsed;
                    ListBoxSelectItem.Items.Add(new ListBoxItem() { Content = "Light Armor (lvl 1)" });
                    ListBoxSelectItem.Items.Add(new ListBoxItem() { Content = "Medium Armor (lvl 2)" });
                    ListBoxSelectItem.Items.Add(new ListBoxItem() { Content = "Heavy Armor (lvl 3)" });
                    break;
                case "Other":
                    RaretySelector.Visibility = Visibility.Collapsed;
                    AmountSelector.Visibility = Visibility.Visible;
                    var lbi = new ListBoxItem() { Content = "Juice" };
                    lbi.Selected += OtherItemSelected;
                    ListBoxSelectItem.Items.Add(lbi);
                    lbi = new ListBoxItem() { Content = "Tape" };
                    lbi.Selected += OtherItemSelected;
                    ListBoxSelectItem.Items.Add(lbi);
                    lbi = new ListBoxItem() { Content = "Banana" };
                    lbi.Selected += OtherItemSelected;
                    ListBoxSelectItem.Items.Add(lbi);
                    lbi = new ListBoxItem() { Content = "Hamball" };
                    lbi.Selected += OtherItemSelected;
                    ListBoxSelectItem.Items.Add(lbi);
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
            else if (category == "Ammo")
                command += "ammo";
            else if (category == "Utility")
                command += "util";
            else if (category == "Armor")
                command += "armor";

            if (ListBoxSelectItem.SelectedIndex == -1)
                return;

            if (category == "Armor")
                command += (ListBoxSelectItem.SelectedIndex + 1).ToString();
            else if (category == "Weapon" || category == "Ammo" || category == "Utility")
                command += ListBoxSelectItem.SelectedIndex.ToString();
            else // Other
            {
                var itemType = (string)((ListBoxItem)ListBoxSelectItem.SelectedItem).Content;
                switch (itemType)
                {
                    case "Juice":
                        command += "juice";
                        break;
                    case "Tape":
                        command += "tape";
                        break;
                    case "Banana":
                        command += "banana";
                        break;
                    case "Hamball":
                        command += "hamball";
                        break;
                    default:
                        return;
                }
            }

            if (RaretySelector.Visibility == Visibility.Visible)
            {
                if (ListBoxRarety.SelectedIndex == -1)
                    return;
                command += " " + ListBoxRarety.SelectedIndex.ToString();
            }
            if (AmountSelector.Visibility == Visibility.Visible)
                if (SliderAmount.Value > 0)
                    command += " " + SliderAmount.Value.ToString();

            Command = command;
            Hide();
        }

        private void SliderAmount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderAmount.Value = (int)e.NewValue;
            if (e.NewValue >= 1)
                LabelAmount.Content = ((int)e.NewValue).ToString();
            else
                LabelAmount.Content = "Default value";
        }
    }
}
