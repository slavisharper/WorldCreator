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
            combos.Add("fire", new List<Combination>());
            combos.Add("water", new List<Combination>());
            combos.Add("air", new List<Combination>());
            combos.Add("earth", new List<Combination>());
            combos.Add("lava", new List<Combination>());
            combos.Add("dust", new List<Combination>());
            combos.Add("energy", new List<Combination>());
            combos.Add("rain", new List<Combination>());
            combos.Add("steam", new List<Combination>());
            combos.Add("swamp", new List<Combination>());

            combos["fire"].Add(new Combination("Fire", "Water", "Steam", 1));
            combos["fire"].Add(new Combination("Fire", "Earth", "Lava", 1));
            combos["fire"].Add(new Combination("Fire", "Air", "Energy", 1));
            combos["fire"].Add(new Combination("Fire", "Energy", "Plasma", 2));
            combos["water"].Add(new Combination("Water", "Fire", "Steam", 1));
            combos["water"].Add(new Combination("Water", "Earth", "Swamp", 1));
            combos["water"].Add(new Combination("Water", "Air", "Rain", 1));
            combos["water"].Add(new Combination("Water", "Lava", "Stone", 2));
            combos["water"].Add(new Combination("Water", "Dust", "Mud", 2));
            combos["water"].Add(new Combination("Water", "Energy", "Wave", 2));
            combos["earth"].Add(new Combination("Earth", "Water", "Swamp", 1));
            combos["earth"].Add(new Combination("Earth", "Steam", "Gayser", 2));
            combos["earth"].Add(new Combination("Earth", "Energy", "Earthquake", 2));
            combos["earth"].Add(new Combination("Earth", "Fire", "Lava", 1));
            combos["earth"].Add(new Combination("Earth", "Air", "Dust", 1));
            combos["air"].Add(new Combination("Air", "Water", "Rain", 1));
            combos["air"].Add(new Combination("Air", "Earth", "Dust", 1));
            combos["air"].Add(new Combination("Air", "Fire", "Energy", 1));
            combos["lava"].Add(new Combination("Lava", "Water", "Stone", 2));
            combos["dust"].Add(new Combination("Dust", "Water", "Mud", 2));
            combos["energy"].Add(new Combination("Energy", "Fire", "Plasma", 2));
            combos["energy"].Add(new Combination("Energy", "Sea", "Wave", 2));
            combos["energy"].Add(new Combination("Energy", "Rain", "Storm", 2));
            combos["energy"].Add(new Combination("Energy", "Earth", "Earthquake", 2));
            combos["energy"].Add(new Combination("Energy", "Water", "Wave", 2));
            combos["energy"].Add(new Combination("Energy", "Swamp", "Life", 2));
            combos["rain"].Add(new Combination("Rain", "Energy", "Storm", 2));
            combos["steam"].Add(new Combination("Steam", "Earth", "Gayser", 2));
            combos["swamp"].Add(new Combination("Swamp", "Energy", "Life", 2));
            return combos;
        }

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

                int x = 50;
                int y = 50;

                foreach (var item in items)
                {
                    item.Left = x;
                    item.Top = y;
                    x += 100;
                    y += 100;
                }

                return items;
            }
        }

        public Dictionary<string, ItemViewModel> GetAllItems()
        {
            var items = new Dictionary<string, ItemViewModel>();

            items.Add("fire", new ItemViewModel("Fire", "../Images/fire.png", 1, "Fire"));
            items.Add("water", new ItemViewModel("Water", "../Images/water.png", 1, "Water"));
            items.Add("air", new ItemViewModel("Air", "../Images/air.png", 1, "Air"));
            items.Add("earth", new ItemViewModel("Earth", "../Images/earth.png", 1, "Earth"));
            items.Add("steam", new ItemViewModel("Steam", "../Images/steam.png", 1, "Water"));
            items.Add("lava", new ItemViewModel("Lava", "../Images/lava.png", 1, "Fire"));
            items.Add("rain", new ItemViewModel("Rain", "../Images/rain.png", 1, "Air"));
            items.Add("dust", new ItemViewModel("Dust", "../Images/dust.png", 1, "Earth"));
            items.Add("swamp", new ItemViewModel("Swamp", "../Images/swamp.png", 1, "Water"));
            items.Add("energy", new ItemViewModel("Energy", "../Images/energy.png", 1, "Energy"));
            items.Add("stone", new ItemViewModel("Stone", "../Images/stone.png", 2, "Earth"));
            items.Add("wave", new ItemViewModel("Wave", "../Images/wave.png", 2, "Water"));
            items.Add("plasma", new ItemViewModel("Plasma", "../Images/plasma.png", 2, "Fire"));
            items.Add("storm", new ItemViewModel("Storm", "../Images/storm.png", 2, "Air"));
            items.Add("earthquake", new ItemViewModel("Earthquake", "../Images/earthquake.png", 2, "Disaster"));
            items.Add("island", new ItemViewModel("Island", "../Images/island.png", 2, "Water"));
            items.Add("life", new ItemViewModel("Life", "../Images/life.png", 2, "Energy"));
            items.Add("gayser", new ItemViewModel("Gayser", "../Images/gayser.png", 2, "Earth"));
            items.Add("mud", new ItemViewModel("Mud", "../Images/mud.png", 2, "Earth"));
            return items;
        }
    }
}
