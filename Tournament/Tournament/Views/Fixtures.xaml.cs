using Tournament.Common;
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
using Windows.Storage;
using SQLite;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Tournament.Views
{
    using DataAccessLayer;
    using Models;

    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Fixtures : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public Fixtures()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        /// <summary>
        /// Get all tournaments from the database. And display the number of players of the entered
        /// tournament. Or display an exception if the tournament does not exsist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Get_Fixtures(object sender, RoutedEventArgs e)
        {
            List<Tournament> models = Dal.GetAllTournaments();

            int i = -1;
            string name = TournamentName.Text.ToString();

            foreach(var j in models)
            {
                if (j.Name == name)
                {
                    i = models.IndexOf(j);
                }
            }

            try
            {
                if (i != -1)
                {
                    Players.Text = models[i].Players.ToString();
                }
                else
                {
                    Players.Text = "No tournament was found.";
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                Players.Text = "No tournament was found.";
            }
        }

        // When focused, set the text to null if it is default.
        private void TournamentName_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TournamentName.Text == "Tournament name")
            {
                TournamentName.Text = "";
            }
        }

        // When focused, set the text to null if it is default.
        private void Players_GotFocus(object sender, RoutedEventArgs e)
        {
            if((Players.Text == "Number of players") || Players.Text == "No tournament was found.")
            {
                Players.Text = "";
            }
        }

        // When unfocused, set default text.
        private void TournamentName_LostFocus(object sender, RoutedEventArgs e)
        {
            if(TournamentName.Text == "")
            {
                TournamentName.Text = "Tournament name";
            }
        }
    }
}
