namespace WorldCreator.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private string iconPath;
        private double top;
        private double left;
        private bool isSelected;

        public ItemViewModel() 
            :this("", "", 0, "")
        { }

        public ItemViewModel(string name, string iconPath, int level)
            :this(name, iconPath, level, "")
        { }

        public ItemViewModel(string name, string iconPath, int level, string groupName)
        {
            this.Name = name;
            this.IconPath = iconPath;
            this.Level = level;
            this.GroupName = groupName;
            this.IsSelected = false;
        }

        public double Top
        {
            get
            {
                return this.top;
            }
            set
            {
                this.top = value;
                this.OnPropertyChanged("Top");
            }
        }

        public double Left
        {
            get
            {
                return this.left;
            }
            set
            {
                this.left = value;
                this.OnPropertyChanged("Left");
            }
        }

        public string Name { get; set; }

        public string IconPath
        {
            get { return this.iconPath; }
            set
            {
                this.iconPath = value;
                this.OnPropertyChanged("IconPath");
            }
        }

        public bool IsSelected 
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }

        public int Level { get; set; }

        public string GroupName { get; set; }
    }
}
