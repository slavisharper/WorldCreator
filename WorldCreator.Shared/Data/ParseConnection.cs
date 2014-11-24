namespace WorldCreator.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Parse;

    using WorldCreator.Models.Parse;
    using WorldCreator.ViewModels;
    using System.Threading.Tasks;

    public class ParseConnection : IScoreManager
    {
        public async Task UploadScore(IPlayerViewModel player)
        {
            var score = new HighScore();
            score.CombosCount = player.CombosCount;
            score.HighestLevelCleared = player.HighestLevelCleared;
            score.HighestLevelElement = player.HighestLevelElement;
            score.PlayerName = player.Name;
            score.Points = player.Points;
            await this.UploadScore(score);
        }

        public async Task UploadScore(HighScore score)
        {
            var scoreQuery = new ParseQuery<HighScore>();
            var result = await scoreQuery.WhereEqualTo("playerName", score.PlayerName).FirstOrDefaultAsync();
            HighScore oldScore = result as HighScore;
            if (oldScore != null)
            {
                oldScore.CombosCount = score.CombosCount;
                oldScore.HighestLevelCleared = score.HighestLevelCleared;
                oldScore.HighestLevelElement = score.HighestLevelElement;
                oldScore.Points = score.Points;

                // Date Format exception!?! Really ? It is your own fucking date .!.
                try
                {
                    await oldScore.SaveAsync();
                }
                catch (FormatException)
                { }
            }
            else
            {
                await score.SaveAsync();
            }
        }

        public async Task<IEnumerable<HighScore>> GetTopScores(int count)
        {
            return await this.GetScoresPage(count, 1);
        }

        public async Task<IEnumerable<HighScore>> GetScoresPage(int count, int page)
        {
            var scoreQuery = new ParseQuery<HighScore>();
            var result = await scoreQuery.OrderByDescending(h => h.Points)
                .Skip((page - 1) * count)
                .Limit(count)
                .FindAsync();

            return result;
        }

        public async static Task<bool> CheckIfNameIsTaken(string name)
        {
            var scoreQuery = new ParseQuery<HighScore>();
            var result = await scoreQuery.WhereEqualTo("playerName", name).FirstOrDefaultAsync() as HighScore;
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
