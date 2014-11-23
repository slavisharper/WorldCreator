namespace WorldCreator.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorldCreator.ViewModels;
    using System.Linq;

    public class CombinationsGetter : IItemssGetter
    {
        public static IEnumerable<ItemViewModel> BasicItems
        {
            get
            {
                var items = new ItemViewModel[4]
                { 
                    new ItemViewModel("Water", "../Images/water.png", 0, "Water"),
                    new ItemViewModel("Fire", "../Images/fire.png", 0, "Fire"),
                    new ItemViewModel("Air", "../Images/air.png", 0, "Air"),
                    new ItemViewModel("Earth", "../Images/earth.png", 0, "Earth")
                };

                int x = 20;
                int y = 20;

                foreach (var item in items)
                {
                    item.Left = x;
                    item.Top = y;
                    x += 50;
                    y += 120;
                }

                return items;
            }
        }

        public Dictionary<string, List<Combination>> GetAllCombinations()
        {
            return this.CombosToDictionary(GetCombinations());
        }

        public Dictionary<string, ItemViewModel> GetAllItems()
        {
            return this.ItemsToDictionary(GetItems());
        }

        public Dictionary<string, List<Combination>> GetCombinationsForLevel(int level)
        {
            var combos = GetCombinations().Where(c => c.Level == level);
            return this.CombosToDictionary(combos);
        }

        public Dictionary<string, ItemViewModel> GetItemsForLevel(int level)
        {
            var items = this.GetItems().Where(i => i.Level == level);
            return this.ItemsToDictionary(items);
        }

        public Dictionary<string, List<Combination>> CombosToDictionary(IEnumerable<Combination> combos)
        {
            var combosDict = new Dictionary<string, List<Combination>>();
            foreach (var combo in combos)
            {
                this.AddComboToDictionary(combosDict, combo);
                var mirrorCombo = new Combination(combo.SecondElementName, combo.FirstElementName, combo.CombinedElementName, combo.Level);
                this.AddComboToDictionary(combosDict, mirrorCombo);
            }

            return combosDict;
        }

        private Dictionary<string, ItemViewModel> ItemsToDictionary(IEnumerable<ItemViewModel> items)
        {
            var itemsDict = new Dictionary<string, ItemViewModel>();
            foreach (var item in items)
            {
                itemsDict.Add(item.Name.ToLower(), item);
            }

            return itemsDict;
        }

        private void AddComboToDictionary(Dictionary<string, List<Combination>> combos, Combination combo)
        {
            if (combos.ContainsKey(combo.FirstElementName))
            {
                combos[combo.FirstElementName].Add(combo);
            }
            else
            {
                combos.Add(combo.FirstElementName, new List<Combination>() { combo });
            }
        }

        private List<ItemViewModel> GetItems()
        {
            var items = new List<ItemViewModel>() 
            { 
                new ItemViewModel("Fire", "../Images/fire.png", 1, "Fire"),
                new ItemViewModel("Water", "../Images/water.png", 1, "Water"),
                new ItemViewModel("Air", "../Images/air.png", 1, "Air"),
                new ItemViewModel("Earth", "../Images/earth.png", 1, "Earth"),
                new ItemViewModel("Steam", "../Images/steam.png", 1, "Water"),
                new ItemViewModel("Lava", "../Images/lava.png", 1, "Fire"),
                new ItemViewModel("Rain", "../Images/rain.png", 1, "Air"),
                new ItemViewModel("Dust", "../Images/dust.png", 1, "Earth"),
                new ItemViewModel("Swamp", "../Images/swamp.png", 1, "Water"),
                new ItemViewModel("Energy", "../Images/energy.png", 1, "Energy"),
                new ItemViewModel("Stone", "../Images/stone.png", 2, "Earth"),
                new ItemViewModel("Wave", "../Images/wave.png", 2, "Water"),
                new ItemViewModel("Plasma", "../Images/plasma.png", 2, "Fire"),
                new ItemViewModel("Storm", "../Images/storm.png", 2, "Air"),
                new ItemViewModel("Earthquake", "../Images/earthquake.png", 2, "Disaster"),
                new ItemViewModel("Island", "../Images/island.png", 2, "Water"),
                new ItemViewModel("Life", "../Images/life.png", 2, "Energy"),
                new ItemViewModel("Gayser", "../Images/gayser.png", 2, "Earth"),
                new ItemViewModel("Mud", "../Images/mud.png", 2, "Earth"),
            };

            return items;
        }

        private List<Combination> GetCombinations()
        {
            var combos = new List<Combination>()
            {
                (new Combination("Fire", "Water", "Steam", 1)),
                (new Combination("Fire", "Earth", "Lava", 1)),
                (new Combination("Fire", "Air", "Energy", 1)),
                (new Combination("Fire", "Energy", "Plasma", 2)),
                (new Combination("Water", "Earth", "Swamp", 1)),
                (new Combination("Water", "Air", "Rain", 1)),
                (new Combination("Water", "Lava", "Stone", 2)),
                (new Combination("Water", "Dust", "Mud", 2)),
                (new Combination("Water", "Energy", "Wave", 2)),
                (new Combination("Earth", "Steam", "Gayser", 2)),
                (new Combination("Earth", "Energy", "Earthquake", 2)),
                (new Combination("Air", "Earth", "Dust", 1)),
                (new Combination("Energy", "Sea", "Wave", 2)),
                (new Combination("Rain", "Energy", "Storm", 2)),
                (new Combination("Swamp", "Energy", "Life", 2)),
            };

            return combos;
        }
    }
}
