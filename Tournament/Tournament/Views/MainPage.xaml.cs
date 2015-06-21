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
    public sealed partial class MainPage : ViewBase
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

		//// Makes the combobox visible if the radiobutton for knockout is checked.
		//private void Knockout_Checked(object sender, RoutedEventArgs e)
		//{
		//	Players.Visibility = Windows.UI.Xaml.Visibility.Visible;
		//}

		//// When the save button is pressed, uncheck both the knockout and leaque radiobuttons.
		//private void AppBarButton_Click(object sender, RoutedEventArgs e)
		//{
		//	Knockout.IsChecked = false;
		//	Leaque.IsChecked = false;
		//}

		//// When the cancel button is pressed, uncheck both the knockout and leaque radiobuttons.
		//private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
		//{
		//	Knockout.IsChecked = false;
		//	Leaque.IsChecked = false;
		//}

        // Makes the textbox for entering a number of players visible if the radiobutton for leaque
        // is checked.
		//private void Leaque_Checked(object sender, RoutedEventArgs e)
		//{
		//	Numbers.Visibility = Windows.UI.Xaml.Visibility.Visible;
		//}

		//// Collapses the combobox if the radiobutton for knockout is unchecked.
		//private void Knockout_Unchecked(object sender, RoutedEventArgs e)
		//{
		//	Players.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
		//}

		//// Collapses the textbox for entering a number of players
		//// if the radiobutton for leaque is unchecked.
		//private void Leaque_Unchecked(object sender, RoutedEventArgs e)
		//{
		//	Numbers.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
		//}

		//// If a string or a zero is entered in the textbox for entering a number of players,
		//// set the text to null.
		//private void Numbers_Changed(object sender, TextChangedEventArgs e)
		//{
		//	foreach(var ch in Numbers.Text)
		//	{
		//		if(! Char.IsNumber(ch))
		//		{
		//			Numbers.Text = "";
		//		}
		//		else if(Char.IsNumber(ch))
		//		{
		//			if (int.Parse(ch.ToString()) < 1)
		//			{
		//				Numbers.Text = "";
		//			}
		//		}
		//	}
		//}
    }
}
