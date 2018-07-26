namespace MonoDragons.Core.UserInterface.Layouts
{
    public class ShareDefintion : Definition
    {
        public ShareDefintion() : this(1) {}

        public ShareDefintion(int shares) : base(SizeType.Share, shares, 0) {}
    }
}
