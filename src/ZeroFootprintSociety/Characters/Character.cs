
namespace ZeroFootPrintSociety.Characters
{
    public abstract class Character
    {
        public CharacterBody Body { get; }
        public CharacterData Stats { get; }
        
        public Character(CharacterBody body, CharacterData data)
        {
            Body = body;
            Stats = data;
        }
    }
}
