namespace Tournament
{
    using MVVM;
    using Models;
    using Helpers;
    using System;
    using System.Windows.Input;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Media;
	using System.Collections.ObjectModel;

    /// <summary>
    /// This class is used for binding the user input in the mainpage.xaml. When binding is TwoWay both the
    /// getter and setter are used. Else only the getter is used.
    /// </summary>
    public class TournamentViewModel : ViewModelBase
    {
        #region Fields
        /// <summary>
        /// Fields.
        /// </summary>
        private Tournament model;
        private ImageSource picture = null;
        private DelegateCommand uploadImageCommand;
		private ObservableCollection<PlayersFixture> _playersFixtures;
		private ObservableCollection<Fixture> _fixtures;
		private ObservableCollection<Player> _players;
        #endregion

        #region Constructor
        /// <summary>
        /// Class constructor.
        /// </summary>
        public TournamentViewModel(Tournament model)
        {
            this.model = model;
            this.uploadImageCommand = new DelegateCommand(this.UploadImage_Executed);


			_playersFixtures = new ObservableCollection<PlayersFixture>();
			_fixtures = new ObservableCollection<Fixture>();
			_players = new ObservableCollection<Player>();
        }
        #endregion

        #region Model values
        /// <summary>
        /// Model value description.
        /// </summary>
        public string Description
        {
            get
            {
                if (this.model == null)
                {
                    return string.Empty;
                }

                return this.model.Description;
            }

            set
            {
                if (this.model != null)
                {
                    this.model.Description = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Model value type.
        /// </summary>
        public string Type
        {
            get
            {
                if (this.model == null)
                {
                    return string.Empty;
                }

                return this.model.Type;
            }

            set
            {
                if (this.model != null)
                {
                    this.model.Type = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Model value Id.
        /// </summary>
        public int Id
        {
            get
            {
                if (this.model == null)
                {
                    return 0;
                }

                return this.model.TournamentId;
            }

            set
            {
                if (this.model != null)
                {
                    this.model.TournamentId = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Tournament Model
        {
            get
            {
                return this.model;
            }

            set
            {
                this.SetProperty(ref this.model, value);
                this.picture = null;
                this.OnPropertyChanged(string.Empty);
            }
        }

		/// <summary>
		/// Player collection.
		/// </summary>
		public ObservableCollection<PlayersFixture> PlayersFixtures
		{
			get
			{
				return this._playersFixtures;
			}

			set
			{
				this.SetProperty(ref this._playersFixtures, value);
			}
		}

		/// <summary>
		/// Fixture collection.
		/// </summary>
		public ObservableCollection<Fixture> Fixtures
		{
			get
			{
				return this._fixtures;
			}

			set
			{
				this.SetProperty(ref _fixtures, value);
			}
		}

		/// <summary>
		/// Player collection.
		/// </summary>
		public ObservableCollection<Player> Players
		{
			get
			{
				return this._players;
			}

			set
			{
				this.SetProperty(ref _players, value);
			}
		}

        /// <summary>
        /// Model value Name.
        /// </summary>
        public string Name
        {
            get
            {
                if (this.model == null)
                {
                    return string.Empty;
                }

                return this.model.Name;
            }

            set
            {
                if (this.model != null)
                {
                    this.model.Name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Use the ByteArrayBitmapExtensions class to get the picture as a bitmap.
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                if (this.model != null && this.picture == null)
                {
                    this.picture = this.model.Picture.AsBitmapImage();
                }

                return this.picture;
            }
        }


        /// <summary>
        /// Model value Picture.
        /// </summary>
        public byte[] Picture
        {
            set
            {
                this.picture = null;
                this.model.Picture = value;
                this.OnPropertyChanged("ImageSource");
            }
        }

        /// <summary>
        /// Model value status.
        /// </summary>
        public Status Status
        {
            get
            {
                if (this.model == null)
                {
                    return Status.Playing;
                }

                return (Status)this.model.Status;
            }

            set
            {
                if (this.model != null)
                {
                    this.model.Status = (int)value;
                }
            }
        }

        /// <summary>
        /// StatusString for parsing Status value.
        /// </summary>
        public string StatusString
        {
            get { return this.Status.ToString(); }
            set { this.Status = (Status)System.Enum.Parse(typeof(Status), value); }
        }

        /// <summary>
        /// Asyncronous method for uploading an image.
        /// </summary>
        private async void UploadImage_Executed()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile imgFile = await openPicker.PickSingleFileAsync();
            if (imgFile != null)
            {
                this.Picture = await imgFile.AsByteArray();
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Delegate command for uploading an image.
        /// </summary>
        public ICommand UploadImageCommand
        {
            get { return this.uploadImageCommand; }
        }
        #endregion
    }
}