namespace WorldCreator.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorldCreator.Models.Parse;
    using WorldCreator.ViewModels;

    public interface IScoreManager
    {

        Task UploadScore(HighScore score);

        Task<IEnumerable<HighScore>> GetTopScores(int count);

        Task<IEnumerable<HighScore>> GetScoresPage(int count, int page);

        Task UploadScore(IPlayerViewModel player);
    }
}
