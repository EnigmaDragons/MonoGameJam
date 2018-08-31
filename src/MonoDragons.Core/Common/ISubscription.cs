namespace MonoDragons.Core
{
    public interface ISubscription<in T>
    {
        void Update(T change);
    }
}
