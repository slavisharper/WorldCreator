using Parse;

namespace WorldCreator.Models.Parse
{
    [ParseClassName("HighScore")]
    public class HighScore : ParseObject
    {
        public HighScore()
        {
        }

        [ParseFieldName("playerName")]
        public string PlayerName 
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); } 
        }

        [ParseFieldName("points")]
        public int Points 
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("combosCount")]
        public int CombosCount
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); } 
        }

        [ParseFieldName("highestLevelElement")]
        public int HighestLevelElement
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("highestLevelCleared")]
        public int HighestLevelCleared
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
    }
}
