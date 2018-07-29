
namespace MonoDragons.Core.Engine
{
    public interface IInitializable
    {
        void Init();
    }

    public static class InitializationExtensions
    {
        public static T Initialized<T>(this T obj) where T : IInitializable
        {
            obj.Init();
            return obj;
        }
    }
}
