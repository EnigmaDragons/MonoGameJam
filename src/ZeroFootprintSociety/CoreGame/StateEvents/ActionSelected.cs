namespace ZeroFootPrintSociety.CoreGame.StateEvents
{
    public class ActionSelected
    {
        public string Name { get; }

        public ActionSelected(string name) => Name = name;
    }
}
