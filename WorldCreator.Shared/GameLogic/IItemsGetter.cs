namespace WorldCreator.GameLogic
{
    using System.Collections.Generic;
    using WorldCreator.ViewModels;

    public interface IItemssGetter
    {
        Dictionary<string, List<Combination>> GetAllCombinations();

        Dictionary<string, ItemViewModel> GetAllItems();

        Dictionary<string, List<Combination>> GetCombinationsForLevel(int level);

        Dictionary<string, ItemViewModel> GetItemsForLevel(int level);
    }
}
