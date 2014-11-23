using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using WorldCreator.Models.Parse;

namespace WorldCreator.ViewModels
{
    public class HighScoreViewModel : BaseViewModel
    {
        public static Expression<Func<HighScore, HighScoreViewModel>> FromHighScore
        {
            get
            {
                return h => new HighScoreViewModel()
                {
                    CombosCount = h.CombosCount,
                    HighestLevelCleared = h.HighestLevelCleared,
                    HighestLevelElement = h.HighestLevelElement,
                    Name = h.PlayerName,
                    Points = h.Points
                };
            }
        }
        public string Name { get; set; }

        public int CombosCount { get; set; }

        public int HighestLevelCleared { get; set; }

        public int HighestLevelElement { get; set; }

        public int Place { get; set; }

        public int Points { get; set; }
    }
}
