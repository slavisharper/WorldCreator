namespace WorldCreator.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorldCreator.Models.Parse;
    using WorldCreator.ViewModels;

    public interface IScoreManager
    {
        void UploadScore(HighScore score);

        Task<IEnumerable<HighScore>> GetTopScores(int count);

        Task<IEnumerable<HighScore>> GetScoresPage(int count, int page);

        void UploadScore(IPlayerViewModel player);
    }
}
