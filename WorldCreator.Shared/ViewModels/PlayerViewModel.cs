namespace WorldCreator.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using WorldCreator.Commands;
    using WorldCreator.Data;
    using WorldCreator.Extensions;

    public class PlayerViewModel : BaseViewModel, WorldCreator.ViewModels.IPlayerViewModel
    {
        private const int ScoresPerPage = 10;
        private string name;
        private int points;
        private ICommand getScores;
        private ObservableCollection<AchievmentViewModel> achievments;
        private ObservableCollection<HighScoreViewModel> scores;
        private IScoreManager scoreManager;

        //TO DO edit constructor
        public PlayerViewModel(string name, IEnumerable<AchievmentViewModel> achievments, int points, int combos)
        {
            this.ScorePage = 1;
            this.Name = name;
            this.Achievments = achievments;
            this.Points = points;
            this.CombosCount = combos;
            //TO DO extract to contructor
            this.scoreManager = new ParseConnection();
            this.LoadScores();
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public int Points
        {
            get { return this.points; }
            set
            {
                this.points = value;
                this.OnPropertyChanged("Points");
            }
        }

        public int Place { get; set; }

        public int CombosCount { get; set; }

        public int HighestLevelElement { get; set; }

        public int HighestLevelCleared { get; set; }

        public int ScorePage { get; set; }

        public bool IsNextPageAvailable { get; set; }

        public bool IsPrevPageAvailable { get; set; }

        public ICommand RetrieveScores
        {
            get
            {
                if (this.getScores == null)
                {
                    this.getScores = new RelayCommandWithParameter(this.PerformGetScores);
                }

                return getScores;
            }
        }

        public IEnumerable<HighScoreViewModel> Scores 
        {
            get
            {
                if (this.scores == null)
                {
                    this.scores = new ObservableCollection<HighScoreViewModel>();
                }

                return this.scores;
            }
            set
            {
                if (this.scores == null)
                {
                    this.scores = new ObservableCollection<HighScoreViewModel>();
                }

                this.scores.Clear();
                this.scores.AddRange(value);
            }
        }

        public IEnumerable<AchievmentViewModel> Achievments
        {
            get
            {
                if (this.achievments == null)
                {
                    this.achievments = new ObservableCollection<AchievmentViewModel>();
                }

                return this.achievments;
            }
            set
            {
                if (this.achievments == null)
                {
                    this.achievments = new ObservableCollection<AchievmentViewModel>();
                }

                this.achievments.Clear();
                this.achievments.AddRange(value);
            }
        }

        public void UpdateScore()
        {
            this.scoreManager.UploadScore(this);
        }

        private void PerformGetScores(object obj)
        {
            string direction = obj as string;
            if (direction != null)
            {
                if (direction == "Prev" && this.IsNextPageAvailable)
                {
                    this.ScorePage++;
                }
                else if (direction == "Next" && this.ScorePage - 1 > 0)
                {
                    this.ScorePage--;
                }

                this.LoadScores();
            }
        }

        private async void LoadScores()
        {
            var fetchedScores = await this.scoreManager.GetScoresPage(ScoresPerPage, this.ScorePage);
            var parsedScores = fetchedScores.AsQueryable().Select(HighScoreViewModel.FromHighScore).ToList();
            int startPlace = 1 * this.ScorePage;
            int count = 0;
            foreach (var result in parsedScores)
            {
                result.Place = startPlace;
                startPlace++;
                count++;
            }

            this.SetPageButtonsAvailabiluty(count);
            this.Scores = parsedScores;
        }

        private void SetPageButtonsAvailabiluty(int count)
        {
            if (count < 10)
            {
                this.IsNextPageAvailable = false;
            }
            else
            {
                this.IsNextPageAvailable = false;
            }

            if (this.ScorePage < 2)
            {
                this.IsPrevPageAvailable = false;
            }
            else
            {
                this.IsPrevPageAvailable = true;
            }
        }
    }
}
