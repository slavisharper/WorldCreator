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
        }

        public string Name { get; set; }

        public IList<ItemViewModel> Items
        {
            get { return this.items; }
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
