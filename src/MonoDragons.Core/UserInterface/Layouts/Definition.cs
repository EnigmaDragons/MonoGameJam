namespace MonoDragons.Core.UserInterface.Layouts
{
    public abstract class Definition
    {
        public SizeType Size { get; }
        public int Shares { get; }
        public decimal Length { get; set; }

        protected Definition(SizeType size, int shares, decimal length)
        {
            Size = size;
            Shares = shares;
            Length = length;
        }
    }
}
