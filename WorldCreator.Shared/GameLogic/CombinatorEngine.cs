namespace WorldCreator.GameLogic
{
    using System.Collections.Generic;
    using WorldCreator.ViewModels;

    public class CombinatorEngine
    {
        private Dictionary<string, List<Combination>> combosByName;
        private Dictionary<string, ItemViewModel> items;
        private CombinationsGetter combosGetter;

        public CombinatorEngine()
            :this(new CombinationsGetter())
        {
        }

        public CombinatorEngine(CombinationsGetter comboGetter)
        {
            this.combosGetter = comboGetter;
            this.combosByName = combosGetter.GetAllCombinations();
            this.items = combosGetter.GetAllItems();
        }

        public static IEnumerable<ItemViewModel> BasicItems
        {
            get { return CombinationsGetter.BasicItems; }
        }

        public ItemViewModel PerformCombination(Combination combination)
        {
            var possibleCombos = this.combosByName[combination.FirstElementName];
            ItemViewModel item = null;

            foreach (var combo in possibleCombos)
	        {
		        if (combo.SecondElementName == combination.SecondElementName)
	            {
		            item = this.items[combo.CombinedElementName];
	            }
	        }
            
            return item;
        }

        public ItemViewModel PerformCombination(ItemViewModel first, ItemViewModel second)
        {
            Combination combination = new Combination(first.Name, second.Name);
            return this.PerformCombination(combination);
        }
    }
}
