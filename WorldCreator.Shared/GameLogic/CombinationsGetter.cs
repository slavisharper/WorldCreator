namespace WorldCreator.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorldCreator.ViewModels;
    using System.Linq;

    public class CombinationsGetter : IItemssGetter
    {
        private const string ImageFormatString = "../Images/{0}.png";
        public static IEnumerable<ItemViewModel> BasicItems
        {
            get
            {
                var items = new ItemViewModel[4]
                { 
                    new ItemViewModel(Items.Water, "../Images/water.png", 0, Items.Water),
                    new ItemViewModel(Items.Fire, "../Images/fire.png", 0, Items.Fire),
                    new ItemViewModel(Items.Air, "../Images/air.png", 0, Items.Air),
                    new ItemViewModel(Items.Earth, "../Images/earth.png", 0, Items.Earth)
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
                new ItemViewModel(Items.Fire, "../Images/fire.png", 1, Items.Fire),
                new ItemViewModel(Items.Water, "../Images/water.png", 1, Items.Water),
                new ItemViewModel(Items.Air, "../Images/air.png", 1, Items.Air),
                new ItemViewModel(Items.Earth, "../Images/earth.png", 1, Items.Earth),
                new ItemViewModel(Items.Steam, "../Images/steam.png", 1, Items.Water),
                new ItemViewModel(Items.Lava, "../Images/lava.png", 1, Items.Fire),
                new ItemViewModel(Items.Rain, "../Images/rain.png", 1, Items.Air),
                new ItemViewModel(Items.Dust, "../Images/dust.png", 1, Items.Earth),
                new ItemViewModel(Items.Swamp, "../Images/swamp.png", 1, Items.Water),
                new ItemViewModel(Items.Energy, "../Images/energy.png", 1, Items.Energy),
                new ItemViewModel(Items.Stone, "../Images/stone.png", 2, Items.Earth),
                new ItemViewModel(Items.Wave, "../Images/wave.png", 2, Items.Water),
                new ItemViewModel(Items.Plasma, "../Images/plasma.png", 2, Items.Fire),
                new ItemViewModel(Items.Storm, "../Images/storm.png", 2, Items.Air),
                new ItemViewModel(Items.Earthquake, "../Images/earthquake.png", 2, Items.Disaster),
                new ItemViewModel(Items.Island, "../Images/island.png", 2, Items.Water),
                new ItemViewModel(Items.Life, "../Images/life.png", 2, Items.Energy),
                new ItemViewModel(Items.Gayser, "../Images/gayser.png", 2, Items.Earth),
                new ItemViewModel(Items.Mud, "../Images/mud.png", 2, Items.Earth),
                new ItemViewModel(Items.Algae, "../Images/algae.png", 3, Items.BasicLife),
                new ItemViewModel(Items.Metal, "../Images/metal.png", 3, Items.Earth),
                new ItemViewModel(Items.Volcano, "../Images/volcano.png", 3, Items.Disaster),
                new ItemViewModel(Items.Sand, "../Images/sand.png", 3, Items.Earth),
                new ItemViewModel(Items.Egg, "../Images/egg.png", 3, Items.BasicLife),
                new ItemViewModel(Items.Human, "../Images/human.png", 3, Items.Human),
                new ItemViewModel(Items.Bacteria, "../Images/bacteria.png", 3, Items.BasicLife),
                new ItemViewModel(Items.Electricity, "../Images/electricity.png", 4, Items.Energy),
                new ItemViewModel(Items.Glass, "../Images/glass.png", 4, Items.Earth),
                new ItemViewModel(Items.Boiler, "../Images/boiler.png", 4, Items.Technology),
                new ItemViewModel(Items.Clay, "../Images/clay.png", 4, Items.Earth),
                new ItemViewModel(Items.Moss, "../Images/moss.png", 4, Items.BasicLife),
                new ItemViewModel(Items.Plankton, "../Images/plankton.png", 4, Items.BasicLife),
                new ItemViewModel(Items.Seeds, "../Images/seeds.png", 4, Items.Plant),
                new ItemViewModel(Items.Dinosaur, "../Images/dinosaur.png", 4, Items.Creatures),
                new ItemViewModel(Items.Bird, "../Images/bird.png", 4, Items.Animal),
                new ItemViewModel(Items.Robot, "../Images/robot.png", 4, Items.Technology),
                new ItemViewModel(Items.Mushroom, "../Images/mushroom.png", 4, Items.BasicLife),
                new ItemViewModel(Items.IronMan, "../Images/ironman.png", 4, Items.Human),
                new ItemViewModel(Items.Corpse, "../Images/corpse.png", 4, Items.Human),
            };

            return items;
        }

        private List<Combination> GetCombinations()
        {
            var combos = new List<Combination>()
            {
                new Combination(Items.Fire, Items.Water, Items.Steam, 1),
                new Combination(Items.Fire, Items.Earth, Items.Lava, 1),
                new Combination(Items.Fire, Items.Air, Items.Energy, 1),
                new Combination(Items.Fire, Items.Energy, Items.Plasma, 2),
                new Combination(Items.Water, Items.Earth, Items.Swamp, 1),
                new Combination(Items.Water, Items.Air, Items.Rain, 1),
                new Combination(Items.Water, Items.Lava, Items.Stone, 2),
                new Combination(Items.Water, Items.Dust, Items.Mud, 2),
                new Combination(Items.Water, Items.Energy, Items.Wave, 2),
                new Combination(Items.Earth, Items.Steam, Items.Gayser, 2),
                new Combination(Items.Earth, Items.Energy, Items.Earthquake, 2),
                new Combination(Items.Air, Items.Earth, Items.Dust, 1),
                new Combination(Items.Rain, Items.Energy, Items.Storm, 2),
                new Combination(Items.Swamp, Items.Energy, Items.Life, 2),
                new Combination(Items.Water, Items.Life, Items.Algae, 3),
                new Combination(Items.Swamp, Items.Life, Items.Bacteria, 3),
                new Combination(Items.Stone, Items.Fire, Items.Metal, 3),
                new Combination(Items.Lava, Items.Gayser, Items.Volcano, 3),
                new Combination(Items.Stone, Items.Water, Items.Sand, 3),
                new Combination(Items.Stone, Items.Life, Items.Egg, 3),
                new Combination(Items.Mud, Items.Life, Items.Human, 3),
                new Combination(Items.Metal, Items.Energy, Items.Electricity, 4),
                new Combination(Items.Sand, Items.Fire, Items.Glass, 4),
                new Combination(Items.Metal, Items.Gayser, Items.Boiler, 4),
                new Combination(Items.Swamp, Items.Sand, Items.Clay, 4),
                new Combination(Items.Algae, Items.Swamp, Items.Moss, 4),
                new Combination(Items.Water, Items.Bacteria, Items.Plankton, 4),
                new Combination(Items.Life, Items.Sand, Items.Seeds, 4),
                new Combination(Items.Egg, Items.Earth, Items.Dinosaur, 4),
                new Combination(Items.Air, Items.Egg, Items.Bird, 4),
                new Combination(Items.Water, Items.Bacteria, Items.Plankton, 4),
                new Combination(Items.Metal, Items.Life, Items.Robot, 4),
                new Combination(Items.Human, Items.Metal, Items.IronMan, 4),
                new Combination(Items.Algae, Items.Earth, Items.Mushroom, 4),
                new Combination(Items.Human, Items.Fire, Items.Corpse, 4),
            };

            return combos;
        }
    }
}
