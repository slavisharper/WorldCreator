namespace WorldCreator.ViewModels
{
    using System.Collections.Generic;
    using Windows.UI.Xaml.Media.Imaging;
    using WorldCreator.Extensions;

    public class GroupViewModel : BaseViewModel
    {
        private IList<ItemViewModel> items;
        private const string IconPathFormatString = "../Images/{0}.png";
        private string iconPath;

        public GroupViewModel()
            :this("", null)
        {
        }

        public GroupViewModel(string name, IEnumerable<ItemViewModel> groupItems)
        {
            this.Items = new List<ItemViewModel>(groupItems);
            this.Name = name;
            this.IconPath = name.ToLower();
        }

        public string Name { get; set; }

        public string IconPath
        {
            get
            {
                return this.iconPath;
            }
            private set
            {
                this.iconPath = string.Format(IconPathFormatString, value);
            }
        }

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
                this.OnPropertyChanged("Items");
            }
        }
    }
}
