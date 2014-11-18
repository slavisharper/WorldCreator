using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldCreator.ViewModels;

namespace WorldCreator.GameLogic
{
    public class CombinationsGetter
    {
        public Dictionary<string, List<Combination>> GetAllCombinations()
        {
            var combos = new Dictionary<string, List<Combination>>();
            combos.Add("Fire", new List<Combination>());
            combos.Add("Water", new List<Combination>());
            combos.Add("Air", new List<Combination>());
            combos.Add("Earth", new List<Combination>());
            
            combos["Fire"].Add(new Combination("Fire", "Water", "Steam", 1));
            combos["Water"].Add(new Combination("Fire", "Water", "Steam", 1));

            return combos;
        }

        public async Task<Dictionary<string, List<Combination>>> GetAllCombinationsAsync()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, ItemViewModel> GetAllItems()
        {
            var items = new Dictionary<string, ItemViewModel>();

            items.Add("steam", new ItemViewModel("Steam", "steam.png", 1, "Water"));

            return items;
        }

        public async Task<Dictionary<string, ItemViewModel>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
