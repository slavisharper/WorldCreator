namespace WorldCreator.ViewModels
{
    using System.Collections.Generic;
    using WorldCreator.Extensions;

    public class PlayerViewModel : BaseViewModel
    {
        private IList<AchievmentViewModel> achievments;

        public PlayerViewModel()
            :this("Unnamed")
        {
        }

        public PlayerViewModel(string name)
            :this(name, null)
        {
        }

        public PlayerViewModel(string name, IEnumerable<AchievmentViewModel> achievments)
        {
            this.Name = name;
            this.Achievments = achievments;
        }

        public PlayerViewModel(string name, IEnumerable<AchievmentViewModel> achievments, int points, int combos)
        {
            this.Name = name;
            this.Achievments = achievments;
            this.Points = points;
            this.CombosCount = combos;
        }

        public string Name { get; set; }

        public int Points { get; set; }

        public int CombosCount { get; set; }

        public int HighestLevelElement { get; set; }

        public int HighestLevelCleared { get; set; }

        public IEnumerable<AchievmentViewModel> Achievments
        {
            get { return this.achievments; }
            set
            {
                if (this.achievments == null)
                {
                    this.achievments = new List<AchievmentViewModel>();
                }

                this.achievments.Clear();
                this.achievments.AddRange(value);
            }
        }
    }
}
