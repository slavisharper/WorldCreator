namespace WorldCreator.GameLogic
{
    public class Combination
    {
        public Combination(string first, string second)
            :this(first, second, null, -1)
        {
        }

        public Combination(string first, string second, string combined, int level)
        {
            this.FirstElementName = first.ToLower();
            this.SecondElementName = second.ToLower();
            this.CombinedElementName = combined.ToLower();
            this.Level = level;
        }

        public int Level { get; set; }

        public string FirstElementName { get; set; }

        public string SecondElementName { get; set; }

        public string CombinedElementName { get; set; }

        public override int GetHashCode()
        {
            return this.FirstElementName.GetHashCode() ^
                this.SecondElementName.GetHashCode();
        }
    }
}
