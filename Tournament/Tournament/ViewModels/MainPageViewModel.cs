namespace Tournament.ViewModels
{
    using MVVM;
    using DataAccessLayer;
    using Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    /// This class is connected to the mainpage.xaml. And defines the different commands within mainpage.xaml.
    /// </summary>
    class MainPageViewModel : ViewModelBase
    {
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

            this.createCommand = new DelegateCommand(this.Create_Executed);
            this.selectCommand = new DelegateCommand(this.Select_Executed);
            this.newCommand = new DelegateCommand(this.New_Executed, this.New_CanExecute);
            this.deleteCommand = new DelegateCommand(this.Delete_Executed, this.Edit_CanExecute);
            this.saveCommand = new DelegateCommand(this.Save_Executed, this.Save_CanExecute);
            this.cancelCommand = new DelegateCommand(this.Cancel_Executed, this.Save_CanExecute);
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
        protected override bool Edit_CanExecute()
        {
            return this.selectedTournament != null && base.Edit_CanExecute();
        }

        protected override void Edit_Executed()
        {
            base.Edit_Executed();
            this.selectedTournament.IsInEditMode = true;
            this.saveCommand.RaiseCanExecuteChanged();
            this.cancelCommand.RaiseCanExecuteChanged();
        }
        private void Cancel_Executed()
        {
            if (this.selectedTournament.Id == 0)
            {
                this.tournaments.Remove(this.selectedTournament);
            }
            else
            {
                // Get old one back from db
                this.selectedTournament.Model = Dal.GetTournamentById(this.selectedTournament.Id);
                this.selectedTournament.IsInEditMode = false;
            }

            this.IsInEditMode = false;
        }

        private void Create_Executed()
        {
            Dal.CreateDatabase();

            // Select. Otherwise the displayed list may be out of sync with the db.
            this.selectCommand.Execute(null);
        }

        private void Delete_Executed()
        {
            // Remove from db
            Dal.DeleteTournament(this.selectedTournament.Model);

            // Remove from list
            this.Tournaments.Remove(this.selectedTournament);
        }

        private bool New_CanExecute()
        {
            return !this.IsInEditMode && this.isDatabaseCreated;
        }

        private void New_Executed()
        {
            this.tournaments.Add(new TournamentViewModel(new Tournament()));
            this.SelectedTournament = this.tournaments.Last();
            this.editCommand.Execute(null);
        }

        private bool Save_CanExecute()
        {
            return this.IsInEditMode;
        }

        private void Save_Executed()
        {
            // Store new one in db
            Dal.SaveTournament(this.selectedTournament.Model);

            // Force a property change notification on the ViewModel:
            this.selectedTournament.Model = this.selectedTournament.Model;

            // Leave edit mode
            this.IsInEditMode = false;
            this.selectedTournament.IsInEditMode = false;
        }

        private void Select_Executed()
        {
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