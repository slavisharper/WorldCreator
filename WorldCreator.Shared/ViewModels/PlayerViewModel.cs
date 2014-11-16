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
            :this(name, "")
        {
        }

        public PlayerViewModel(string name, string nationality)
            :this(name, nationality, new List<AchievmentViewModel>())
        {
        }

        public PlayerViewModel(string name, string nationality, IEnumerable<AchievmentViewModel> achievments)
            :this(name, nationality, achievments, null)
        {
        }

        public PlayerViewModel(string name, string nationality, IEnumerable<AchievmentViewModel> achievments, PlayerStateViewModel state)
        {
            this.Name = name;
            this.Nationality = nationality;
            this.Achievments = achievments;
            this.State = state;
        }

        public string Name { get; set; }

        public string Nationality { get; set; }

        public PlayerStateViewModel State { get; set; }

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
