namespace Tournament
{
    using MVVM;
    using DataAccessLayer;
    using Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Helpers;
	using System;
	using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// This class is connected to the mainpage.xaml. And defines the different commands within mainpage.xaml.
    /// It uses ICommands to perform actions.
    /// </summary>
    public class OverviewViewModel : ViewModelBase
	{
		#region Fields
		// Declare delegate commands (Command linked from within the XAML view)
        private DelegateCommand cancelCommand;
        private DelegateCommand createCommand;
        private DelegateCommand deleteCommand;
		private DelegateCommand saveCommand;
		private DelegateCommand showScheduleCommand;
		private DelegateCommand addPlayersCommand;
		private DelegateCommand newCommand;
		private DelegateCommand editCommand;

		// View state booleans
        private bool hasSelection = false;
		private bool isDatabaseCreated = false;
		private bool isNewCreated = false;
		private bool isInEditMode = false;

		// Tournament collections and selectedTournament
        private ObservableCollection<TournamentViewModel> tournaments = new ObservableCollection<TournamentViewModel>();
        private TournamentViewModel selectedTournament = null;
		#endregion

		/// <summary>
		/// OverviewModel constructor
		/// </summary>
        public OverviewViewModel()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            // Initiate the delegate commands.
            this.createCommand = new DelegateCommand(this.Create_Executed);
			this.showScheduleCommand = new DelegateCommand(this.ShowSchedule, this.ShowShedule_CanExecute);
            this.newCommand = new DelegateCommand(this.New_Executed, this.New_CanExecute);
            this.deleteCommand = new DelegateCommand(this.Delete_Executed, this.TournamentSelected);
            this.saveCommand = new DelegateCommand(this.Save_Executed, this.Save_CanExecute);
            this.cancelCommand = new DelegateCommand(this.Cancel_Executed, this.Save_CanExecute);
			this.addPlayersCommand = new DelegateCommand(this.AddPlayers_Executed);
			this.editCommand = new DelegateCommand(this.Edit_Executed, this.TournamentSelected);

			// Create database
			Dal.CreateDatabase();
        }

		// All the commands getters 
		#region Commands getters

		public ICommand EditCommand
		{
			get { return this.editCommand; }
		}

		public ICommand ShowScheduleCommand
		{
			get { return this.showScheduleCommand; }
		}

		public ICommand CancelCommand
		{
			get { return this.cancelCommand; }
		}

		public ICommand CreateCommand
		{
			get { return this.createCommand; }
		}

		public ICommand DeleteCommand
		{
			get { return this.deleteCommand; }
		}

		public ICommand NewCommand
		{
			get { return this.newCommand; }
		}

		public ICommand SaveCommand
		{
			get { return this.saveCommand; }
		}

		public ICommand AddPlayersCommand
		{
			get { return this.addPlayersCommand; }
		}

		
		#endregion

		// All the states and can execute methods (all return true/false)
		#region Command States

		public bool IsNewCreated
		{
			get { return isNewCreated; }
			private set { this.SetProperty(ref this.isNewCreated, value); }
		}

		// item selected
		public bool HasSelection
		{
			get { return this.hasSelection; }
			private set { this.SetProperty(ref this.hasSelection, value); }
		}

		// True if a tournament is selected.
		protected bool Edit_CanExecute()
		{
			return this.selectedTournament != null;
		}

		// True while in editmode and if a db exists.
		private bool New_CanExecute()
		{
			return !this.IsInEditMode && this.isDatabaseCreated;
		}

		// True while in editmode. For saving a tournament.
		private bool Save_CanExecute()
		{
			return this.IsInEditMode;
		}

		public bool IsInDesignMode
		{
			get { return Windows.ApplicationModel.DesignMode.DesignModeEnabled; }
		}

		public bool IsInEditMode
		{
			get
			{
				return this.isInEditMode;
			}

			set
			{
				this.SetProperty(ref this.isInEditMode, value);
				this.editCommand.RaiseCanExecuteChanged();
			}
		}

		private bool ShowShedule_CanExecute()
		{
			return TournamentSelected() && !IsNewCreated;
		}

		private bool TournamentSelected()
		{
			return selectedTournament != null;
		}

		#endregion

		// All the methods
		#region Methods

		// Navigate to Matches view and send TournamentViewModel
		private void ShowSchedule()
		{
			NavigationService.Navigate("Matches", SelectedTournament);
		}

		// Editmode is true. And designmode is false.
		protected void Edit_Executed()
		{
			this.IsInEditMode = true; ;
			this.saveCommand.RaiseCanExecuteChanged();
			this.cancelCommand.RaiseCanExecuteChanged();
		}

		// Cancel the changes made to the selected tournament.
		private void Cancel_Executed()
		{
			if (this.selectedTournament.Id == 0)
			{
				this.tournaments.Remove(this.selectedTournament);
			}
			else
			{
				// Get old one back from db.
				this.selectedTournament.Model = Dal.GetTournamentById(this.selectedTournament.Id);
			}

			this.IsInEditMode = false;
			this.IsNewCreated = false;
		}

		// This method is not used. The db is now created when the select button is pressed.
		private void Create_Executed()
		{
			// Create a db.
			Dal.CreateDatabase();

			// Select. Otherwise the displayed list may be out of sync with the db.
			Select_Executed();
		}

		// Delete a tournament from the db.
		private void Delete_Executed()
		{
			// Remove from db.
			Dal.DeleteTournament(this.selectedTournament.Model);

			// Remove from list.
			this.Tournaments.Remove(this.selectedTournament);
		}

		// New empty tournament is created. And is then displayed as the selected tournament.
		private void New_Executed()
		{
			this.tournaments.Add(new TournamentViewModel(new Tournament()));
			this.SelectedTournament = this.tournaments.Last();
			this.IsNewCreated = true;
			this.editCommand.Execute(null);
		}

		// Save the selected tournament in the db. Binding is with the save button in mainpage.xaml.
		private void Save_Executed()
		{
			// Store new one in db.
			Dal.SaveTournament(this.selectedTournament.Model);

			this.selectedTournament.Model = this.selectedTournament.Model;

			// Leave edit mode.
			this.IsInEditMode = false;
			this.IsNewCreated = false;
		}

		// Select all tournaments from the db. Binding is with the select button in mainpage.xaml.
		private void Select_Executed()
		{
			// Also create a new db. Because the new button in mainpage.xaml is not used.
			// Dal.CreateDatabase();

			List<Tournament> models = Dal.GetAllTournaments();

			this.tournaments.Clear();
			foreach (var m in models)
			{
				this.tournaments.Add(new TournamentViewModel(m));
			}

			this.isDatabaseCreated = true;
			this.newCommand.RaiseCanExecuteChanged();
		}

		/// <summary>
		/// Move to AddPlayer view
		/// </summary>
		private void AddPlayers_Executed()
		{
			// Store new one in db.
			Dal.SaveTournament(this.selectedTournament.Model);

			this.selectedTournament.Model = this.selectedTournament.Model;

			// navigate to AddPlayers page!
			NavigationService.Navigate("AddPlayers", SelectedTournament);
		}

		#endregion

		// Data access for the View
		#region ViewModel Data properties
		public ObservableCollection<TournamentViewModel> Tournaments
        {
            get { return this.tournaments; }
            set { this.SetProperty(ref this.tournaments, value); }
        }

        public TournamentViewModel SelectedTournament
        {
            get { return this.selectedTournament; }
            set
            {
                this.SetProperty(ref this.selectedTournament, value);
                this.HasSelection = this.selectedTournament != null;
				// change execute state of the following commands
                this.deleteCommand.RaiseCanExecuteChanged();
				this.editCommand.RaiseCanExecuteChanged();
				this.showScheduleCommand.RaiseCanExecuteChanged();
            }
        }
		#endregion


		/// <summary>
		/// Override the OnNavigatedTo from the ViewModelBase
		/// Execute function to load the Tournaments
		/// </summary>
		/// <param name="navigationEvent"></param>
		public override void OnNavigatedTo(NavigationEventArgs navigationEvent)
		{
			// get tournaments
			Select_Executed();
		}
    }
}