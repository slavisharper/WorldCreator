namespace WorldCreator.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorldCreator.ViewModels;

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
            combos["Fire"].Add(new Combination("Fire", "Earth", "Lava", 1));
            return combos;
        }

        public static IEnumerable<ItemViewModel> BasicItems
        {
            get
            { 
                return new ItemViewModel[4]
                { 
                    new ItemViewModel("Water", "water.png", 0, "Water"),
                    new ItemViewModel("Fire", "fire.png", 0, "Fire"),
                    new ItemViewModel("Air", "air.png", 0, "Air"),
                    new ItemViewModel("Earth", "earth.png", 0, "Earth")
                };
            }
        }

      //  public async Task<Dictionary<string, List<Combination>>> GetAllCombinationsAsync()
      //  {
      //      throw new NotImplementedException();
      //  }

        public Dictionary<string, ItemViewModel> GetAllItems()
        {
            var items = new Dictionary<string, ItemViewModel>();

            items.Add("fire", new ItemViewModel("Fire", "fire.png", 1, "Fire"));
            items.Add("water", new ItemViewModel("Water", "water.png", 1, "Water"));
            items.Add("air", new ItemViewModel("Air", "air.png", 1, "Air"));
            items.Add("earth", new ItemViewModel("Earth", "earth.png", 1, "Earth"));
            items.Add("steam", new ItemViewModel("Steam", "steam.png", 1, "Water"));
            items.Add("lava", new ItemViewModel("Lava", "lava.png", 1, "Fire"));

            return items;
        }

        //public async Task<Dictionary<string, ItemViewModel>> GetAllItemsAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
