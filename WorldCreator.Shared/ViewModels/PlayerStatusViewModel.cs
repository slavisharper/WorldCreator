namespace WorldCreator.ViewModels
{
    public class PlayerStateViewModel : BaseViewModel
    {
        public PlayerStateViewModel()
        {
        }

        public int Place { get; set; }

        public int Points { get; set; }

        public int CombosCount { get; set; }

        public int HighestLevelElement { get; set; }

        public int HighestLevelCleared { get; set; }

    }
}
