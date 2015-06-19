namespace Tournament.ViewModels
{
    using MVVM;
    using DataAccessLayer;
    using Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Helpers;

    /// <summary>
    /// This class is connected to the mainpage.xaml. And defines the different commands within mainpage.xaml.
    /// It uses ICommands to perform actions.
    /// </summary>
    class MainPageViewModel : ViewModelBase
    {
        // Declare delegate commands.
        private DelegateCommand cancelCommand;
        private DelegateCommand createCommand;
        private DelegateCommand deleteCommand;
        private bool hasSelection = false;
        private DelegateCommand newCommand;
        private ObservableCollection<TournamentViewModel> tournaments = new ObservableCollection<TournamentViewModel>();
        private DelegateCommand saveCommand;
        private DelegateCommand selectCommand;
        private TournamentViewModel selectedTournament = null;
        private bool isDatabaseCreated = false;
        public MainPageViewModel()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            // Initiate the delegate commands.
            this.createCommand = new DelegateCommand(this.Create_Executed);
            this.selectCommand = new DelegateCommand(this.Select_Executed);
            this.newCommand = new DelegateCommand(this.New_Executed, this.New_CanExecute);
            this.deleteCommand = new DelegateCommand(this.Delete_Executed, this.Edit_CanExecute);
            this.saveCommand = new DelegateCommand(this.Save_Executed, this.Save_CanExecute);
            this.cancelCommand = new DelegateCommand(this.Cancel_Executed, this.Save_CanExecute);
        }

        /// <summary>
        /// The following commands are bindings to the buttons in mainpage.xaml.
        /// </summary>
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

        public bool HasSelection
        {
            get { return this.hasSelection; }
            private set { this.SetProperty(ref this.hasSelection, value); }
        }

        public ICommand NewCommand
        {
            get { return this.newCommand; }
        }

        public ObservableCollection<TournamentViewModel> Tournaments
        {
            get { return this.tournaments; }
            set { this.SetProperty(ref this.tournaments, value); }
        }

        public ICommand SaveCommand
        {
            get { return this.saveCommand; }
        }

        public ICommand SelectCommand
        {
            get { return this.selectCommand; }
        }
        public TournamentViewModel SelectedTournament
        {
            get { return this.selectedTournament; }
            set
            {
                this.SetProperty(ref this.selectedTournament, value);
                this.HasSelection = this.selectedTournament != null;
                this.deleteCommand.RaiseCanExecuteChanged();
                this.editCommand.RaiseCanExecuteChanged();
            }
        }

        // True if a tournament is selected.
        protected override bool Edit_CanExecute()
        {
            return this.selectedTournament != null && base.Edit_CanExecute();
        }

        // Editmode is true. And designmode is false.
        protected override void Edit_Executed()
        {
            base.Edit_Executed();
            this.selectedTournament.IsInEditMode = true;
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
                this.selectedTournament.IsInEditMode = false;
            }

            this.IsInEditMode = false;
        }

        // This method is not used. The db is now created when the select button is pressed.
        private void Create_Executed()
        {
            // Create a db.
            Dal.CreateDatabase();

            // Select. Otherwise the displayed list may be out of sync with the db.
            this.selectCommand.Execute(null);
        }

        // Delete a tournament from the db.
        private void Delete_Executed()
        {
            // Remove from db.
            Dal.DeleteTournament(this.selectedTournament.Model);

            // Remove from list.
            this.Tournaments.Remove(this.selectedTournament);
        }

        // True while in editmode and if a db exists.
        private bool New_CanExecute()
        {
            return !this.IsInEditMode && this.isDatabaseCreated;
        }

        // New empty tournament is created. And is then displayed as the selected tournament.
        private void New_Executed()
        {
            this.tournaments.Add(new TournamentViewModel(new Tournament()));
            this.SelectedTournament = this.tournaments.Last();
            this.editCommand.Execute(null);
        }

        // True while in editmode. For saving a tournament.
        private bool Save_CanExecute()
        {
            return this.IsInEditMode;
        }

        // Save the selected tournament in the db. Binding is with the save button in mainpage.xaml.
        private void Save_Executed()
        {
            // Store new one in db.
            Dal.SaveTournament(this.selectedTournament.Model);

            this.selectedTournament.Model = this.selectedTournament.Model;

            // Leave edit mode.
            this.IsInEditMode = false;
            this.selectedTournament.IsInEditMode = false;
        }

        // Select all tournaments from the db. Binding is with the select button in mainpage.xaml.
        private void Select_Executed()
        {
            // Also create a new db. Because the new button in mainpage.xaml is not used.
            Dal.CreateDatabase();

            List<Tournament> models = Dal.GetAllTournaments();

            this.tournaments.Clear();
            foreach (var m in models)
            {
                this.tournaments.Add(new TournamentViewModel(m));
            }

            this.isDatabaseCreated = true;
            this.newCommand.RaiseCanExecuteChanged();
        }
    }
}