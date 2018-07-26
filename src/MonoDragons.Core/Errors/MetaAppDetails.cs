namespace MonoDragons.Core.Errors
{
    public class MetaAppDetails
    {
        public string Name { get; }
        public string Version { get; }
        public string OS { get; }

        public MetaAppDetails(string name, string version, string os)
        {
            Name = name;
            Version = version;
            OS = os;
        }
    }
}
