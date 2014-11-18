namespace WorldCreator.ViewModels
{
    public class AchievmentViewModel : BaseViewModel
    {
        public AchievmentViewModel()
            :this("", "", 0)
        {
        }

        public AchievmentViewModel(string name, string description, int bonus)
        {
            this.Name = name;
            this.Description = description;
            this.BonusPoints = bonus;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BonusPoints { get; set; }
    }
}
