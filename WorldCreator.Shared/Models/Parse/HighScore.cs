using Parse;

namespace WorldCreator.Models.Parse
{
    [ParseClassName("HighScore")]
    public class HighScore : ParseObject
    {
        [ParseFieldName("PlayerName")]
        public string PlayerName { get; set; }

        [ParseFieldName("Points")]
        public int Points { get; set; }

        [ParseFieldName("CombosCount")]
        public int CombosCount { get; set; }

        [ParseFieldName("HighestLevelElement")]
        public int HighestLevelElement { get; set; }

        [ParseFieldName("HighestLevelCleared")]
        public int HighestLevelCleared { get; set; }
    }
}
