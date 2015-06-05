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

namespace App3
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
            bob1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //OrganizerStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            bob2.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //InviteeStackPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            bob3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            bob4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            bob5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            bob6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        /// <summary>
        /// Organizer and Invitee properties are mutually exclusive.
        /// This radio button enables the invitees properties while disabling the organizer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void League_Checked(object sender, RoutedEventArgs e)
        {
            bob3.Visibility = Windows.UI.Xaml.Visibility.Visible;
            bob4.Visibility = Windows.UI.Xaml.Visibility.Visible;
            bob5.Visibility = Windows.UI.Xaml.Visibility.Visible;
            bob6.Visibility = Windows.UI.Xaml.Visibility.Visible;
            bob1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            bob2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }



    }
}
