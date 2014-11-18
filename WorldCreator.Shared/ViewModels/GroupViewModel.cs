namespace WorldCreator.ViewModels
{
    using System.Collections.Generic;
    using WorldCreator.Extensions;

    public class GroupViewModel : BaseViewModel
    {
        private IList<ItemViewModel> items;

        public GroupViewModel()
            :this("", null)
        {
        }

        public GroupViewModel(string name, IEnumerable<ItemViewModel> groupItems)
        {
            this.Items = new List<ItemViewModel>(groupItems);
            this.Name = name;
            this.IconPath = name.ToLower() + ".png";
        }

        public string Name { get; set; }

        public string IconPath { get; set; }

        public IList<ItemViewModel> Items
        {
            get 
            {
                if (this.items == null)
                {
                    this.Items = new List<ItemViewModel>();
                }

                return this.items; 
            }
            set
            {
                if (this.items == null)
                {
                    this.items = new List<ItemViewModel>();
                }

                this.items.Clear();
                this.items.AddRange(value);
            }
        }
    }
}
