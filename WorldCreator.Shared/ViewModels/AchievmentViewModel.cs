namespace WorldCreator.ViewModels
{
    public class AchievmentViewModel : BaseViewModel
    {
        public AchievmentViewModel()
            :this("", "")
        {
        }

        public AchievmentViewModel(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
