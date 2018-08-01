using MonoDragons.Core.Engine;
using System;
using System.Collections.Generic;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.AI
{
    class EnemyAI : IAutomaton
    {
        private List<IAutomaton> _automata = new List<IAutomaton>();
        private List<object> _aiActorComponents = new List<object>();

        public EnemyAI()
        {
            var characterData = new Dictionary<Character, AICharacterData>();
            Add(new AIThinkingDelay());
            Add(new AICharacterDataInitializer(characterData));
            Add(new AIEnemyWatcher(characterData));
            Add(new AIMovement(characterData));
            Add(new AIActionSelector(characterData));
            Add(new AIShootingTargetSelector());
            Add(new AIActionConfirmer());
        }

        private void Add(IAutomaton a) => _automata.Add(a);
        private void Add(object o) => _aiActorComponents.Add(o);

        public void Update(TimeSpan delta)
        {
            _automata.ForEach(x => x.Update(delta));
        }
    }
}
