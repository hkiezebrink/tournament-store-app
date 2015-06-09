using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Tournament
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Knockout_Checked(object sender, RoutedEventArgs e)
        {
            Players.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Knockout.IsChecked = false;
            Leaque.IsChecked = false;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Knockout.IsChecked = false;
            Leaque.IsChecked = false;
        }

        private void Leaque_Checked(object sender, RoutedEventArgs e)
        {
            Numbers.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void Knockout_Unchecked(object sender, RoutedEventArgs e)
        {
            Players.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void Leaque_Unchecked(object sender, RoutedEventArgs e)
        {
            Numbers.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Fixtures.IsEnabled = true;
        }

        private void Fixtures_Page(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Views.Fixtures));
        }

        private void Numbers_Changed(object sender, TextChangedEventArgs e)
        {
            foreach(var ch in Numbers.Text)
            {
                if(! Char.IsNumber(ch))
                {
                    Numbers.Text = "";
                }
            }
            if (Numbers.Text.Length > 2)
            {
                Numbers.Text = "";
            }
        }
    }
}
