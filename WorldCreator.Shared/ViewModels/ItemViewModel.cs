namespace WorldCreator.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
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
        }

        public double X { get; set; }

        public double Y { get; set; }

        public string Name { get; set; }

        public string IconPath { get; set; }

        public int Level { get; set; }

        public string GroupName { get; set; }
    }
}
