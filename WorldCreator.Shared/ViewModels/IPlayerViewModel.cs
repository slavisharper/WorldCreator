namespace WorldCreator.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public interface IPlayerViewModel
    {

        int CombosCount { get; set; }

        int HighestLevelCleared { get; set; }

        int HighestLevelElement { get; set; }

        string Name { get; set; }

        int Place { get; set; }

        int Points { get; set; }

        ICommand RetrieveScores { get; }

        int ScorePage { get; set; }

        bool IsNextPageAvailable { get; set; }

        bool IsPrevPageAvailable { get; set; }

        IEnumerable<HighScoreViewModel> Scores { get; set; }

        IEnumerable<AchievmentViewModel> Achievments { get; set; }

        void UpdateScore();

        void LoadScores();
    }
}
